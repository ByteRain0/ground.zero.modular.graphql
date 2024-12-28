using Anime.Contracts.Models;
using Core.Clasifiers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Anime.Service.Infrastructure.Data.Configurations;

internal class AnimeConfiguration : IEntityTypeConfiguration<Contracts.Models.Anime>
{
    public void Configure(EntityTypeBuilder<Contracts.Models.Anime> builder)
    {
        builder
            .Property(x => x.Id)
            .IsRequired();

        builder
            .Property(x => x.Title)
            .IsRequired();

        builder
            .Property(x => x.IsCompleted)
            .HasDefaultValue(false);

        builder
            .Property(x => x.IsAiring)
            .HasDefaultValue(false);

        builder
            .Property(x => x.Synopsis)
            .IsRequired();

        builder.Property(x => x.Demographics)
            .HasDefaultValue(Demographics.Shounen)
            .HasConversion(new EnumToStringConverter<Demographics>());
        
        builder
            .HasOne<Studio>()
            .WithMany(x => x.Animes);
    }
}