using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using SuperShop.Models;
using System;
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

        public async Task AddItemToOrderAsync(AddItemViewModel model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null) 
            {
                return;
            }

            var product = await _context.Products.FindAsync(model.ProductId);

            if (product == null) 
            {
                return;
            }

            var orderDatailTemp = await _context.OerderDetailsTemp
                .Where(odt => odt.User == user && odt.Product == product)
                .FirstOrDefaultAsync();

            if (orderDatailTemp == null) 
            {
                orderDatailTemp = new OrderDetailTemp
                {
                    Price = product.Price,
                    Product = product,
                    Quantity = model.Quantity,
                    User = user,
                };

                _context.OerderDetailsTemp.Add(orderDatailTemp);
            }
            else
            {
                orderDatailTemp.Quantity += model.Quantity;
                _context.OerderDetailsTemp.Update(orderDatailTemp);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ConfirmOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);

            if(user == null) 
            { 
                return false;
            }

            var orderTemps = await _context.OerderDetailsTemp
                .Include(o => o.Product)
                .Where(o => o.User == user)
                .ToListAsync();

            if(orderTemps == null || orderTemps.Count == 0)
            {
                return false;
            }

            var details = orderTemps.Select(o => new OrderDetail
            {
                Price = o.Price,
                Product = o.Product,
                Quantity = o.Quantity,

            }).ToList();

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                User = user,
                Items = details

            };

            await CreateAsync(order);
            _context.OerderDetailsTemp.RemoveRange(orderTemps);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteItemAsync(int id)
        {
           var orderDetailTempo = await _context.OerderDetailsTemp.FindAsync(id);

            if (orderDetailTempo == null)
            {
                return; 
            }

            _context.OerderDetailsTemp.Remove(orderDetailTempo);

            await _context.SaveChangesAsync();
        }

        public async Task DeliveryOrder(DeliveryViewModel model)
        {
             var order = await _context.Oerder.FindAsync(model.Id);

            if (order == null) 
            {
                return;
            }

            order.OrderDate = model.DeliveryDate;
            _context.Oerder.Update(order);
            await _context.SaveChangesAsync();
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
                    .Include(o => o.User)
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

        public async Task<Order> GetOrderAsync(int id)
        {
            return await _context.Oerder.FindAsync(id);
           
        }

        public async Task ModifyOrderDetailTempoQuantityAsync(int id, double quantity)
        {
            var orderDetailTamp = await _context.OerderDetailsTemp.FindAsync(id);
            if (orderDetailTamp == null) 
            { 
                return;
            }

            orderDetailTamp.Quantity += quantity;
            if (orderDetailTamp.Quantity > 0) 
            { 
                _context.OerderDetailsTemp.Update(orderDetailTamp);
                await _context.SaveChangesAsync();
            }
        }
    }
}
