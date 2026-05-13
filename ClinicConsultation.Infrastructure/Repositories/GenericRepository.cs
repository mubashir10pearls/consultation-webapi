using ClinicConsultation.Application.Interfaces;
using ClinicConsultation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicConsultation.Infrastructure.Repositories
{
    public class GenericRepository<T>
     : IGenericRepository<T>
     where T : class
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<T> _dbSet;

        public GenericRepository(
            ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
