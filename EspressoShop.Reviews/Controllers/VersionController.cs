using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EspressoShop.ProductCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VersionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Get()
        {
            var serviceVersion = _configuration.GetValue<string>("SERVICE_VERSION");
            return Ok(serviceVersion);
        }
    }
}
