using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.RolePermissionClass
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => new { x.RoleId, x.PermissionId });
            builder.HasOne(x => x.Role)
               .WithMany(x => x.RolePermission)
               .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.Permission)
                  .WithMany(x => x.RolePermissions)
                  .HasForeignKey(x => x.PermissionId);
        }
    }
}
