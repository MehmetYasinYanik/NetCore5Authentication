
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Core.Dto
{
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
