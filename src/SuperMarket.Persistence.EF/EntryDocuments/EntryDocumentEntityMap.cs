using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperMarket.Entities;

namespace SuperMarket.Persistence.EF.EntryDocuments
{
    public class EntryDocumentEntityMap : IEntityTypeConfiguration<EntryDocument>
    {
        public void Configure(EntityTypeBuilder<EntryDocument> _)
        {
            _.ToTable("EntryDocuments");

            _.HasKey(_ => _.Id);

            _.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            _.Property(_ => _.BuyPrice)
                .IsRequired();

            _.Property(_ => _.DateBuy)
                .IsRequired();

            _.Property(_ => _.GoodsId)
                .IsRequired();

            _.HasOne(_ => _.Goods)
                .WithOne(_ => _.EntryDocuments)
                .HasForeignKey<EntryDocument>(_ => _.GoodsId)
                .OnDelete(DeleteBehavior.ClientNoAction);

        }
    }
}
