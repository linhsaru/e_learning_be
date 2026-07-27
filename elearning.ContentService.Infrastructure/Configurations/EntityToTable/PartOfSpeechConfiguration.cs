using elearning.ContentService.Domain.MasterData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class PartOfSpeechConfiguration : IEntityTypeConfiguration<PartOfSpeech>
    {
        public void Configure(EntityTypeBuilder<PartOfSpeech> builder)
        {
            builder.ToTable("PartsOfSpeech");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.ShortName)
                .HasMaxLength(20);

            builder.Property(x => x.Description)
                .HasMaxLength(500);
        }
    }
}
