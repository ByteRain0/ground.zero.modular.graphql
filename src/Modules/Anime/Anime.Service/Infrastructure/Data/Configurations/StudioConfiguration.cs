using Anime.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anime.Service.Infrastructure.Data.Configurations;

internal class StudioConfiguration : IEntityTypeConfiguration<Studio>
{
    public void Configure(EntityTypeBuilder<Studio> builder)
    {
        builder
            .Property(x => x.Id)
            .IsRequired();

        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .HasMany(x => x.Animes)
            .WithOne(x => x.Studio);
    }
}