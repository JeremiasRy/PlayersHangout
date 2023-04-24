using Backend.Src.Db;
using Backend.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Backend.Src.Repositories.WantedRepo;
using Backend.Src.Services;
using Backend.Src.Services.WantedService;
using Backend.Src.Converter.Wanted;
using Backend.Src.Services.UserService;
using Backend.Src.Services.Implementation;
using Backend.Src.Repositories.GenreRepo;
using Backend.Src.Services.GenreService;
using Backend.Src.Converter.User;
using Backend.Src.Converter.Instrument;
using Backend.Src.Services.InstrumentService;
using Backend.Src.Converter.Genre;
using Backend.Src.Repositories.InstrumentRepo;
using Backend.Src.Services.AuthService;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsDevelopment())
{
    builder.Services
    .AddIdentity<User, IdentityRole<Guid>>(options =>
    {
        //To make development a bit easier
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AppDbContext>();
}

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
    });

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddDbContext<AppDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserConverter, UserConverter>();
builder.Services.AddScoped<IInstrumentConverter, InstrumentConverter>();
builder.Services.AddScoped<IGenreConverter, GenreConverter>();
builder.Services.AddScoped<IWantedConverter, WantedConverter>();
builder.Services
    .AddScoped<IGenreRepo, GenreRepo>()
    .AddScoped<IGenreService, GenreService>();
builder.Services
    .AddScoped<IInstrumentRepo, InstrumentRepo>()
    .AddScoped<IInstrumentService, InstrumentService>();
builder.Services
    .AddScoped<IWantedRepo, WantedRepo>()
    .AddScoped<IWantedService, WantedService>();

builder.Services.AddTransient<ClaimsPrincipal>(s =>
    s.GetService<IHttpContextAccessor>().HttpContext.User);
    
builder.Services.AddScoped<IAuthService, AuthService>();    
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        if (dbContext != null)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
