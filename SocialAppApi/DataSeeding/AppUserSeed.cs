using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialAppApi.Entities;

namespace SocialAppApi.DataSeeding
{
    public class AppUserSeed : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(new AppUser
            {
                Id = 1,
                UserName = "Test",
            });

            builder.HasData(new AppUser
            {
                Id = 2,
                UserName = "Test Two",
            });

            builder.HasData(new AppUser
            {
                Id = 3,
                UserName = "Test Three",
            });
        }
    }
}
