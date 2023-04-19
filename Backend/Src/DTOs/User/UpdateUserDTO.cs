﻿namespace Backend.Src.DTOs;

using Backend.Src.Models;

public class UpdateUserDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set;} = null!;
    public string Email { get; set; } = null!;  
    public string NewPassword { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Location Location { get; set; } = null!;
}
