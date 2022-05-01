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

            _.HasKey(x => x.Id);

            _.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            _.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            _.Property(x => x.BuyPrice)
                .IsRequired();

            _.Property(x => x.DateBuy)
                .IsRequired();

            _.Property(x => x.GoodsId)
                .IsRequired();

            _.HasMany(x => x.Goods)
                .WithOne(x => x.EntryDocuments)
                .HasForeignKey(x => x.EntryDocumentId);

        }
    }
}
