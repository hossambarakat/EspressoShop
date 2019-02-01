using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using EspressoShop.Web.Infrastructure;

namespace EspressoShop.Web.Services
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger _logger;

        public ProductServiceClient(HttpClient client, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger<ProductServiceClient>();
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                var userAgent = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                HeadersHelper.AddTracingHeaders(client, httpContextAccessor);

                var productsResponse = await client.GetAsync("api/products");

                var products = await productsResponse.Content.ReadAsAsync<List<Product>>();

                return products;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        public async Task<Product> GetProductAsync(int id)
        {
            try
            {
                _logger.LogInformation("Loading product with {ProductId}", id);
                var userAgent = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                HeadersHelper.AddTracingHeaders(client, httpContextAccessor);

                var productsResponse = await client.GetAsync($"api/products/{id}");

                var product = await productsResponse.Content.ReadAsAsync<Product>();

                return product;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
