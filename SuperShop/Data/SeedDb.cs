using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using System;
using System.Collections.Generic;
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
            await _conext.Database.MigrateAsync();

            await _userHelper.ChecRoleAsync("Admin");
            await _userHelper.ChecRoleAsync("Customer");

            if (!_conext.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Lisboa" });
                cities.Add(new City { Name = "Porto" });
                cities.Add(new City { Name = "Faro" });

                _conext.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Portugal"

                });

                await _conext.SaveChangesAsync();
            }



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
                    Address = "Rua jau 33",
                    CityId = _conext.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _conext.Countries.FirstOrDefault().Cities.FirstOrDefault()




                };

                var result = await _userHelper.AddUserAsync(user, "Abc123!");

                if (result != IdentityResult.Success) 
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                await _userHelper.ConfirmEmailAsync(user, token);

            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
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
