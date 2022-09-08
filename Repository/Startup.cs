using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model.ContextFolder;

namespace Repository
{

    public static class Startup
    {
        public static void ConfigureRepository(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Context>((options) =>
            {
                options.UseNpgsql(connectionString);
            });
        }
    }
}
