using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace EspressoShop.Web.Services
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient client;

        public ProductServiceClient(HttpClient client)
        {
            this.client = client;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var productsResponse = await client.GetAsync("api/products");

            var products = await productsResponse.Content.ReadAsAsync<List<Product>>();

            return products;
        }
        public async Task<Product> GetProductAsync(int id)
        {
            var productsResponse = await client.GetAsync($"api/products/{id}");

            var product = await productsResponse.Content.ReadAsAsync<Product>();

            return product;
        }
    }
}
