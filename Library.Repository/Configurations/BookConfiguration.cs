using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Repository.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.ISBN)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Title).HasMaxLength(255);
        
        builder.HasData(new List<Book>()
        {
            new()
            {
                Id = new Guid("f86f763e-7f80-491d-ac29-ec93cf0048e0"),
                ISBN = "978-3-4534-3577-3",
                Name = "It",
                GenreId = new Guid("2330f6c1-3212-44fd-b231-c9ee4ffe196e"),
                Title = "Stephen King’s terrifying, classic #1 New York Times bestseller.",
                AuthorId = new Guid("2fc63da3-b9ed-480d-9543-a31037599bbb"),
            },
            new()
            {
                Id = new Guid("bd304d0c-66b5-4873-bd22-c83f804ca7b7"),
                ISBN = "978-2-2264-9274-6",
                Name = "Holly",
                GenreId = new Guid("2330f6c1-3212-44fd-b231-c9ee4ffe196e"),
                Title = "Holly Gibney, one of Stephen King’s most compelling and resourceful characters, returns in this chilling novel to solve the gruesome truth behind multiple disappearances in a midwestern town.",
                AuthorId = new Guid("2fc63da3-b9ed-480d-9543-a31037599bbb"),
            },
            new()
            {
                Id = new Guid("4dccb0be-1de4-4659-80d2-0cf971a0d599"),
                ISBN = "978-0-4608-7595-0",
                Name = "Eugene Onegin",
                GenreId = new Guid("3898242b-a685-4d49-bd06-8c7e34e14d7c"),
                Title = "Eugene Onegin is the master work of the poet whom Russians regard as the fountainhead of their literature.",
                AuthorId = new Guid("ac9417d1-a277-4974-b41c-25abde25bf38"),
            },
            new()
            {
                Id = new Guid("2c463c56-21eb-4aae-8c2b-a87bcda80256"),
                ISBN = "978-9-8515-5288-3",
                Name = "The New Land",
                GenreId = new Guid("27e0db66-79ef-448d-b20c-af2c1a769e6b"),
                Title = "The first Belarusian lyric-epic work.",
                AuthorId = new Guid("944936e4-5167-41fe-8373-0540f319c3d3"),
            },
            new()
            {
                Id = new Guid("130fa159-70c1-42fb-8f31-5907f04b20e2"),
                ISBN = "978-9-8588-1435-9",
                Name = "Heritage",
                GenreId = new Guid("27e0db66-79ef-448d-b20c-af2c1a769e6b"),
                Title = "The book of People's Poet of Belarus Yanka Kupala includes verses and poems that give an idea of the main stages of his creative path, the ideological, thematic and genre richness of his poetry, the peculiarities of his artistic skill.",
                AuthorId = new Guid("944936e4-5167-41fe-8373-0540f319c3d3"),
            }
        });
    }
}