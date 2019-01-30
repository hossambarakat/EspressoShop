using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace EspressoShop.Web.Services
{
    public class ReviewsServiceClient: IReviewsServiceClient
    {
        private readonly HttpClient client;

        public ReviewsServiceClient(HttpClient client)
        {
            this.client = client;
        }
        public async Task<IEnumerable<Review>> GetReviewsAsync(int productId)
        {
            var reviewsResponse = await client.GetAsync($"api/reviews/{productId}");

            var reviews = await reviewsResponse.Content.ReadAsAsync<List<Review>>();

            return reviews;
        }
    }
}
