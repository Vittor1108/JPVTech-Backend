using AutoMapper;

namespace JpvTech.Application.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper ConfigureAutoMapper()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<PersonMapper>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            return mapper;
        }
    }
}
