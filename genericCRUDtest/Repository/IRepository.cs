using Newtonsoft.Json.Linq;

namespace genericCRUDtest.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
       // Task AddAsync<T>(JObject jsonObject);
    }
}
