using elearning.ContentService.Domain.Knowledge.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class VocabularyExampleConfiguration : IEntityTypeConfiguration<VocabularyExample>
    {
        public void Configure(EntityTypeBuilder<VocabularyExample> builder)
        {
            builder.ToTable("VocabularyExamples");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Sentence)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Translation)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Phonetic)
                .HasMaxLength(255);

            builder.HasOne(x => x.Vocabulary)
                .WithMany(x => x.Examples)
                .HasForeignKey(x => x.VocabularyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AudioMedia)
                .WithMany()
                .HasForeignKey(x => x.AudioMediaId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
