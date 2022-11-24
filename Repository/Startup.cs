using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Model.ContextFolder;
using System.Reflection;

namespace Repository
{

    public static class Startup
    {
        public static Assembly ConfigureRepository(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Context>((options) =>
            {
                options.UseNpgsql(connectionString);
            });
            return services.GetModelAssembly();
        }
    }
}
