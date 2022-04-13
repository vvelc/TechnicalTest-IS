using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.IRepository
{
    //This is a generic repository to interactuate with each entity
    public interface IRepository<T> where T : class
    {
        Task<bool> AddOne(T entity);
        Task<T> GetOneById(int Id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> UpdateOne(int Id, T entity);
        Task<bool> DeleteOne(int Id);
    }
}
