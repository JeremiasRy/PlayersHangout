using Backend.Src.Repositories.BaseRepo;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Backend.Src.DTOs.Filter;

public class NameFilter : IFilterOptions
{
    public string Name { get; set; } = null!;
}
