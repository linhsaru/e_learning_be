using elearning.ContentService.Domain.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class LessonBlockConfiguration : IEntityTypeConfiguration<LessonBlock>
    {
        public void Configure(EntityTypeBuilder<LessonBlock> builder)
        {
            builder.ToTable("LessonBlocks");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BlockType)
                .IsRequired();

            builder.Property(x => x.OrderIndex)
                .IsRequired();

            builder.Property(x => x.ContentPayload)
                .HasColumnType("jsonb");

            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.LessonBlocks)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
