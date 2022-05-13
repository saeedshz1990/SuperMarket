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
    public class DeleteCategoryWithGoods : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryRepository _categoryRepository;
        private readonly CategoryService _sut;
        private Category _category;
        private AddCategoryDto _addCategoryDto;
        Action expected;
        private Goods _goods;

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
            CreateCategory();
        }

        [And("کالایی با عنوان ‘ماست رامک’  با قیمت فروش’۲۰۰۰’  با کد کالا انحصاری’YR-190’   با موجودی ‘۱۰’ در دسته بندی کالا موجود می باشد")]
        public void GivenAnd()
        {
            CreateGoods();
        }

        [When("دسته بندی با عنوان 'لبنیات' را حذف میکنیم")]
        public void When()
        {
            expected = () => _sut.Delete(_category.Id);
        }

        [Then("دسته بندی با عنوان 'لبنیات'در فهرست دسته بندی کالا باید وجود داشته باشد")]
        public void Then()
        {
            _context.Categories.Should().HaveCount(1);
        }

        [And("خطایی با عنوان ‘امکان حذف بدلیل وجود کالا در این دسته امکان پذیر نمی باشد’  باید رخ دهد")]
        public void ThenAnd()
        {
            expected.Should().ThrowExactly<CategoryHasGoodsException>();
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

        private void CreateCategory()
        {
            _category = new Category
            {
                Name = "لبنیات"
            };

            _context.Manipulate(_ => _.Categories.Add(_category));
        }

        private void CreateGoods()
        {
            _goods = new Goods
            {
                Name = "ماست رامک",
                SalesPrice = 2000,
                MinimumInventory = 5,
                Count = 10,
                UniqueCode = "YR-190",
                CategoryId = _category.Id,
            };
            _context.Manipulate(_ => _.Goods.Add(_goods));
        }
    }
}