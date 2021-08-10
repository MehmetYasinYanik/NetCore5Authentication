using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExternalTest1.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        //46
        [HttpGet]
        public IActionResult GetInvoice()
        {
            var username = HttpContext.User.Identity.Name;
            var userIdClaim = User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier);
            return Ok($"(Invoice) Username : {username} - User ID : {userIdClaim.Value}");
        }
    }
}
