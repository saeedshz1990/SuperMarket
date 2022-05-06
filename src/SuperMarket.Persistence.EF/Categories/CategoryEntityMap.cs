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

            _.HasKey(x => x.Id);

            _.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            _.Property(x => x.Name)
                .HasMaxLength(50).IsRequired();

            

        }
    }
}
