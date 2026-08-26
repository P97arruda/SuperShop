using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataConext _context;
        private readonly IUserHelper _userHelper;

        public OrderRepository(DataConext context, IUserHelper userHelper) : base(context) 
        {

            _context = context;
            _userHelper = userHelper;
        }

        public async Task<IQueryable<OrderDetailTemp>> GetDetailsTempsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            return _context.OerderDetailsTemp
                .Include(p => p.Product)
                .Where(o => o.User == user)
                .OrderBy(o => o.Product.Name);
        }

        public async Task<IQueryable<Order>> GetOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null) 
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin")) 
            {
                return _context.Oerder
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .OrderByDescending(o => o.OrderDate);
            }

            return _context.Oerder
                .Include(o => o.Items)
                .ThenInclude(p => p.Product)
                .Where(o => o.User == user)
                .OrderByDescending (o => o.OrderDate);
        }
    }
}
