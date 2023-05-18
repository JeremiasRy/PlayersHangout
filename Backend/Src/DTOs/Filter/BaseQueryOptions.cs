namespace Backend.Src.DTOs;

public class BaseQueryOptions : IFilterOptions
{
    public int Skip { get; set; } = 0;
    public int Limit { get; set; } = 30;
}
