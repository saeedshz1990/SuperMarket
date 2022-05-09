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

            _.HasKey(_ => _.Id);

            _.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            _.Property(_ => _.Count)
                .IsRequired();

            _.Property(_ => _.CustomerName)
                .HasMaxLength(50)
                .IsRequired();

            _.Property(_ => _.SalesPrice)
                .HasColumnName("SalePrice")
                .IsRequired();

            _.Property(_ => _.SalesDate)
                .IsRequired();


            _.HasOne(_ => _.Goods)
                .WithOne(_ => _.SalesInvoices)
                .HasForeignKey<SalesInvoice>(_ => _.GoodsId)
                .OnDelete(DeleteBehavior.ClientNoAction);



        }
    }
}
