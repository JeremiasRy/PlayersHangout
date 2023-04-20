namespace Backend.Src.DTOs.Genre;

using Backend.Src.DTOs;
using Backend.Src.Models;

public class GenreReadDTO : BaseDTO<Genre>
{
    public string Name { get; set; } = null!;

    public override void UpdateModel(Genre model)
    {
        throw new NotImplementedException();
    }
}