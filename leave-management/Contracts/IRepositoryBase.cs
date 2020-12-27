using System.Collections.Generic;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<ICollection<T>> FindAll();

        Task<T> FindById(int Id);

        Task<bool> IsExists(int Id);

        Task<bool> Create(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(T entity);

        Task<bool> Save();
    }
}