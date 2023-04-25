namespace Backend.Src.DTOs;

public class AuthReadDTO
{
    public string[] Roles { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
}