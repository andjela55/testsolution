using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Model;
using Model.ContextFolder;
using Model.PermissionClass;
using Model.RoleClass;
using Model.RolePermissionClass;
using Model.UserClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Fixture
{
    public class CustomWebApplicationFactory<Program> : WebApplicationFactory<Program> where Program : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                       d => d.ServiceType ==
                           typeof(DbContextOptions<Context>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<Context>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<Context>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<Program>>>();

                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    try
                    {
                        //SeedData(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                        throw ex;
                    }
                }
            });
        }
        private void SeedData(Context context)
        {

            var user = new User
            {
                Id = 1,
                Name = "Andjela Filipovic",
                Email = "andjela@gmail.com",
                Password = "oRdR0yIrrbZFdjs/3mJJ6Qi1/5E=",
                Salt = "1111"

            };
            context.Users.Add(user);
            context.SaveChanges();

            var role = new Role
            {
                Id = 1,
                Name = "Admin"

            };
            context.Roles.Add(role);
            context.SaveChanges();

            var userRole = new UserRole
            {
                UserId = 3,
                RoleId = 1

            };
            context.UserRoles.Add(userRole);
            context.SaveChanges();

            var perm1 = new Permission
            {
                Id = 1,
                Name = "CanViewAllUsers"

            };
            var perm2 = new Permission
            {
                Id = 2,
                Name = "RoleAdmin"
            };
            context.Permissions.AddRange(perm1, perm2);
            context.SaveChanges();

            var rolePermissions = new List<RolePermission> {
                    new RolePermission
                    {
                        RoleId=1,
                        PermissionId=1

                    },
                    new RolePermission
                    {
                        RoleId=1,
                        PermissionId=2
                    },
                      new RolePermission
                    {
                        RoleId=2,
                        PermissionId=2
                    }
                      };
            context.RolePermissions.AddRange(rolePermissions);
            context.SaveChanges();
        }
    }
 
}
