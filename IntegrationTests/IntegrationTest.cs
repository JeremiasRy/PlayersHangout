namespace IntegrationTests;

using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly string BaseUrl = "/api/v1";
    private readonly IConfiguration _configuration;
    public IntegrationTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("InitValues/TestData.json")
            .AddJsonFile("InitValues/city.json")
            .AddJsonFile("InitValues/genres.json")
            .AddJsonFile("InitValues/instruments.json")
            .Build();
    }
    static int _initValuesCount = 0;
    static int _signUpCount = 0;
    AuthSignUpDTO ValidSignUpDTO() 
    {
        var firstname = _configuration.GetSection("Firstnames").Get<string[]>()[_initValuesCount];
        var lastname = _configuration.GetSection("Lastnames").Get<string[]>()[_initValuesCount];
        var city = _configuration.GetSection("cities").Get<string[]>()[_initValuesCount];
        var coordinates = _configuration.GetSection($"Coordinates:{city}").GetChildren();
        var instrument = _configuration.GetSection("instruments").Get<string[]>()[_initValuesCount];
        var passSeed = _configuration.GetValue<string>("PassSeed");
        var genre = _configuration.GetSection("genres").Get<string[]>()[_initValuesCount];

        var auth = new AuthSignUpDTO()
        {
            Name = firstname + _signUpCount,
            LastName = lastname + _signUpCount,
            City = city,
            Password = passSeed + _signUpCount,
            Level = User.LevelOfCommitment.SemiPro,
            Instruments = new List<UserInstrumentDTO>()
            {
                new UserInstrumentDTO()
                {
                    Instrument = instrument,
                    LookingToPlay = true,
                    IsMain = true,
                },
            },
            Genres = new List<GenreDTO>()
            {
                new GenreDTO()
                {
                    Name = genre
                }
            },
            Email = $"mail{_signUpCount}@mail.com",
            Latitude = double.Parse(coordinates.ElementAt(0).Value, CultureInfo.InvariantCulture),
            Longitude = double.Parse(coordinates.ElementAt(1).Value, CultureInfo.InvariantCulture)
        };
        _signUpCount++;
        _initValuesCount = _initValuesCount + 1 > 11 ? 0 : _initValuesCount + 1;
        return auth;

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
            Longitude = 60,
            Level = User.LevelOfCommitment.Amateur,
            Instruments = new List<UserInstrumentDTO>()
            {
                new UserInstrumentDTO() 
                {
                    Instrument = "Mandolin",
                    LookingToPlay = true,
                    IsMain = false
                },
                new UserInstrumentDTO()
                {
                    Instrument = "Guitar",
                    LookingToPlay = true,
                    IsMain = true
                }
            }, 
            Genres = new List<GenreDTO>()
            {
                new GenreDTO()
                {
                    Name = "Blackened Crust"
                },
                new GenreDTO()
                {
                    Name = "Celtic Folk Punk"
                }
            }
           
        }, false },
        new object[] { new
        {
            Name = "InvalidUser",
            LastName = "TestLastName",
            Email = "jeremias@mail.com",
            Password = "qwerty",
            City = "Tampere",
            Level = User.LevelOfCommitment.Amateur,
            Latitude = 60,
            Longitude = 60
        }, false },
        new object[] { new
        {
            Name = "ValidUser",
            LastName = "TestLastName",
            Email = "jeremias@mail.com",
            Password = "p-55wörd12AA",
            City = "Tampere",
            Level = User.LevelOfCommitment.Amateur,
            Latitude = 60,
            Longitude = 60,
            Instruments = new List<UserInstrumentDTO>()
            {
                new UserInstrumentDTO()
                {
                    Instrument = "Mandolin",
                    LookingToPlay = true,
                    IsMain = false
                },
                new UserInstrumentDTO()
                {
                    Instrument = "Guitar",
                    LookingToPlay = true,
                    IsMain = true
                }
            },
            Genres = new List<GenreDTO>()
            {
                new GenreDTO()
                {
                    Name = "Blackened Crust"
                },
                new GenreDTO()
                {
                    Name = "Celtic Folk Punk"
                }
            }
        }, true },
        new object[] { new
        {
            Name = "ValidUser",
            LastName = "TestLastName",
            Email = "jeremias@mail.com",
            Password = "p@55wörd12AA",
            Level = User.LevelOfCommitment.Amateur,
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
    [Fact]
    public async void CreatingUsersAndWanteds()
    {
        var client = _factory.CreateClient();
        

        for (int i = 0; i < 1000; i++)
        {
            if (i == 999)
            {
                var result = await client.PostAsync($"{BaseUrl}/Auth/signup", ConvertObjToContent(ValidSignUpDTO()));
                var token = await result.Content.ReadFromJsonAsync<AuthReadDTO>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.Token);
                continue;
            }
            await client.PostAsync($"{BaseUrl}/Auth/signup", ConvertObjToContent(ValidSignUpDTO()));
        }
        var response = await client.GetAsync($"{BaseUrl}/Users");
        var users = await response.Content.ReadFromJsonAsync<ICollection<UserReadDTO>>();

        Assert.Equal(50, users?.Count);
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