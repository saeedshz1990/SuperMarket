using System;
using System.Linq;
using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.Categories;
using SuperMarket.Services.Categories;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.Categories.Exceptions;
using Xunit;

namespace SuperMarket.Services.Test.Unit.Categories
{
    public class CategoryServiceTests
    {
        private readonly EFDataContext _context;
        private readonly CategoryRepository _categoryRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryService _sut;
        private AddCategoryDto _dto;
        private Category _category;
        private GetCategoryDto _getCategoryDto;
        private UpdateCategoryDto _updateCategoryDto;

        public CategoryServiceTests()
        {
            _context = new EFInMemoryDatabase().CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new CategoryAppService(_unitOfWork, _categoryRepository);
        }

        [Fact]
        public void Add_adds_Category_Properly()
        {
            var dto = new AddCategoryDto
            {
                Name = "Test"

            };
            _sut.Add(dto);

            var expected = _context.Categories.FirstOrDefault();

            expected.Name.Should().Be(dto.Name);
        }

        [Fact]
        public void Add_ThrowException_When_NameIsDuplicate()
        {

            var category = new Category
            {
                Name = "Test"
            };
            _context.Manipulate(_ => _.Categories.Add(category));

            var dto = new AddCategoryDto
            {
                Name = "Test1"
            };
            _sut.Add(dto);

            Action expected = () => _sut.Add(dto);
            expected.Should().ThrowExactly<CategoryNameIsExistException>();

        }


        [Fact]
        public void Get_gets_OneCategory_properly()
        {
            _category = new Category
            {
                Name = "Test"
            };
            _context.Manipulate(_ => _.Categories.Add(_category));

            var expected = _sut.GetById(_category.Id);

            expected.Name.Should().Be(_category.Name);
        }

        [Fact]
        public void GetAll_returns_all_categories_Properly()
        {
            _category = new Category
            {
                Name = "Test"
            };

            _context.Manipulate(_ => _.Categories.Add(_category));
            var expected = _sut.GetAll();
            expected.Should().Contain(_ => _.Name == _category.Name);
        }

        [Fact]
        public void Delete_deletes_Category_Properly()
        {
            _category = new Category
            {
                Name = "Test"
            };
            _context.Manipulate(_ => _.Categories.Add(_category));
            _sut.Delete(_category.Id);

            _context.Categories.Count().Should().Be(0);
        }

        [Fact]
        public void Delete_ThrowException_When_CategoryIsNotFound()
        {
            var fakeCategoryId = 200;
            _category = new Category
            {
                Name = "Test"
            };
            _context.Manipulate(_ => _.Categories.Add(_category));

            Action expected = () => _sut.Delete(fakeCategoryId);
            expected.Should().ThrowExactly<CategoryNotFoundException>();
        }

        [Fact]
        public void Update_updates_category_Properly()
        {
            _category = new Category
            {
                Name = "Test"
            };
            _context.Manipulate(_ => _.Categories.Add(_category));

            _updateCategoryDto = new UpdateCategoryDto
            {
                Name = "Updated Test"
            };

            _sut.Update(_category.Id, _updateCategoryDto);


            _context.Categories.Should().Contain(_ => _.Name == _updateCategoryDto.Name);
        }

        [Fact]
        public void Update_ThrowException_When_CategoryNameIsExist()
        {
            var fakeCategoryId = 200;
            _category = new Category
            {
                Name = "Test"
            };
            _context.Manipulate(_ => _.Categories.Add(_category));

            _updateCategoryDto = new UpdateCategoryDto
            {
                Name = "Test"
            };

            Action expected = () => _sut.Update(_category.Id, _updateCategoryDto);
            expected.Should().ThrowExactly<CategoryNameIsExistException>();
        }
        
        
    }
}
