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
    [Scenario("تعریف دسته بندی")]
    [Feature("",
        AsA = "فروشنده ",
        IWantTo = " دسته بندی کالا را مدیریت کنم  ",
        InOrderTo = "کالاهای خود را دسته بندی کنم"
    )]
    public class UpdateCategoryWithDuplicatedName:EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly CategoryRepository _categoryRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryService _sut;
        private Category _category;
        private UpdateCategoryDto _updateCategoryDto;
        Action expected;

        
        public UpdateCategoryWithDuplicatedName(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new CategoryAppService(_unitOfWork, _categoryRepository);
        }
        [Given("دسته بندی با عنوان 'لبنیات'در فهرست دسته بندی کالا وجود دارد")]
        public void Given()
        {
            _category = new Category
            {
                Name = "لبنیات"
            };
            _context.Manipulate(_ => _.Categories.Add(_category));
        }

        [Given("دسته بندی با عنوان 'لبنیات'در فهرست دسته بندی کالا وجود دارد")]
        public void GivenAnd()
        {
            _category = new Category
            {
                Name = "لبنیات"
            };

            _context.Manipulate(_ => _.Categories.Add(_category));
        }

        [When("دسته بندی با عنوان 'لبنیات' را به 'خشکبار'ویرایش میکنم")]
        public void When()
        {
            _updateCategoryDto = new UpdateCategoryDto
            {
                Name = "لبنیات"
            };

            expected = () => _sut.Update(_category.Id, _updateCategoryDto);
        }
        [Then("یک دسته بندی با عنوان 'لبنیات' باید در فهرست دسته بندی ها وجود داشته باشد")]
        public void Then()
        {
            _context.Categories
                .Should().Contain(_ => _.Name == _category.Name);
        }

        [And("خطایی با عنوان 'عنوان دسته بندی تکراری است' باید ارسال شود")]
        public void ThenAnd()
        {
            expected.Should().ThrowExactly<CategoryNameIsExistException>();
        }

        [Fact]
        public void Run()
        {
            Given();
            GivenAnd();
            When();
            Then();
            ThenAnd();
        }
    }
}
