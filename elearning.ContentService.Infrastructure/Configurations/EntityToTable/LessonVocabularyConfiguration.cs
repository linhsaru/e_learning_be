using elearning.ContentService.Domain.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class LessonVocabularyConfiguration : IEntityTypeConfiguration<LessonVocabulary>
    {
        public void Configure(EntityTypeBuilder<LessonVocabulary> builder)
        {
            builder.ToTable("LessonVocabularies");
            builder.HasKey(x => new { x.LessonId, x.VocabularyId });

            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.LessonVocabularies)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Vocabulary)
                .WithMany(x => x.LessonVocabularies)
                .HasForeignKey(x => x.VocabularyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
