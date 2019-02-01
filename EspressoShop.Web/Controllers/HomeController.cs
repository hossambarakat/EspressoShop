using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EspressoShop.Web.Models;
using Humanizer;
using EspressoShop.Web.Services;

namespace EspressoShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductServiceClient _productServiceClient;
        private readonly IReviewsServiceClient _reviewsServiceClient;

        public HomeController(IProductServiceClient productServiceClient, IReviewsServiceClient reviewsServiceClient)
        {
            _productServiceClient = productServiceClient;
            _reviewsServiceClient = reviewsServiceClient;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productServiceClient.GetProductsAsync();
            var productsViewModel = products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
            return View(productsViewModel);
        }

        public async Task<IActionResult> Product(int id)
        {
            var getProductTask = _productServiceClient.GetProductAsync(id);
            var getReviewsTask = _reviewsServiceClient.GetReviewsAsync(id);
            await Task.WhenAll(getProductTask, getReviewsTask);
            var viewModel = new ProductDetailsViewModel()
            {
                Id = id,
                Name = getProductTask.Result.Name,
                Description = getProductTask.Result.Description,
            };
            if(getReviewsTask.Result != null)
            {
                viewModel.Reviews = getReviewsTask.Result.Select(x => new ReviewViewModel
                {
                    Id = x.Id,
                    Headline = x.Headline,
                    ReviewText = x.ReviewText,
                    CreationDate = x.CreationDate.Humanize(),
                    ReviewerName = x.ReviewerName,
                    Stars = x.Stars
                }).ToList();
            }
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
