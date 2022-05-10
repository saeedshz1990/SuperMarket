using System.Collections.Generic;
using System.Linq;
using SuperMarket.Entities;
using SuperMarket.Services.Categories.Contracts;

namespace SuperMarket.Persistence.EF.Categories
{
    public class EFCategoryRepository : CategoryRepository
    {
        private readonly EFDataContext _context;

        public EFCategoryRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Add(category);
        }

        public bool IsCategoryExist(string dtoname)
        {
            var name = _context
                .Categories
                .Any(_ => _.Name == dtoname);

            return name;
        }

        public Category GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(_ => _.Id == id);
            return category;
        }

        public IList<GetCategoryDto> GetAll()
        {
            return _context.Categories.Select(_ => new GetCategoryDto
            {
                Id = _.Id,
                Name = _.Name
            }).ToList();

        }

        public void Delete(int id)
        {
            _context.Remove(FindById(id));
        }

        public Category FindById(int id)
        {
            return _context.Categories.FirstOrDefault(_ => _.Id == id);
        }

        public Category FindByName(string name)
        {
            return _context.Categories.FirstOrDefault(_ => _.Name == name);
        }

        public void Update(int id, Category category)
        {


        }

        public bool FindCategoryById(int id)
        {
            return _context.Categories.Any(_ => _.Id == id);
        }

       
    }
}
