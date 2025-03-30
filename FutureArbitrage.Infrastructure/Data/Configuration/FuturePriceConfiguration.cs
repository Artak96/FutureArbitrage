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
            builder.HasKey(fp => fp.Id);
            builder.Property(fp => fp.Id); 
           
            builder.Property(fp => fp.Timestamp)
                .IsRequired();
            
            builder.Property(fp => fp.Price)
                .HasColumnType("decimal(18, 2)")
                .IsRequired(); 

            builder.HasOne(fp => fp.FuturesContract)
                .WithMany(fc => fc.FuturesPrices)
                .HasForeignKey(fp => fp.FutureContractId);

            builder.Property(g => g.CreatedDate)
                .IsRequired(true)
                .HasColumnName("CreatedDate");

            builder.Property(g => g.UpdatedDate)
                .IsRequired(true)
                .HasColumnName("UpdatedDate");
        }
    }
}
