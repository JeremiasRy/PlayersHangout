namespace IntegrationTests;

using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly string BaseUrl = "/api/v1";
    public IntegrationTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    static int count = 0;
    static AuthSignUpDTO ValidSignUpDTO() 
    {
        return new AuthSignUpDTO()
        {
            Name = "ValidUserForLogin" + count++,
            LastName = "TestLastName",
            Email = "jeremias@mail.com" + count++,
            Password = "p@55wörd12AA",
            City = "Tampere",
            Latitude = 60,
            Longitude = 60
        };
    }
    // key is item to try, value is expected result 
    static public IEnumerable<object[]> AuthSignUpDTOs() => new List<object[]>()
    {
        new object[] { new
        {
           Name = "InvalidUser",
           LastName = "TestLastName",
           Email = "jeremias@mail.com",
           Password = "p@55wörd",
           City = "Tampere",
           Latitude = 60,
           Longitude = 60
        }, false },
        new object[] { new
        {
           Name = "InvalidUser",
           LastName = "TestLastName",
           Email = "jeremias@mail.com",
           Password = "qwerty",
           City = "Tampere",
           Latitude = 60,
           Longitude = 60
        }, false },
        new object[] { new
        {
           Name = "ValidUser",
           LastName = "TestLastName",
           Email = "jeremias@mail.com",
           Password = "p@55wörd12AA",
           City = "Tampere",
           Latitude = 60,
           Longitude = 60
        }, true },
        new object[] { new
        {
           Name = "ValidUser",
           LastName = "TestLastName",
           Email = "jeremias@mail.com",
           Password = "p@55wörd12AA",
           City = "Tampere",
           Latitude = 60,
           Longitude = 60
        }, false },

    };
        
    [Theory, MemberData(nameof(AuthSignUpDTOs))]
    public async void SignUp(object input, bool expected)
    {
        var client = _factory.CreateClient();

        var result = await client.PostAsync($"{BaseUrl}/Auth/signup", ConvertObjToContent(input));
        Assert.Equal(expected, result.IsSuccessStatusCode);
        if (expected)
        {
            Converter converter = new ();
            converter.CreateModel(input, out AuthSignUpDTO authSignUpDTO);
            result = await client.PostAsync($"{BaseUrl}/Auth/login", ConvertObjToContent(new { authSignUpDTO.Email, authSignUpDTO.Password }));
            Assert.Equal(!expected, result.IsSuccessStatusCode);
        }
    }
    [Fact]
    public async void LoginLogout()
    {
        var client = _factory.CreateClient();
        var validSignUp = ValidSignUpDTO();
        var response = await client.PostAsync($"{BaseUrl}/Auth/signup", ConvertObjToContent(validSignUp));

        AuthReadDTO? token = await response.Content.ReadFromJsonAsync<AuthReadDTO>();
        Assert.NotNull(token);
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        
        var result = await client.PostAsync($"{BaseUrl}/Auth/logout", null);
        Assert.True(result.IsSuccessStatusCode);
        response = await client.PostAsync($"{BaseUrl}/Auth/login", ConvertObjToContent(new { validSignUp.Email, validSignUp.Password }));
        Assert.True(response.IsSuccessStatusCode);
        response = await client.PostAsync($"{BaseUrl}/Auth/login", ConvertObjToContent(new { Email = "öiasudföaisrt", Password = "asdhjbsdfgooo" }));
        Assert.True(!response.IsSuccessStatusCode);
    }
    [Fact]
    public async void AuthorizedEndointsRequireAuthorization()
    {
        var client = _factory.CreateClient();
        var validSignUp = ValidSignUpDTO();
        var signUpResponse = await client.PostAsync($"{BaseUrl}/Auth/signup", ConvertObjToContent(validSignUp));

        AuthReadDTO? token = await signUpResponse.Content.ReadFromJsonAsync<AuthReadDTO>();
        Assert.NotNull(token);

        var response = await client.GetAsync($"{BaseUrl}/Genres");
        Assert.True(response.IsSuccessStatusCode);
        response = await client.PostAsync($"{BaseUrl}/Genres", ConvertObjToContent(new { Name = "GenreToAdd" }));

        Assert.True(!response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        response = await client.PostAsync($"{BaseUrl}/Genres", ConvertObjToContent(new { Name = "GenreToAdd" }));

        Assert.True(response.IsSuccessStatusCode);
    }
    [Fact]
    public async void InsertDataAndQueryStrings()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("InitValues/city.json")
            .AddJsonFile("InitValues/genres.json")
            .AddJsonFile("InitValues/instruments.json")
            .Build();

        var client = _factory.CreateClient();
        var validSignUp = ValidSignUpDTO(); 

        var cities = configuration.GetSection("cities").Get<string[]>();
        var instruments = configuration.GetSection("instruments").Get<string[]>();
        var genres = configuration.GetSection("genres").Get<string[]>();

        var signUpResponse = await client.PostAsync($"{BaseUrl}/Auth/signup", ConvertObjToContent(validSignUp));
        AuthReadDTO token = new();
        JsonConvert.PopulateObject(await signUpResponse.Content.ReadAsStringAsync(), token);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

        foreach (string city in cities)
        {
            await client.PostAsync($"{BaseUrl}/Cities", ConvertObjToContent(new { Name = city }));
        }
        foreach (string instrument in instruments)
        {
            await client.PostAsync($"{BaseUrl}/Instruments", ConvertObjToContent(new { Name = instrument }));
        }
        foreach (string genre in genres)
        {
            await client.PostAsync($"{BaseUrl}/Genres", ConvertObjToContent(new { Name = genre }));
        }

        var result = await client.GetAsync($"{BaseUrl}/Genres?name=metal&limit=4");
        var content = await result.Content.ReadFromJsonAsync<ICollection<GenreDTO>>();
        Assert.NotNull(content);
        Assert.Equal(4, content.Count);
        Assert.All(content, item => item.Name.Contains("Metal"));

        result = await client.GetAsync($"{BaseUrl}/Cities?name=ki");
        var cityContent = await result.Content.ReadFromJsonAsync<ICollection<CityDTO>>();
        Assert.NotNull(cityContent);
        Assert.All(cityContent, item => item.Name.ToUpperInvariant().Contains("KI"));

        result = await client.GetAsync($"{BaseUrl}/Instruments?name=guitar");
        var instrumentContent = await result.Content.ReadFromJsonAsync<ICollection<InstrumentDTO>>();
        Assert.NotNull(instrumentContent);
        Assert.All(instrumentContent, item => item.Name.ToLower().Contains("guitar"));
    }

    static ByteArrayContent ConvertObjToContent(object obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        var buffer = System.Text.Encoding.UTF8.GetBytes(json);
        var content = new ByteArrayContent(buffer);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return content;
    }
}