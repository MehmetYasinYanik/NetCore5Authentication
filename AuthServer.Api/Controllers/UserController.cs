using AuthServer.Core.Dto;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ProjectBaseController
    {
        //41
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto) 
        {
            return ActionResultInstance(await _userService.CreateUserAsync(createUserDto)); 
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser(CreateUserDto createUserDto)
        {
            return ActionResultInstance(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }
    }
}
