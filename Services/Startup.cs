using Microsoft.Extensions.DependencyInjection;
using Repository;
using SharedRepository;

namespace Services
{
    public static class Startup
    {
        public static void SetRepository(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.ConfigureRepository(connectionString);

        }
    }
}
