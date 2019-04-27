using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace EspressoShop.ProductCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Product[] _products = {
                new Product
                {
                    Id=  1,
                    Name = "Flat white",
                    Description = "A flat white is a coffee drink consisting of espresso with microfoam (steamed milk with small, fine bubbles and a glossy or velvety consistency). It is comparable to a latte, but smaller in volume and with less microfoam, therefore having a higher proportion of coffee to milk, and milk that is more velvety in consistency – allowing the espresso to dominate the flavour, while being supported by the milk."
                },
                new Product
                {
                    Id=  2,
                    Name = "Latte",
                    Description = "A latte is a coffee drink made with espresso and steamed milk. The term as used in English is a shortened form of the Italian caffè latte, caffelatte or caffellatte, which means \"milk coffee\""
                }
            };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
