using Microsoft.AspNetCore.Identity;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class SeedDb
    {
        private readonly DataConext _conext;
        private readonly IUserHelper _userHelper;
       
        private Random _random;
        public SeedDb(DataConext conext, IUserHelper userHelper)
        {
            _conext = conext;
            _userHelper = userHelper;
            
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _conext.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("rafaasfs@gmail.com");
            if(user == null) 
            {
                user = new User
                {
                    FistName = "Rafael",
                    LastName = "Santos",
                    Email = "rafaasfs@gmail.com",
                    UserName = "rafaasfs@gmail.com",
                    PhoneNumber = "1234567890",
                };

                var result = await _userHelper.AddUserAsync(user, "Abc123");

                if (result != IdentityResult.Success) 
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

            }

            if (!_conext.Products.Any())
            {
                AddProduct("Iphone X", user);
                AddProduct("Magic Mouse", user);
                AddProduct("IWatch Series 4", user);
                AddProduct("Ipad Mini", user);

                await _conext.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            _conext.Products.Add(new Product
            {
                Name = name,
                Price =_random.Next(1000),
                Isvailable = true,
                Stock = _random.Next(100),
                User = user
            });
        }
    }
}
