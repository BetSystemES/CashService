﻿using CashService.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CashService.DataAccess
{
    public class CashDbContext : DbContext
    {
        public CashDbContext(DbContextOptions<CashDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ProfileEntityConfiguration())
                .ApplyConfiguration(new TransactionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}