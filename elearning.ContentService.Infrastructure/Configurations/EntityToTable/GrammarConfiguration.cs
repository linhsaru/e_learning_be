using elearning.ContentService.Domain.Knowledge.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class GrammarConfiguration : IEntityTypeConfiguration<Grammar>
    {
        public void Configure(EntityTypeBuilder<Grammar> builder)
        {
            builder.ToTable("Grammars");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StructureName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Explanation)
                .HasColumnType("text")
                .IsRequired();

            builder.HasOne(x => x.Language)
                .WithMany(x => x.Grammars)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Level)
                .WithMany(x => x.Grammars)
                .HasForeignKey(x => x.LevelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
