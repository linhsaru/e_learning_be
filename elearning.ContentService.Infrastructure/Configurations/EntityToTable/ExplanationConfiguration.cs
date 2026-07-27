using elearning.ContentService.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class ExplanationConfiguration : IEntityTypeConfiguration<Explanation>
    {
        public void Configure(EntityTypeBuilder<Explanation> builder)
        {
            builder.ToTable("Explanations");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ExplanationText)
                .HasColumnType("text")
                .IsRequired();

            builder.HasOne(x => x.Question)
                .WithMany(x => x.Explanations)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Media)
                .WithMany()
                .HasForeignKey(x => x.MediaId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
