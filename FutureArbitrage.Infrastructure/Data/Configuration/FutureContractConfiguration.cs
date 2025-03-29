using FutureArbitrage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureArbitrage.Infrastructure.Data.Configuration
{
    public class FutureContractConfiguration : IEntityTypeConfiguration<FutureContract>
    {
        public void Configure(EntityTypeBuilder<FutureContract> builder)
        {
            builder.HasKey(fc => fc.Id); // Set Id as primary key
            builder.Property(fc => fc.Id); // Auto-generate GUID
            builder.Property(fc => fc.Symbol).IsRequired().HasMaxLength(50); // Symbol is required and max length 50
            builder.Property(fc => fc.DeliveryDate).IsRequired(); // DeliveryDate is required
            builder.Property(fc => fc.Asset).IsRequired().HasMaxLength(50); // Asset is required and max length 50

            builder.HasMany(fc => fc.FuturesPrices)
                .WithOne(fp => fp.FuturesContract)
                .HasForeignKey(fp => fp.FutureContractId); // Relationship with FuturePrice

            builder.HasMany(fc => fc.ArbitrageResultsAsContract1)
                .WithOne(ar => ar.FuturesContract1)
                .HasForeignKey(ar => ar.FuturesContract1Id); // Relationship with ArbitrageResult
        }
    }
}
