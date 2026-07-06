using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;

namespace SuperShop.Data
{
    public class DataConext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DataConext(DbContextOptions<DataConext> options) : base(options)
        {

        }
    }
}
