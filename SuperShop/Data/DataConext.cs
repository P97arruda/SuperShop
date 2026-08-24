using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;

namespace SuperShop.Data
{
    public class DataConext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Oerder { get; set; }

        public DbSet<OrderDetail> OerderDetails { get; set; }

        public DbSet<OrderDetailTemp> OerderDetailsTemp { get; set; }

        public DataConext(DbContextOptions<DataConext> options) : base(options)
        {

        }
    }
}
