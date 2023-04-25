namespace Backend.Src.DTOs;

using System.ComponentModel.DataAnnotations;

public class AuthSignInDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}