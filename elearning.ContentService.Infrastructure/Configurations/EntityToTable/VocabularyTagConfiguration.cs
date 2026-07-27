using elearning.ContentService.Domain.Knowledge.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class VocabularyTagConfiguration : IEntityTypeConfiguration<VocabularyTag>
    {
        public void Configure(EntityTypeBuilder<VocabularyTag> builder)
        {
            builder.ToTable("VocabularyTags");
            builder.HasKey(x => new { x.VocabularyId, x.TagId });

            builder.HasOne(x => x.Vocabulary)
                .WithMany(x => x.VocabularyTags)
                .HasForeignKey(x => x.VocabularyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.VocabularyTags)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
