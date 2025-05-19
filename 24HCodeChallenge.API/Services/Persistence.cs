using _24HCodeChallenge.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

namespace _24HCodeChallenge.API.Services
{
    public class Persistence<T, TKey> : IPersistence<T, TKey> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Persistence(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
