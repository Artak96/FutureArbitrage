using FutureArbitrage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutureArbitrage.Infrastructure.Data.Configuration
{
    internal class ArbitrageResultConfiguration : IEntityTypeConfiguration<ArbitrageResult>
    {
        public void Configure(EntityTypeBuilder<ArbitrageResult> builder)
        {
            builder.ToTable("ArbitrageResults");
            builder.HasKey(ar => ar.Id); 
            builder.Property(ar => ar.Id); 
           
            builder.Property(ar => ar.Timestamp)
                .IsRequired(); 
          
            builder.Property(ar => ar.PriceF1)
                .HasColumnType("decimal(18, 2)")
                .IsRequired(); 
           
            builder.Property(ar => ar.PriceF2)
                .HasColumnType("decimal(18, 2)")
                .IsRequired(); 
           
            builder.Property(ar => ar.PriceDifference)
                .HasColumnType("decimal(18, 2)")
                .IsRequired(); 

            builder.HasOne(ar => ar.FuturesContract1)
                .WithMany(fc => fc.ArbitrageResultsAsContract1)
                .HasForeignKey(ar => ar.FuturesContract1Id)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(ar => ar.FuturesContract2)
                .WithMany()
                .HasForeignKey(ar => ar.FuturesContract2Id)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
