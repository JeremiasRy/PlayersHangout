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