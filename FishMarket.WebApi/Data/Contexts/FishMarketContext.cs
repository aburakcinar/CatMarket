using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FishMarket.WebApi.Data.Contexts.Entities;
using Microsoft.EntityFrameworkCore;

namespace FishMarket.WebApi.Data.Contexts
{
    public class FishMarketContext : DbContext
    {
        #region CTOR

        public FishMarketContext(DbContextOptions<FishMarketContext> options) : base(options)
        {
        }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region DbSets

        public virtual DbSet<FishDefinition> FishDefinition { get; set; }

        #endregion
    }
}
