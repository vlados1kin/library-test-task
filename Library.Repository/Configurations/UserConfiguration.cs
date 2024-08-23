using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Repository.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole()
            {
                Name = "user",
                NormalizedName = "USER"
            },
            new IdentityRole()
            {
                Name = "admin",
                NormalizedName = "ADMIN"
            }
            );
    }
}