using JPVTech.Commons;
using JPVTech.Commons.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JpvTech.Application.Injectors
{
    public static class CommonsInjector
    {
        public static IServiceCollection ServicesCollectionCommons(this IServiceCollection services)
        {
            services.AddScoped<IResponseCommon, ResponseCommon>();
            return services;
        }
    }
}
