using AuthServer.Core.Configuration;
using AuthServer.Core.Dto;
using AuthServer.Core.Model;
using AuthServer.Core.Repository;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServer.Service.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        //31
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshToken;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshToken)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshToken = userRefreshToken;
        }

        public Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<ClientTokenDto>> CreateTokenByClientAsync(ClientLoginDto clientLoginDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<EmptyDataDto>> RevokeRefreshToken(string refreshToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
