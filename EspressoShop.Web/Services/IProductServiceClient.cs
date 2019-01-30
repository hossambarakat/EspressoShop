using System.Collections.Generic;
using System.Threading.Tasks;

namespace EspressoShop.Web.Services
{
    public interface IProductServiceClient
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int id);
    }
}
