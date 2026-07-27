using elearning.ContentService.Domain.Knowledge.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elearning.ContentService.Infrastructure.Configurations.EntityToTable
{
    public class GrammarExampleConfiguration : IEntityTypeConfiguration<GrammarExample>
    {
        public void Configure(EntityTypeBuilder<GrammarExample> builder)
        {
            builder.ToTable("GrammarExamples");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Sentence)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Translation)
                .HasColumnType("text")
                .IsRequired();

            builder.HasOne(x => x.Grammar)
                .WithMany(x => x.Examples)
                .HasForeignKey(x => x.GrammarId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AudioMedia)
                .WithMany()
                .HasForeignKey(x => x.AudioMediaId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
