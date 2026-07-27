using elearning.ContentService.Domain.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class MediaAssetConfiguration : IEntityTypeConfiguration<MediaAsset>
    {
        public void Configure(EntityTypeBuilder<MediaAsset> builder)
        {
            builder.ToTable("MediaAssets");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.MediaType)
                .IsRequired();

            builder.Property(x => x.Url)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.SizeInBytes)
                .IsRequired();
        }
    }
}
