namespace Backend.src.Repositories.BaseRepo;

public interface IBaseRepo<T>
{
    Task<IEnumerable<T>> GetAllAsync(int page, int pageSize);
    Task<T?> GetByIdAsync(Guid id);
    Task<T> UpdateOneAsync(T update);
    Task<bool> DeleteOneAsync(Guid id);
    Task<T?> CreateOneAsync (T create);
}

public class QueryOptions
{
    public string Sort { get; set; } = string.Empty;    
    public SortBy SortBy { get; set; }
    public int Limit { get; set; } = 30;
    public int Skip { get; set; } = 0;
}

public enum SortBy
{
    ASC,
    DESC
}