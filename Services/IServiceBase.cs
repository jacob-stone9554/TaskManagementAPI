using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public interface IServiceBase<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T t);
        T Update(int id, T t);
        bool Delete(int id);
    }
}
