using Microsoft.EntityFrameworkCore;
using SuperMarket.Entities;
using SuperMarket.Persistence.EF.Categories;

namespace SuperMarket.Persistence.EF
{
    public class EFDataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<EntryDocument> EntryDocuments { get; set; }

        public EFDataContext(DbContextOptions options) : base(options)
        {
        }
        public EFDataContext(string connectionString) :
            this(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        { }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(CategoryEntityMap).Assembly);
        }
    }
}
