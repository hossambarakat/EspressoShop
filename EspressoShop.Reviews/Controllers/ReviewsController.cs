using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EspressoShop.Reviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Review[] _reviews = {
                new Review
                {
                    ProductId = 1,
                    Id=  1,
                    Headline = "Strong Enough Coffee!",
                    ReviewText = "This is the best coffee. Great flavour and good quality.",
                    CreationDate = DateTime.Today,
                    ReviewerName = "Jon",
                    Stars = 1

                },
                new Review
                {
                    ProductId = 1,
                    Id=  2,
                    Headline = "I love this coffee",
                    ReviewText = "Great coffee that has a good taste but not as strong as I would like it to be",
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
            if(productId %2 == 0)
            {
                Thread.Sleep(6000);
            }
            var serviceVersion = _configuration.GetValue<string>("SERVICE_VERSION");
            if(serviceVersion == "v1")
            {
                return _reviews.Select(x => { x.Stars = null; return x; }).Where(x => x.ProductId == productId).ToArray();
            }
            else
            {
                return _reviews.Where(x => x.ProductId == productId).ToArray();
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
