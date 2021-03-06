using AuthServer.Core.Configuration;
using AuthServer.Core.Dto;
using AuthServer.Core.Model;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared.Configuration;
using System;
using System.Security.Cryptography;

namespace AuthServer.Service.Service
{
    class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _customTokenOption;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _customTokenOption = options.Value;
        }

        private string CreateRefreshToken() 
        {
            //return Guid.NewGuid().ToString();

            var numberByte = new byte[32];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(numberByte);
                return Convert.ToBase64String(numberByte);
            }
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            throw new NotImplementedException();
        }

        public ClientTokenDto CreateTokenyClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
