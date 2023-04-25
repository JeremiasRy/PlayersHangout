namespace Backend.Src.DTOs;

using Backend.Src.Repositories;

public class NameFilter : BaseQueryOptions
{
    public string Name { get; set; } = null!;
}
