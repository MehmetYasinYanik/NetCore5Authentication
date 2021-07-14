using Microsoft.AspNetCore.Mvc;
using Shared.Dto;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectBaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(Response<T> response) where T : class 
        {
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
