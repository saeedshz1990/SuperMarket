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

            _.HasKey(_ => _.Id);

           _.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            
            _.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);

            _.Property(_ => _.Count)
                .IsRequired();

            _.Property(_ => _.SalesPrice)
                .IsRequired();

            _.Property(_ => _.MinimumInventory)
                .IsRequired();

            _.Property(_ => _.UniqueCode)
                .IsRequired();

            _.HasOne(_ => _.Category)
                .WithMany(_ => _.Goods)
                .HasForeignKey(_ => _.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
