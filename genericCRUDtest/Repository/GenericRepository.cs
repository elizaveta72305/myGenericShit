using genericCRUD.Models;
using genericCRUDtest.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace genericCRUDtest.GenericRepository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(long id)
        {
            try
            {
                var entityType = typeof(T);
                var entity = await _context.FindAsync(entityType, id);

                if (entity != null)
                {
                    var includes = _context.Model.FindEntityType(entityType)?.GetNavigations().Select(n => n.Name);

                    if (includes != null)
                    {
                        foreach (var include in includes)
                        {
                            if (_context.Entry(entity).Metadata.FindNavigation(include)!.IsCollection == true)
                            {
                                _context.Entry(entity).Collection(include).Load();
                            }
                            else
                            {
                                _context.Entry(entity).Reference(include).Load();
                            }
                        }
                    }
                }

                return (T)entity!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the entity by ID.", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var query = _context.Set<T>().AsQueryable();
                foreach (var navigation in _context.Model.FindEntityType(typeof(T))!.GetNavigations())
                {
                    query = query.Include(navigation.Name);
                }
                var list = await query.ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching all entities.", ex);
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding the entity.", ex);
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating the entity.", ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting the entity.", ex);
            }
        }
    }
}
