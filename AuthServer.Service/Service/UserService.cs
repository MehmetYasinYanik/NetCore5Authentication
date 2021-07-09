using AuthServer.Core.Dto;
using AuthServer.Core.Model;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Service
{
    public class UserService : IUserService
    {
        //36
        private readonly UserManager<UserApp> _userManager;

        public UserService(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp { Email = createUserDto.Email, UserName = createUserDto.Username };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded) 
            {
                var errors = result.Errors.Select(i => i.Description).ToList();
                return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
            }
            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user),200);
        }

        public async Task<Response<UserAppDto>> GetUserByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
                return Response<UserAppDto>.Fail("User not found", 400,true);
            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }
    }
}
