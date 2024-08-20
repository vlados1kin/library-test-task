using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Repository.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);

        builder.HasMany(g => g.Books)
            .WithOne(b => b.Genre)
            .HasForeignKey(b => b.GenreId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new List<Genre>()
        {
            new()
            {
                Id = new Guid("2330f6c1-3212-44fd-b231-c9ee4ffe196e"),
                Name = "Novel"
            },
            new()
            {
                Id = new Guid("3898242b-a685-4d49-bd06-8c7e34e14d7c"),
                Name = "Novel in verse"
            },
            new()
            {
                Id = new Guid("27e0db66-79ef-448d-b20c-af2c1a769e6b"),
                Name = "Poem"
            }
        });
    }
}