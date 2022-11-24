using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.PermissionClass
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasMany(x => x.RolePermissions)
                  .WithOne(x => x.Permission)
                  .HasForeignKey(x => x.PermissionId);
        }
    }
}
