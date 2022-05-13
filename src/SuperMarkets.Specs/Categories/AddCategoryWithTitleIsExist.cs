using System;
using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.Categories;
using SuperMarket.Services.Categories;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.Categories.Exceptions;
using SuperMarkets.Specs.Infrastructure;
using Xunit;
using static SuperMarkets.Specs.BDDHelper;

namespace SuperMarkets.Specs.Categories
{
    [Scenario("مدیریت دسته بندی")]
    [Feature("",
        AsA = "فروشنده ",
        IWantTo = " دسته بندی کالا   ",
        InOrderTo = "مدیریت دسته بندی"
    )]
    public class AddCategoryWithTitleIsExist : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly CategoryRepository _categoryRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryService _sut;
        private AddCategoryDto _dto;
        private Category _firstCategory;
        private Category _secondCategory;
        Action expected;
        public AddCategoryWithTitleIsExist(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new CategoryAppService(_unitOfWork, _categoryRepository);
        }

        [Given("یک دسته بندی با عنوان 'لبنیات'در فهرست دسته بندی کالا وجود دارد")]
        public void Given()
        {
            CreateCategory();
        }
        
        [When("دسته بندی جدیدی با عنوان ‘لبنیات’ تعریف می کنم")]
        public void When()
        {
            CreateCategoryDto();
            expected = () => _sut.Add(_dto);
        }

        [Then("تنها یک دسته بندی با عنوان ' لبنیات' باید در فهرست دسته بندی کالا وجود داشته باشد")]
        public void Then()
        {
            _context.Categories.Should().HaveCount(1);
            _context.Categories
                .Should().Contain(_ => _.Name == _dto.Name);
        }
        
        [And(": خطایی با عنوان 'عنوان دسته بندی کالا تکراریست ' باید رخ دهد")]
        public void ThenAnd()
        {
            expected.Should().ThrowExactly<CategoryNameIsExistException>();
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given()
                , _ => When()
                , _ => Then()
                , _ => ThenAnd());
        }

        private void CreateCategoryDto()
        {
            _dto = new AddCategoryDto
            {
                Name = "لبنیات"
            };
        }
        
        private void CreateCategory()
        {
            _firstCategory = new Category
            {
                Name = "لبنیات"
            };
            _context.Manipulate(_ => _.Categories.Add(_firstCategory));
        }
    }
}
