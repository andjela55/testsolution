using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Services.Models;
using SharedRepository;
using System.Reflection;

namespace Services
{
    public static class Startup
    {
        public static List<Assembly> SetRepository(this IServiceCollection services, string connectionString, IConfiguration config)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            var assembly = services.ConfigureRepository(connectionString);
            services.Configure<VariableConfigObject>(config.GetSection("Variables"));

            return new List<Assembly> { assembly, Assembly.GetExecutingAssembly() };
        }
    }
}
