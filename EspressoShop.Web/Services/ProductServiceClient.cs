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
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public ProductServiceClient(HttpClient client, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory)
        {
            this._client = client;
            this._httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger<ProductServiceClient>();
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                _client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                HeadersHelper.AddTracingHeaders(_client, _httpContextAccessor);

                var productsResponse = await _client.GetAsync("api/products");

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
                var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                _client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                HeadersHelper.AddTracingHeaders(_client, _httpContextAccessor);

                var productsResponse = await _client.GetAsync($"api/products/{id}");

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
