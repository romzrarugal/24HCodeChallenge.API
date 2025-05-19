using _24HCodeChallenge.API.Services.Interfaces;
using MyProject.Data;
using MyProject.Domain.Entities;

namespace _24HCodeChallenge.API.Services
{
    public class PizzaTypeService : IPizzaTypeService
    {
        private readonly AppDbContext _context;
        private readonly IPersistence<PizzaType, string> _persistence;
        public PizzaTypeService(AppDbContext context, IPersistence<PizzaType, string> persistence)
        {
            _context = context;
            _persistence = persistence;
        }
        public Task<IEnumerable<PizzaType>> GetAllAsync() => _persistence.GetAllAsync();
        public Task<PizzaType?> GetByIdAsync(string id) => _persistence.GetByIdAsync(id);
    }
}
