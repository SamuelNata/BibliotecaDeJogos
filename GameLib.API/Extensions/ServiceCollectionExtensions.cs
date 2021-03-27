using System.Reflection;
using GameLib.Repository;
using GameLib.Service;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace GameLib.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Este metodo irá configuar a injeção de dependencia de todas as Repositories e Services
        /// contanto que exista apenas uma implementação para cada uma
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Scoped)
        {
            services
                .RegisterAssemblyPublicNonGenericClasses(
                    Assembly.GetAssembly(typeof(IGenericRepository<>)),
                    Assembly.GetAssembly(typeof(IGenericService<>))
                )
                .Where(c => c.Name.EndsWith("Service") || c.Name.EndsWith("Repository"))
                .AsPublicImplementedInterfaces(lifeTime);

            return services;
        }
    }
}