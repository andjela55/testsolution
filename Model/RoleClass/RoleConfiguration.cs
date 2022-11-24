using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.RoleClass
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(x => x.UserRoles)
                  .WithOne(x => x.Role)
                  .HasForeignKey(x => x.RoleId);

            builder.HasMany(x => x.RolePermission)
                  .WithOne(x => x.Role)
                  .HasForeignKey(x => x.RoleId);
        }
    }
}
