using AuthServer.Core.Configuration;
using AuthServer.Core.Dto;
using AuthServer.Core.Model;
using AuthServer.Core.Repository;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            //32
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            
            //400 Bad Request
            if (user == null) 
                return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);

            if(!await _userManager.CheckPasswordAsync(user,loginDto.Password)) 
                return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);

            var token = _tokenService.CreateToken(user);
            
            var userRefreshToken = await _userRefreshToken.Where(i => i.UserId == user.Id).FirstOrDefaultAsync();
            if (userRefreshToken == null)
                await _userRefreshToken.AddAsync(new UserRefreshToken() { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            else 
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }
            await _unitOfWork.CommitAsync();
            return Response<TokenDto>.Success(token, 200);
        }

        public Response<ClientTokenDto> CreateTokenByClientAsync(ClientLoginDto clientLoginDto)
        {
            //33
            var client = _clients.SingleOrDefault(i => i.Id == clientLoginDto.ClientId && i.Secret == clientLoginDto.ClientSecret);
            if (client == null) 
                return Response<ClientTokenDto>.Fail("Client Id or Client Secret Not Found.", 404, true);
            var token = _tokenService.CreateTokenClient(client);
            return Response<ClientTokenDto>.Success(token, 200);
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            //34
            var currentRefreshToken = await _userRefreshToken.Where(i => i.Code == refreshToken).SingleOrDefaultAsync();
            if (currentRefreshToken == null)
                return Response<TokenDto>.Fail("Refresh token not found", 404, true);
            var user = await _userManager.FindByIdAsync(currentRefreshToken.UserId);
            if (user == null)
                return Response<TokenDto>.Fail("User Id not found", 404, true);
            var token = _tokenService.CreateToken(user);
            currentRefreshToken.Code = token.RefreshToken;
            currentRefreshToken.Expiration = token.RefreshTokenExpiration;
            await _unitOfWork.CommitAsync();
            return Response<TokenDto>.Success(token, 200);
        }

        public async Task<Response<EmptyDataDto>> RevokeRefreshToken(string refreshToken)
        {
            //35
            var currentRefreshToken = await _userRefreshToken.Where(i => i.Code == refreshToken).SingleOrDefaultAsync();
            if (currentRefreshToken == null)
                return Response<EmptyDataDto>.Fail("Refresh token not found", 404, true);
            _userRefreshToken.Remove(currentRefreshToken);
            await _unitOfWork.CommitAsync();
            return Response<EmptyDataDto>.Success(200);
        }
    }
}
