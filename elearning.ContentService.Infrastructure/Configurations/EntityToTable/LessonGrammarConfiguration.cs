using elearning.ContentService.Domain.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class LessonGrammarConfiguration : IEntityTypeConfiguration<LessonGrammar>
    {
        public void Configure(EntityTypeBuilder<LessonGrammar> builder)
        {
            builder.ToTable("LessonGrammars");
            builder.HasKey(x => new { x.LessonId, x.GrammarId });

            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.LessonGrammars)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Grammar)
                .WithMany(x => x.LessonGrammars)
                .HasForeignKey(x => x.GrammarId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
