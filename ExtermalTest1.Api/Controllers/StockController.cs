using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace ExternalTest1.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        //46
        [HttpGet]
        public IActionResult GetStock() 
        {
            var username = HttpContext.User.Identity.Name;
            var userIdClaim = User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier);
            return Ok($"(Stock) Username : {username} - User ID : {userIdClaim.Value}");
        }
    }
}
