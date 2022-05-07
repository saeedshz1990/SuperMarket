using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;

namespace SuperMarket.Services.Categories.Contracts
{
    public interface CategoryRepository :Repository
    {
        void Add(Category category);
        bool IsCategoryExist(string dtoname);
        Category GetById(int id);
        IList<GetCategoryDto> GetAll();
        void Delete(int id);
        Category FindById(int id);
        Category FindByName(string name);
        void Update(int id, Category category);
        bool FindCategoryById(int id);
    }
}
