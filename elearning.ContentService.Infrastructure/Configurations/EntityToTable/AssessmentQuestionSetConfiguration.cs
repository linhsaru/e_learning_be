using elearning.ContentService.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class AssessmentQuestionSetConfiguration : IEntityTypeConfiguration<AssessmentQuestionSet>
    {
        public void Configure(EntityTypeBuilder<AssessmentQuestionSet> builder)
        {
            builder.ToTable("AssessmentQuestionSets");
            builder.HasKey(x => new { x.AssessmentId, x.QuestionSetId });

            builder.Property(x => x.OrderIndex)
                .IsRequired();

            builder.Property(x => x.ScoreWeight)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(x => x.Assessment)
                .WithMany(x => x.AssessmentQuestionSets)
                .HasForeignKey(x => x.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.QuestionSet)
                .WithMany(x => x.AssessmentQuestionSets)
                .HasForeignKey(x => x.QuestionSetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
