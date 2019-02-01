using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using EspressoShop.Web.Infrastructure;

namespace EspressoShop.Web.Services
{
    public class ReviewsServiceClient: IReviewsServiceClient
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ReviewsServiceClient(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<Review>> GetReviewsAsync(int productId)
        {
            try
            {
                var userAgent = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                HeadersHelper.AddTracingHeaders(client, httpContextAccessor);

                var reviewsResponse = await client.GetAsync($"api/reviews/{productId}");

                var reviews = await reviewsResponse.Content.ReadAsAsync<List<Review>>();

                return reviews;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
