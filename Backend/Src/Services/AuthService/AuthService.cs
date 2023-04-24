namespace Backend.Src.Services.AuthService;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Backend.Src.Models;
using System.Security.Claims;
using Backend.Src.DTOs.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _tokenService;
    private readonly  ClaimsPrincipal _claim;

    public AuthService(UserManager<User> userManager, IJwtTokenService tokenService, ClaimsPrincipal claim)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _claim = claim;
    }

    public async Task<AuthReadDTO?> Login(AuthSignInDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);        
        if (user is null) 
        {
            throw new Exception("The Email is not valid");
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
             throw new Exception("Wrong Password");
        }        

        if (user.ActiveSession)
        {
            throw new Exception("There is another session active");
        }

        user.ActiveSession = true;
        await _userManager.UpdateAsync(user);
        return await _tokenService.GenerateToken(user);
    }

    public async Task<AuthReadDTO> SignUp(AuthSignUpDTO request)
    {
        var user = new User
        {
            UserName = request.Email,
            FirstName = request.Name,
            LastName = request.LastName,
            Email = request.Email
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {   
            throw new Exception(result.Errors.ToList()[0].Description ?? "Error Registering User");
        }

        user.ActiveSession = true;
        await _userManager.UpdateAsync(user);
        return await _tokenService.GenerateToken(user);
    }

    public async Task<bool> Logout()
    {
        var user = await _userManager.FindByIdAsync(GetUserId());
        if (user is null)
        {
            throw new Exception("User is not found");
        }
        
        user.ActiveSession = false;
        await _userManager.UpdateAsync(user);
        return true;
    }

    public string GetUserId()
    {
        var claim = _claim.FindFirst(ClaimTypes.NameIdentifier);
        Console.WriteLine("get user id ----> ", claim.Value);
        return claim.Value ?? "";        
    }
}