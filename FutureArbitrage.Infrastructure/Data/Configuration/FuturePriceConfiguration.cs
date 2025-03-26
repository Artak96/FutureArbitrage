using FutureArbitrage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureArbitrage.Infrastructure.Data.Configuration
{
    internal class FuturePriceConfiguration : IEntityTypeConfiguration<FuturePrice>
    {
        public void Configure(EntityTypeBuilder<FuturePrice> builder)
        {
            builder.ToTable("FuturePrices");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            builder.Property(e => e.Symbol)
                .IsRequired()
                .HasMaxLength(50); 

            builder.Property(e => e.Price)
                .HasColumnType("decimal(18,4)");

            builder.Property(g => g.CreatedDate)
                .IsRequired(true)
                .HasColumnName("CreatedDate");

            builder.Property(g => g.UpdatedDate)
                .IsRequired(true)
                .HasColumnName("UpdatedDate");
        }
    }
}
