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
using SuperMarkets.Specs.Infrastructure;
using Xunit;
using static SuperMarkets.Specs.BDDHelper;

namespace SuperMarkets.Specs.Categories
{
    [Scenario("مدیریت دسته بندی")]
    [Feature("",
        AsA = "فروشنده ",
        IWantTo = "دسته بندی کالا مدیریت کنم",
        InOrderTo = "کالا های خود را دسته بندی کنم"
    )]
    public class DeleteCategoryWithGoods :EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryRepository _categoryRepository;
        private readonly CategoryService _sut;
        private Category _category;
        private AddCategoryDto _addCategoryDto;
        Action expected;
        
        public DeleteCategoryWithGoods(ConfigurationFixture configuration) : base(configuration)
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

        [And("کالایی با عنوان 'ماست کاله'و قیمت'5000' و تعداد '5' در دسته بندی 'لبنیات' وجود دارد")]
        public void GivenAnd()
        {
           //after Goods are added in project then  here completed but now Skipped
        }

        [When("دسته بندی با عنوان 'لبنیات' را حذف میکنیم")]
        public void When()
        {
            expected = () => _sut.Delete(_category.Id);
        }

        [Then("دسته بندی با عنوان 'لبنیات'در فهرست دسته بندی کالا باید وجود داشته باشد")]
        public void Then()
        {
            _context.Categories.Count().Should().Be(1);
            _context.Categories.Should().Contain(_ => _.Id == _category.Id);
        }

        [And("خطایی با عنوان 'کالا در این دسته بندی وجود دارد' باید ارسال شود")]
        public void ThenAnd()
        {
            expected.Should().ThrowExactly<ThisCategoryHasGoodsException>();
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
