using SuperShop.Data.Entities;

namespace SuperShop.Data
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataConext conext) : base(conext)
        {
            
        }
    }
}
