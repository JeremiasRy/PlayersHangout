namespace IntegrationTests;

using Backend.Src.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    public IntegrationTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    [Fact]
    public async void AuthTests()
    {
        var client = _factory.CreateClient();

        AuthSignUpDTO authSignUpDTO = new ()
        {
            Name = "Test",
            LastName = "TestLastName",
            Email = "jeremias@mail.com",
            Password = "p@55wörd",
            City = "Tampere",
            CityId = null,
            Latitude = 60,
            Longitude = 60,
        };
        
        var response = await client.PostAsync("api/v1/Auths/signup", ConvertObjToContent(authSignUpDTO));
        Assert.False(response.IsSuccessStatusCode);
        var message = await response.Content.ReadAsStringAsync();
        Assert.True(message == "\"Passwords must have at least one uppercase ('A'-'Z').\"");
        authSignUpDTO.Password = "IhavAnUppercas5";

        response = await client.PostAsync("api/v1/Auths/signup", ConvertObjToContent(authSignUpDTO));
        Assert.False(response.IsSuccessStatusCode);
        message = await response.Content.ReadAsStringAsync();
        Assert.True(message == "\"Passwords must have at least one non alphanumeric character.\"");

        authSignUpDTO.Password = "Ih@veUppercas5";
        response = await client.PostAsync("api/v1/Auths/signup", ConvertObjToContent(authSignUpDTO));
        var content = await response.Content.ReadAsStringAsync();
        AuthReadDTO authReadDTO = new();
        JsonConvert.PopulateObject(content, authReadDTO);
        
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(authReadDTO.Expiration - DateTime.Now < TimeSpan.FromHours(1) && !(authReadDTO.Expiration - DateTime.Now > TimeSpan.FromHours(1)));

        response = await client.PostAsync("api/v1/Auths/signup", ConvertObjToContent(authSignUpDTO));
        Assert.False(response.IsSuccessStatusCode);

        AuthSignInDTO authSignInDTO = new AuthSignInDTO()
        {
            Email = authSignUpDTO.Email,
            Password = authSignUpDTO.Password
        };
        response = await client.PostAsync("api/v1/Auths/login", ConvertObjToContent(authSignInDTO));
        Assert.False(response.IsSuccessStatusCode);
        message = await response.Content.ReadAsStringAsync();
        Assert.Equal("\"There is another session active\"", message);
        
        AuthSignUpDTO authSignUpDTO2 = new()
        {
            Name = "Test2",
            LastName = "TestLastName2",
            Email = "jeremias@mail.com",
            Password = "P2@asdasdasd",
            City = "Helsinki",
            CityId = null,
            Latitude = 60,
            Longitude = 60,
        };
        response = await client.PostAsync("api/v1/Auths/signup", ConvertObjToContent(authSignUpDTO2));
        Assert.True(response.IsSuccessStatusCode);


    }

    static ByteArrayContent ConvertObjToContent(object obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        var buffer = System.Text.Encoding.UTF8.GetBytes(json);
        var content = new ByteArrayContent(buffer);
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        return content;
    }
}