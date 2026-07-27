using elearning.ContentService.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.ThumbnailUrl)
                .HasMaxLength(500);

            builder.Property(x => x.Description)
                .HasColumnType("text");

            builder.HasOne(x => x.LearningPath)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.LearningPathId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Level)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.LevelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
