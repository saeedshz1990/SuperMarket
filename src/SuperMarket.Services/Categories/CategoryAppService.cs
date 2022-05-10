using System.Collections.Generic;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.Categories.Exceptions;

namespace SuperMarket.Services.Categories
{
    public class CategoryAppService : CategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly UnitOfWork _unitOfWork;

        public CategoryAppService(UnitOfWork unitOfWork, CategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public void Add(AddCategoryDto dto)
        {
            bool isCategoryNameExist = _categoryRepository
                .IsCategoryExist(dto.Name);

            if (isCategoryNameExist)
            {
                throw new CategoryNameIsExistException();
            }

            var category = new Category
            {
                Name = dto.Name,
            };

            _categoryRepository.Add(category);
            _unitOfWork.Commit();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public IList<GetCategoryDto> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public void Delete(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category==null)
            {
                throw new CategoryNotFoundException();
            }
            _categoryRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public void Update(int id, UpdateCategoryDto dto)
        {
            var category = _categoryRepository.FindById(id);
            var isCategoryNameExist = _categoryRepository
                .IsCategoryExist(dto.Name);

            if (!isCategoryNameExist)
            {
                throw new CategoryNameIsExistException();
            }

            category.Name = dto.Name;
            _unitOfWork.Commit();
        }
    }
}
