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
                },
                new Product
                {
                    Id=  3,
                    Name = "Macchiato",
                    Description = "Caffè macchiato, sometimes called espresso macchiato, is an espresso coffee drink with a small amount of milk, usually foamed"
                },
                new Product
                {
                    Id=  4,
                    Name = "Cappuccino",
                    Description = "A cappuccino is an espresso-based coffee drink that originated in Italy, and is traditionally prepared with steamed milk foam. Variations of the drink involve the use of cream instead of milk, and flavoring with cinnamon or chocolate powder"
                },
                new Product
                {
                    Id=  5,
                    Name = "Espresso",
                    Description = "Espresso is coffee of Italian origin, brewed by expressing or forcing a small amount of nearly boiling water under pressure through finely ground coffee beans"
                },
                new Product
                {
                    Id=  6,
                    Name = "Piccolo",
                    Description = "A baby latte that is made with espresso and steamed milk."
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
