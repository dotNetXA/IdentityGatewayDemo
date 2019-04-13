using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        // GET api/product
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "This is product api!";
        }
    }
}
