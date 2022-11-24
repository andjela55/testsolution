using Microsoft.EntityFrameworkCore;
using Model.PermissionClass;
using Model.RoleClass;
using Model.RolePermissionClass;
using Model.UserClass;
using Model.UserRoleClass;
using Model.UserTokenClass;

namespace Model.ContextFolder
{
    public class Context : DbContext
    {
        private readonly string? _connectionString;

        public Context()
        {
        }
        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            Console.WriteLine($"Connection string: {_connectionString}");

            if (!string.IsNullOrEmpty(_connectionString))
            {
                optionsBuilder.UseNpgsql(_connectionString);

                //optionsBuilder.EnableSensitiveDataLogging();
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasData(new[]
                {
                    new User
                    {
                        Id=1,
                        Name="Andjela Filipovic",
                        Email="andjela@gmail.com",
                        Password="oRdR0yIrrbZFdjs/3mJJ6Qi1/5E=",
                        Salt="1111",
                        AccountConfirmed=true

                    },
                    new User
                    {
                        Id=2,
                        Name="Petar Milic",
                        Email="petar@gmail.com",
                        Password="0000",
                        Salt="1111"
                    }

                });
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasData(new[]
                {
                    new Role
                    {
                        Id=1,
                        Name="Admin"

                    },
                    new Role
                    {
                        Id=2,
                        Name="Developer"
                    }

                });
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasData(new[]
                {
                    new UserRole
                    {
                        UserId=1,
                        RoleId=1

                    },
                    new UserRole
                    {
                        UserId=2,
                        RoleId=2
                    }

                });
            });
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasData(new[]
                {
                    new Permission
                    {
                        Id=1,
                        Name="CanViewAllUsers"

                    },
                    new Permission
                    {
                        Id=2,
                        Name="RoleAdmin"
                    }

                });
            });
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasData(new[]
                {
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
                    },

                });
            });
        }
    }
}
