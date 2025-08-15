namespace Restaurant.Domain.Repositories;

public interface IGenericRepository<T> where T : class
{
    public Task<T> InsertAsync(T model);
    public Task<T> UpdateAsync(T model);
    public Task<bool> DeleteAsync(int id);
    public Task<T?> GetByIdAsync(int id);
    public Task<List<T>> GetAllAsync();
    public Task<bool> IsExistId(int id);
}