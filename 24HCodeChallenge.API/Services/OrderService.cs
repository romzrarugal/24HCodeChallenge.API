using _24HCodeChallenge.API.Services.Interfaces;
using MyProject.Data;
using MyProject.Domain.Entities;

namespace _24HCodeChallenge.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IPersistence<Order, int> _persistence;
        public OrderService(AppDbContext context, IPersistence<Order, int> persistence)
        {
            _context = context;
            _persistence = persistence;
        }
        public Task<IEnumerable<Order>> GetAllAsync() => _persistence.GetAllAsync();
        public Task<Order?> GetByIdAsync(int id) => _persistence.GetByIdAsync(id);
    }
}
