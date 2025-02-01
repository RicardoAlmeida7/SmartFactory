namespace SmartFactoryDomain.Interfaces.Repository
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<T?> GetByIdAsync(int id);

        Task<T?> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
