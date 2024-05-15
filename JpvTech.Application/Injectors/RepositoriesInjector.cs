

using JPVTech.Data.Repositories;
using JPVTech.Domain.Entities;
using JPVTech.Domain.Interfaces.Repositories;

namespace JpvTech.Application.Injectors
{
    public static class RepositoriesInjector
    {
        public static IServiceCollection ServiceCollectionRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<PersonEntity>, BaseRepository<PersonEntity>>();
            return services;
        }
    }
}
