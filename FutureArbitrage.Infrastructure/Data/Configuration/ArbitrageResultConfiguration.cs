using FutureArbitrage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureArbitrage.Infrastructure.Data.Configuration
{
    internal class ArbitrageResultConfiguration : IEntityTypeConfiguration<ArbitrageResult>
    {
        public void Configure(EntityTypeBuilder<ArbitrageResult> builder)
        {
            builder.HasKey(ar => ar.Id); // Set Id as primary key
            builder.Property(ar => ar.Id); // Auto-generate GUID
            builder.Property(ar => ar.Timestamp).IsRequired(); // Timestamp is required
            builder.Property(ar => ar.PriceF1).HasColumnType("decimal(18, 2)").IsRequired(); // PriceF1 as decimal
            builder.Property(ar => ar.PriceF2).HasColumnType("decimal(18, 2)").IsRequired(); // PriceF2 as decimal
            builder.Property(ar => ar.PriceDifference).HasColumnType("decimal(18, 2)").IsRequired(); // PriceDifference as decimal

            builder.HasOne(ar => ar.FuturesContract1)
                .WithMany(fc => fc.ArbitrageResultsAsContract1)
                .HasForeignKey(ar => ar.FuturesContract1Id)
                .OnDelete(DeleteBehavior.Restrict); // Set FK for FuturesContract1

            builder.HasOne(ar => ar.FuturesContract2)
                .WithMany()
                .HasForeignKey(ar => ar.FuturesContract2Id)
                .OnDelete(DeleteBehavior.Restrict); // Set FK for FuturesContract2
        }
    }
}
