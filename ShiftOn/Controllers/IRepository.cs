using ShiftOn.Models;

namespace ShiftOn.Controllers
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(User item);
        void Create (T item);
        void Delete(Guid id);
        void Details (dynamic id);
        void Edit (T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(object id);
        Task SaveAsync();
    }
}