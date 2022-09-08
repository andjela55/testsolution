using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Shared.HelperClasses;
using SharedRepository;

namespace Services
{
    public static class Startup
    {
        public static void SetRepository(this IServiceCollection services, string connectionString, IConfiguration config)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.ConfigureRepository(connectionString);
            services.Configure<VariableConfigObject>(config.GetSection("Variables"));
        }
    }
}
