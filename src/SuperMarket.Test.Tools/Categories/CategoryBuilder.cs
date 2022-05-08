using SuperMarket.Entities;

namespace SuperMarket.Test.Tools.Categories
{
    public class CategoryBuilder
    {
        private Category category;

        public CategoryBuilder()
        {
            category = new Category
            {
                Name = "لبنیات"
            };
        }

        public Category Build()
        {
            return category;
        }
    }
}
