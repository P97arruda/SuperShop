using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using SuperShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly DataConext _conext;

        public CountryRepository(DataConext conext) : base(conext) 
        {
            _conext = conext;
        }

        public async Task AddCityAsync(CityViewModel model)
        {
            var country = await this.GetCountryWithCitiesAsync(model.CountryId);
            if (country == null)
            {
                return;
            }

            country.Cities.Add(new City { Name = model.Name });
            _conext.Countries.Update(country);
            await _conext.SaveChangesAsync();
        }

        public async Task<int> DeleteCityAsync(City city)
        {
            var country = await _conext.Countries.Where(c => c.Cities.Any(ci => ci.Id == city.Id)).FirstOrDefaultAsync();
            if (country == null)
            {
                return 0;
            }

            _conext.Cities.Remove(city);
            await _conext.SaveChangesAsync();
            return country.Id;
        }

        public async Task<City> GetCityAsync(int id)
        {
            return await _conext.Cities.FindAsync(id);
        }

        public IQueryable GetCountriesWithCities()
        {
            return _conext.Countries.Include(c => c.Cities).OrderBy(c => c.Name);
        }

        public async Task<Country> GetCountryWithCitiesAsync(int id)
        {
            return await _conext.Countries.Include(c => c.Cities).Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateCityAsync(City city)
        {
            var country = await _conext.Countries.Where(c => c.Cities.Any(ci => ci.Id == city.Id)).FirstOrDefaultAsync();
            if (country == null)
            {
                return 0;
            }

            _conext.Cities.Update(city);
            await _conext.SaveChangesAsync();
            return country.Id;
        }
    }
}
