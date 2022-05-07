using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperMarket.Entities;

namespace SuperMarket.Persistence.EF.SalesInvoices
{
    public class SalesInvoiceEntityMap :IEntityTypeConfiguration<SalesInvoice>
    {
        public void Configure(EntityTypeBuilder<SalesInvoice> _)
        {
            _.ToTable("SalesInvoices");

            _.HasKey(x => x.Id);

            _.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            _.Property(x => x.Count)
                .IsRequired();

            _.Property(x => x.CustomerName)
                .HasMaxLength(50)
                .IsRequired();

            _.Property(x => x.SalesPrice)
                .HasColumnName("SalePrice")
                .IsRequired();

            _.Property(x => x.SalesDate)
                .IsRequired();


            _.HasMany(x=>x.Goods)
                .WithOne(x => x.SalesInvoices)
                .HasForeignKey(x => x.SalesInvoiceId)
                .OnDelete(DeleteBehavior.ClientNoAction);



        }
    }
}
