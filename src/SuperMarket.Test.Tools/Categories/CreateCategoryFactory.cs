using SuperMarket.Entities;
using SuperMarket.Services.Categories.Contracts;

namespace SuperMarket.Test.Tools.Categories
{
    public static class CreateCategoryFactory
    {
        public static Category CreateCategoryDto(string name)
        {
            return new Category
            {
                Name = name
            };
        }

        public static AddCategoryDto CreateAddCategoryDto(string name)
        {
            return new AddCategoryDto
            {
                Name = name
            };
        }

        public static UpdateCategoryDto CreateUpdateCategoryDto(string name)
        {
            return new UpdateCategoryDto
            {
                Name = name
            };
        }

        public static GetCategoryDto CreateGetCategoryDto(string name)
        {
            return new GetCategoryDto
            {
                Name = name
            };
        }

    }
}
