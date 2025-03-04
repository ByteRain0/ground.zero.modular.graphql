using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manga.Service.Infrastructure.Data.Configurations;

public class MangaConfiguration : IEntityTypeConfiguration<Contracts.Models.Manga>
{
    public void Configure(EntityTypeBuilder<Contracts.Models.Manga> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired();

        builder.Property(x => x.AuthorId)
            .IsRequired();

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Mangas);
    }
}