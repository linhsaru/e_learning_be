using elearning.ContentService.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class QuestionGroupConfiguration : IEntityTypeConfiguration<QuestionGroup>
    {
        public void Configure(EntityTypeBuilder<QuestionGroup> builder)
        {
            builder.ToTable("QuestionGroups");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SharedContent)
                .HasColumnType("text");

            builder.HasOne(x => x.QuestionSet)
                .WithMany(x => x.QuestionGroups)
                .HasForeignKey(x => x.QuestionSetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.SharedMedia)
                .WithMany()
                .HasForeignKey(x => x.SharedMediaId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
