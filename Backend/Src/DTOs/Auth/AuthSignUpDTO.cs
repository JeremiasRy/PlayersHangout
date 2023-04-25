namespace Backend.Src.DTOs;

using System.ComponentModel.DataAnnotations;

public class AuthSignUpDTO
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

}