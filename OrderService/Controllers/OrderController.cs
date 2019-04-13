using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        // GET api/order
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "This is order api !";
        }
    }
}
