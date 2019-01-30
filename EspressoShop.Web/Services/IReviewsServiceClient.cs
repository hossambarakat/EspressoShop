using System.Collections.Generic;
using System.Threading.Tasks;

namespace EspressoShop.Web.Services
{
    public interface IReviewsServiceClient
    {
        Task<IEnumerable<Review>> GetReviewsAsync(int productId);
    }
}
