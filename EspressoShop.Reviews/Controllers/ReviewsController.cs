using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace EspressoShop.Reviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private Review[] reviews = new Review[] {
                new Review
                {
                    ProductId = 1,
                    Id=  1,
                    Headline = "Cool Coffee",
                    ReviewText = "asd",
                    CreationDate = DateTime.Today,
                    ReviewerName = "Jon"

                },
                new Review
                {
                    ProductId = 1,
                    Id=  2,
                    Headline = "Flat white",
                    ReviewText = "cool coffee",
                    CreationDate = DateTime.Today.AddDays(-20),
                    ReviewerName = "Paul"
                }
            };

        [HttpGet("{productId}")]
        public ActionResult<IEnumerable<Review>> Get(int productId)
        {
            return reviews.Where(x => x.ProductId == productId).ToArray() ;
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
    }
}
