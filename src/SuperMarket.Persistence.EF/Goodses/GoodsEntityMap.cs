using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperMarket.Entities;

namespace SuperMarket.Persistence.EF.Goodses
{
    public class GoodsEntityMap : IEntityTypeConfiguration<Goods>
    {
        public void Configure(EntityTypeBuilder<Goods> _)
        {
            _.ToTable("Goods");

            _.HasKey(x => x.Id);

            _.Property(x => x.Id)
                .IsRequired();

            _.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            _.Property(x => x.Count)
                .IsRequired();

            _.Property(x => x.SalesPrice)
                .IsRequired();

            _.Property(x => x.MinimumInventory)
                .IsRequired();

            _.Property(x => x.UniqueCode)
                .IsRequired();

            _.Property(x => x.EntryDocumentId)
                .HasDefaultValue(1);
            
            _.Property(x=>x.SalesInvoiceId)
                .HasDefaultValue(1);

            _.HasOne(x => x.Category)
                .WithMany(x => x.Goods)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
