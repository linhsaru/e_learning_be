using elearning.ContentService.Domain.Knowledge.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class VocabularyConfiguration : IEntityTypeConfiguration<Vocabulary>
    {
        public void Configure(EntityTypeBuilder<Vocabulary> builder)
        {
            builder.ToTable("Vocabularies");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Word)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Phonetic)
                .HasMaxLength(255);

            builder.Property(x => x.PartOfSpeech)
                .HasMaxLength(50);

            builder.Property(x => x.Meaning)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.SinoVietnamese)
                .HasMaxLength(100);

            builder.Property(x => x.Radical)
                .HasMaxLength(50);

            builder.Property(x => x.StrokeOrderJson)
                .HasColumnType("text");

            builder.HasOne(x => x.Language)
                .WithMany(x => x.Vocabularies)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Level)
                .WithMany(x => x.Vocabularies)
                .HasForeignKey(x => x.LevelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AudioMedia)
                .WithMany()
                .HasForeignKey(x => x.AudioMediaId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
