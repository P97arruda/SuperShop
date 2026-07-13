using SuperShop.Data.Entities;
using SuperShop.Models;
using System.Xml.Linq;

namespace SuperShop.Helpers
{
    public class ConvertHelper : IConverterHelper
    {
        public Product ToProduct(ProductViewModel model, string path, bool isNew)
        {
            return new Product
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                Isvailable = model.Isvailable,
                LastPurchase = model.LastPurchase,
                LastSale = model.LastSale,
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
                User = model.User,

            };
        }

        public ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Isvailable = product.Isvailable,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = product.User,
            };
            
        }
    }
}
