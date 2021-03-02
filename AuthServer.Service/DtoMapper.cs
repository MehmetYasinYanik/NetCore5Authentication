using AuthServer.Core.Dto;
using AuthServer.Core.Model;
using AutoMapper;

namespace AuthServer.Service
{
    class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<UserAppDto, UserApp>().ReverseMap();
        }
    }
}
