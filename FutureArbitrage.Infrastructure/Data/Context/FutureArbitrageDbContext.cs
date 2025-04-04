﻿using FutureArbitrage.Domain.Entities;
using FutureArbitrage.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FutureArbitrage.Infrastructure.Data.Context
{
    public sealed class FutureArbitrageDbContext : DbContext
    {
        public FutureArbitrageDbContext(DbContextOptions<FutureArbitrageDbContext> options) : base(options)
        {
        }

        public FutureArbitrageDbContext()
        {
        }

        public DbSet<ArbitrageResult> ArbitrageResults { get; set; }
        public DbSet<FutureContract> FutureContracts { get; set; }
        public DbSet<FuturePrice> FuturePrices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);
                var entityConfigurationsAssembly = typeof(FuturePriceConfiguration).GetTypeInfo().Assembly;
                modelBuilder.ApplyConfigurationsFromAssembly(entityConfigurationsAssembly);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException!.ToString());
            }
        }
    }
}
