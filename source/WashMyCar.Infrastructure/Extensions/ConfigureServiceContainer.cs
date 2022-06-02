using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WashMyCar.Infrastructure.Mapping;

namespace WashMyCar.Infrastructure.Extensions
{
    public static class ConfigureServiceContainer
    {
        public static void AddAutoMapper(this IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ColorProfile());
                
            });
            IMapper mapper = mappingConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }

    }
}
