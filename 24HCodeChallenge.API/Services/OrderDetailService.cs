using _24HCodeChallenge.API.Services.Interfaces;
using MyProject.Data;
using MyProject.Domain.Entities;

namespace _24HCodeChallenge.API.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly AppDbContext _context;
        private readonly IPersistence<OrderDetail, int> _persistence;
        public OrderDetailService(AppDbContext context, IPersistence<OrderDetail, int> persistence)
        {
            _context = context;
            _persistence = persistence;
        }
        public Task<IEnumerable<OrderDetail>> GetAllAsync() => _persistence.GetAllAsync();
        public Task<OrderDetail?> GetByIdAsync(int id) => _persistence.GetByIdAsync(id);
    }
}
