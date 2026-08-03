using elearning.ContentService.Domain.Questions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
    {
        public void Configure(EntityTypeBuilder<Assessment> builder)
        {
            builder.ToTable("Assessments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.ExamType)
                .IsRequired();

            builder.Property(x => x.TimeLimitMinutes)
                .IsRequired();

            builder.Property(x => x.PassScore)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.TotalScore)
                .HasColumnType("decimal(18,2)");
        }
    }
}
