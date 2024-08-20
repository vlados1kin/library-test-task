using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Repository.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.Birthday).IsRequired();

        builder.Property(a => a.Country)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new List<Author>()
        {
            new()
            {
                Id = new Guid("2fc63da3-b9ed-480d-9543-a31037599bbb"),
                FirstName = "Stephen",
                LastName = "King",
                Birthday = new DateOnly(1947, 09, 21),
                Country = "USA"
            },
            new()
            {
                Id = new Guid("ac9417d1-a277-4974-b41c-25abde25bf38"),
                FirstName = "Alexander",
                LastName = "Pushkin",
                Birthday = new DateOnly(1799, 05, 26),
                Country = "Russia"
            },
            new()
            {
                Id = new Guid("944936e4-5167-41fe-8373-0540f319c3d3"),
                FirstName = "Yakub",
                LastName = "Kolas",
                Birthday = new DateOnly(1882, 11, 3),
                Country = "Belarus"
            }
        });
    }
}