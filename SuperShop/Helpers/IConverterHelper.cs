using Microsoft.AspNetCore.Mvc;
using SuperShop.Data.Entities;
using SuperShop.Models;

namespace SuperShop.Helpers
{
    public interface IConverterHelper
    {
        Product ToProduct(ProductViewModel model, string pathg, bool isNew); 

        ProductViewModel ToProductViewModel(Product product);
    }
}
