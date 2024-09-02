using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Repository.Configurations;

public class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.ToTable("Issues");
        
        builder.HasKey(i => i.Id);

        builder.HasOne(i => i.Book)
            .WithOne(b => b.Issue)
            .HasForeignKey<Issue>(i => i.BookId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.User)
            .WithMany(u => u.Issues)
            .HasForeignKey(i => i.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(i => i.ReceiveTime).IsRequired();

        builder.Property(i => i.ReturnTime).IsRequired();
    }
}