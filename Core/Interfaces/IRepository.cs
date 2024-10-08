using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        
        Task<IEnumerable<T>> GetAllAsync();
      
        Task<T?> GetByIdAsync(int id);
       
        Task AddAsync(T entity);
      
        Task AddRangeAsync(IEnumerable<T> entities);
       
        Task RemoveAsync(T entity);
       
        Task RemoveByIdAsync(int id);
      
        Task RemoveRangeAsync(IEnumerable<T> entities);
       
        Task SaveAsync();
    }
   
}
