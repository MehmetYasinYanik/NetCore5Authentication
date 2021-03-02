using AutoMapper;
using System;

namespace AuthServer.Service
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> mapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(conf =>
            {
                conf.AddProfile<DtoMapper>();
            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => mapper.Value;
    }
}
