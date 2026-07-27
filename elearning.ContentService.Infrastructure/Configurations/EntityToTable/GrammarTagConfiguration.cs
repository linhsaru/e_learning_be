using elearning.ContentService.Domain.Knowledge.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class GrammarTagConfiguration : IEntityTypeConfiguration<GrammarTag>
    {
        public void Configure(EntityTypeBuilder<GrammarTag> builder)
        {
            builder.ToTable("GrammarTags");
            builder.HasKey(x => new { x.GrammarId, x.TagId });

            builder.HasOne(x => x.Grammar)
                .WithMany(x => x.GrammarTags)
                .HasForeignKey(x => x.GrammarId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.GrammarTags)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
