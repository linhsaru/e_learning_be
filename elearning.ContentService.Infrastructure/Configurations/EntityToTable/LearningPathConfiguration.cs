using elearning.ContentService.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class LearningPathConfiguration : IEntityTypeConfiguration<LearningPath>
    {
        public void Configure(EntityTypeBuilder<LearningPath> builder)
        {
            builder.ToTable("LearningPaths");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("text");

            builder.HasOne(x => x.TargetLevel)
                .WithMany(x => x.LearningPaths)
                .HasForeignKey(x => x.TargetLevelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
