using elearning.ContentService.Domain.Questions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class QuestionSetConfiguration : IEntityTypeConfiguration<QuestionSet>
    {
        public void Configure(EntityTypeBuilder<QuestionSet> builder)
        {
            builder.ToTable("QuestionSets");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(x => x.Level)
                .WithMany(x => x.QuestionSets)
                .HasForeignKey(x => x.LevelId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
