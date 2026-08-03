using elearning.ContentService.Domain.Questions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Hint)
                .HasColumnType("text");

            builder.Property(x => x.QuestionType)
                .IsRequired();

            builder.HasOne(x => x.QuestionSet)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.QuestionSetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.QuestionGroup)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.QuestionGroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
