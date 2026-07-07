using SuperShop.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class SeedDb
    {
        private readonly DataConext _conext;

        private Random _random;
        public SeedDb(DataConext conext)
        {
            _conext = conext;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _conext.Database.EnsureCreatedAsync();

            if (!_conext.Products.Any())
            {
                AddProduct("Iphone X");
                AddProduct("Magic Mouse");
                AddProduct("IWatch Series 4");
                AddProduct("Ipad Mini");

                await _conext.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            _conext.Products.Add(new Product
            {
                Name = name,
                Price =_random.Next(1000),
                Isvailable = true,
                Stock = _random.Next(100)
            });
        }
    }
}
