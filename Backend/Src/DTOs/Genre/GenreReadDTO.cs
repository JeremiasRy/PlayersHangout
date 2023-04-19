namespace Backend.src.DTOs.Genre;

using Backend.Src.DTOs;
using Backend.src.Models;

public class GenreReadDTO : BaseDTO<Genre>
{
    public string Name { get; set; } = null!;

    public override void UpdateModel(Genre model)
    {
        throw new NotImplementedException();
    }
}