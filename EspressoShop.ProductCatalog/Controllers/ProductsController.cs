using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace EspressoShop.ProductCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private Product[] products = new Product[] {
                new Product
                {
                    Id=  1,
                    Name = "Latte",
                    Description = "asd"
                },
                new Product
                {
                    Id=  2,
                    Name = "Flat white",
                    Description = "cool coffee"
                }
            };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return products.FirstOrDefault(x => x.Id == id);
        }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
