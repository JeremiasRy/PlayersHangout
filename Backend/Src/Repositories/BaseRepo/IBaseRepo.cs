namespace Backend.Src.Repositories;

public interface IBaseRepo<T>
{
    Task<IEnumerable<T>> GetAllAsync(IFilterOptions? filter);
    Task<T?> GetByIdAsync(Guid id);
    Task<T> UpdateOneAsync(T update);
    Task DeleteOneAsync(T model);
    Task<T> CreateOneAsync(T create);
}

public interface IFilterOptions
{

}
public class BaseQueryOptions : IFilterOptions
{
    public int Limit { get; set; } = 50;
    public int Skip { get; set; } = 0;
}

public enum SortBy
{
    ASC,
    DESC
}