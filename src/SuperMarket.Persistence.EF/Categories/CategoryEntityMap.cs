using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperMarket.Entities;

namespace SuperMarket.Persistence.EF.Categories
{
    public class CategoryEntityMap :IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> _)
        {
            _.ToTable("Categories");

            _.HasKey(_ => _.Id);

            _.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            _.Property(_ => _.Name)
                .HasMaxLength(50).IsRequired();

            

        }
    }
}
