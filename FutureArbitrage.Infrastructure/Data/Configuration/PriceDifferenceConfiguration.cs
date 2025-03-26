using FutureArbitrage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureArbitrage.Infrastructure.Data.Configuration
{
    internal class PriceDifferenceConfiguration : IEntityTypeConfiguration<PriceDifference>
    {
        public void Configure(EntityTypeBuilder<PriceDifference> builder)
        {
            builder.ToTable("PriceDifferences");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever(); 

            builder.Property(e => e.Difference)
                .HasColumnType("decimal(18,8)") 
                .IsRequired();

            builder.Property(g => g.CreatedDate)
                .IsRequired(true)
                .HasColumnName("CreatedDate");

            builder.Property(g => g.UpdatedDate)
                .IsRequired(true)
                .HasColumnName("UpdatedDate");
        }
    }
}
