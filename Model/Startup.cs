using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Model
{
    public static class Startup
    {
        public static Assembly GetModelAssembly(this IServiceCollection service)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            return currentAssembly;
        }
    }
}
