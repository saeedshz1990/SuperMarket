using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;

namespace SuperMarket.Services.Categories.Contracts
{
    public interface CategoryService :Service
    {
        void Add(AddCategoryDto dto);
        Category GetById(int id);
        IList<GetCategoryDto> GetAll();
        void Delete(int id);
        void Update(int id, UpdateCategoryDto dto);
    }
}
