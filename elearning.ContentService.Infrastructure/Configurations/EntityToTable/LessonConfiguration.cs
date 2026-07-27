using elearning.ContentService.Domain.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.OrderIndex)
                .IsRequired();

            builder.HasOne(x => x.Unit)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
