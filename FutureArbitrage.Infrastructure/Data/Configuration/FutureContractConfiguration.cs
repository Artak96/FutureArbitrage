using FutureArbitrage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureArbitrage.Infrastructure.Data.Configuration
{
    public class FutureContractConfiguration : IEntityTypeConfiguration<FutureContract>
    {
        public void Configure(EntityTypeBuilder<FutureContract> builder)
        {
            builder.ToTable("FutureContracts");
            builder.HasKey(fc => fc.Id); 
            builder.Property(fc => fc.Id); 
           
            builder.Property(fc => fc.Symbol)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(fc => fc.DeliveryDate)
                .IsRequired(); 
           
            builder.Property(fc => fc.Asset)
                .IsRequired()
                .HasMaxLength(50); 

            builder.HasMany(fc => fc.FuturesPrices)
                .WithOne(fp => fp.FuturesContract)
                .HasForeignKey(fp => fp.FutureContractId); 

            builder.HasMany(fc => fc.ArbitrageResultsAsContract1)
                .WithOne(ar => ar.FuturesContract1)
                .HasForeignKey(ar => ar.FuturesContract1Id); 
        }
    }
}
