using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.Categories;
using SuperMarket.Persistence.EF.Goodses;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.Goodses;
using SuperMarket.Services.Goodses.Contracts;
using SuperMarkets.Specs.Infrastructure;
using Xunit;
using static SuperMarkets.Specs.BDDHelper;

namespace SuperMarkets.Specs.Goodses
{
    [Scenario("مدیریت کالا")]
    [Feature("",
        AsA = "فروشنده ",
        IWantTo = " فروش کالا ",
        InOrderTo = "مدیریت کنم"
    )]
    public class GetGoods : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly GoodsRepository _goodsRepository;
        private readonly GoodsService _sut;
        private readonly EFUnitOfWork _unitOfWork;
        private readonly CategoryRepository _categoryRepository;
        private Goods _goods;
        private Category _category;
        private UpdateCategoryDto _updateCategoryDto;
        private AddGoodsDto _addGoodsDto;
        public GetGoods(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _goodsRepository = new EFGoodsRepository(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new GoodsAppService(_unitOfWork, _goodsRepository, _categoryRepository);
        }

        [Given("دسته بندی کالا با عنوان ‘لبنیات ‘  تعریف می کنیم")]
        public void Given()
        {
            _category = new Category()
            {
                Name = "لبنیات"

            };
            _context.Manipulate(_ => _context.Categories.Add(_category));
        }

        [And("کالایی با عنوان ‘ماست رامک’  با قیمت فروش’۲۰۰۰’  با کد کالا انحصاری’YR-190’   با موجودی ‘۱۰’  تعریف می کنم")]
        public void GivenAnd()
        {
            _goods = new Goods
            {
                Name = "ماست  رامک",
                SalesPrice = 2000,
                MinimumInventory = 5,
                Count = 10,
                UniqueCode = "YK-190",
                CategoryId = _category.Id
            };
            _context.Manipulate(_ => _context.Goods.Add(_goods));
        }

        [When("درخواست نمایش تمام کالای های موجود در دسته بندی را می کنم")]
        public void When()
        {
            _sut.GetAll();
        }

        [Then("تنها کالایی با عنوان ‘ماست رامک’  با قیمت فروش’۲۰۰۰’  با کد کالا انحصاری’YR-190’با موجودی ‘۱۰’   جهت نمایش در فهرست کالا وجود داشته باشد")]
        public void Then()
        {
            var expected = _sut.GetAll();

            expected.Should().HaveCount(0);
            expected.Should().Contain(_ => _.Name == "ماست  رامک");
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given()
                , _ => When()
                , _ => Then());
        }
    }
}