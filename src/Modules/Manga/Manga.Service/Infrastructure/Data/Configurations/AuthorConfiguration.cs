using Manga.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manga.Service.Infrastructure.Data.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasMany(x => x.Mangas)
            .WithOne(x => x.Author);
    }
}