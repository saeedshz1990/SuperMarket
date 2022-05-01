using System;
using Microsoft.EntityFrameworkCore;
using SuperMarket.Entities;

namespace SuperMarket.Persistence.EF
{
    public class EFDataContext :DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<EntryDocument> EntryDocuments { get; set; }
        

        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);
        }        
    }
}
