using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.UserClass
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.UserRoles)
                  .WithOne(x => x.User)
                  .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.UserTokens)
                  .WithOne(x => x.User)
                  .HasForeignKey(x => x.UserId);
        }
    }
}
