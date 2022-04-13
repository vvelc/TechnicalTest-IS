using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.IRepository;
using Persistence.Services;

namespace Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BookServices<T> _context;

        // Repository Constructor
        public Repository()
        {
            _context = new BookServices<T>();
        }

        // Add One entity to database
        public async Task<bool> AddOne(T entity)
        {
            //Use the context to add an entity to database 
            return await _context.AddAsync(entity);
        }

        // Get All entities from database
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.GetListAsync();
        }

        // Get One entity from database
        public async Task<T> GetOneById(int Id)
        {
            return await _context.GetByIdAsync(Id);
        }

        // Update One entity from database
        public async Task<bool> UpdateOne(int Id, T entity)
        {
            return await _context.UpdateAsync(Id, entity);
        }

        // Delete One entity from database
        public async Task<bool> DeleteOne(int Id)
        {
            return await _context.DeleteAsync(Id);
        }
    }
}
