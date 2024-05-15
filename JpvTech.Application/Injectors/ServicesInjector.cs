

using JPVTech.Domain.Entities;
using JPVTech.Domain.Interfaces.Services;
using JPVTech.Service.Services;

namespace JpvTech.Application.Injectors
{
    public static class ServicesInjector
    {
        public static IServiceCollection ServiceCollectionServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseService<PersonEntity>, BaseService<PersonEntity>>();
            services.AddScoped<IUriService>(o =>
            {
                var acessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = acessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            return services;
        }
    }
}
