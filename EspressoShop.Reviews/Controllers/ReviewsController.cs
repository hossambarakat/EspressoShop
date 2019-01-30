using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EspressoShop.Reviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private Review[] reviews = new Review[] {
                new Review
                {
                    ProductId = 1,
                    Id=  1,
                    Headline = "Cool Coffee",
                    ReviewText = "asd",
                    CreationDate = DateTime.Today,
                    ReviewerName = "Jon",
                    Stars = 1

                },
                new Review
                {
                    ProductId = 1,
                    Id=  2,
                    Headline = "Flat white",
                    ReviewText = "cool coffee",
                    CreationDate = DateTime.Today.AddDays(-20),
                    ReviewerName = "Paul",
                    Stars = 5
                }
            };

        public ReviewsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{productId}")]
        public ActionResult<IEnumerable<Review>> Get(int productId)
        {
            var serviceVersion = _configuration.GetValue<string>("SERVICE_VERSION");
            if(serviceVersion == "v1")
            {
                return reviews.Select(x => { x.Stars = null; return x; }).Where(x => x.ProductId == productId).ToArray();
            }
            else
            {
                return reviews.Where(x => x.ProductId == productId).ToArray();
            }
            
        }
    }
    public class Review
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreationDate { get; set; }
        public int ProductId { get; set; }
        public string ReviewerName { get; set; }
        public int? Stars { get; set; }
    }
}
