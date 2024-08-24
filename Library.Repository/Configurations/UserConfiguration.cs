using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Repository.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        builder.HasData(
            new IdentityRole<Guid>()
            {
                Id = new Guid("a35cdaa5-ce07-4f58-a87a-a9f7e2a49ce9"),
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole<Guid>()
            {
                Id = new Guid("f620cb5e-034f-4528-8530-4c0e5dde3f2b"),
                Name = "Admin",
                NormalizedName = "ADMIN"
            }
            );
    }
}