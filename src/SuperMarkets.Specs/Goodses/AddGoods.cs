using System.Linq;
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
    public class AddGoods : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly GoodsRepository _goodsRepository;
        private readonly GoodsService _sut;
        private readonly EFUnitOfWork _unitOfWork;
        private Goods _goods;
        private AddGoodsDto _addGoodsDto;
        private Category _category;
        private readonly CategoryRepository _categoryRepository;

        public AddGoods(ConfigurationFixture configuration) : base(configuration)
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

        [And("هیچ کالایی در دسته بندی 'لبنیات' وجود ندارد")]
        public void GivenAnd()
        {

        }

        [When("کالایی با عنوان ‘ماست رامک’  با قیمت فروش’۲۰۰۰’  با کد کالا انحصاری’YR-190’   با موجودی ‘۱۰’  تعریف می کنمم")]
        public void When()
        {
            _addGoodsDto = new AddGoodsDto()
            {
                Name = "ماست رامک",
                SalesPrice = 2000,
                MinimumInventory = 5,
                Count = 10,
                UniqueCode = "YR-190",
                CategoryId = _category.Id
            };
            _sut.Add(_addGoodsDto);
        }
        
        [Then("کالایی با عنوان ‘ماست رامک’  با قیمت فروش’۲۰۰۰’  با کد کالا انحصاری’YR-190’   با موجودی ‘۱۰’  در فهرست دسته بندی کالا باید وجود داشته باشد")]
        public void Then()
        {
            _context.Goods.Count().Should().Be(1);
            _context.Goods.Should().Contain(_ => _.Name == _addGoodsDto.Name);
            _context.Goods.Should().Contain(_ => _.CategoryId == _addGoodsDto.CategoryId);
            _context.Goods.Should().Contain(_ => _.Count == _addGoodsDto.Count);
            _context.Goods.Should().Contain(_ => _.UniqueCode == _addGoodsDto.UniqueCode);
            _context.Goods.Should().Contain(_ => _.SalesPrice == _addGoodsDto.SalesPrice);
            _context.Goods.Should().Contain(_ => _.MinimumInventory == _addGoodsDto.MinimumInventory);
        }

        [Fact]
        public void Run()
        {
            Given();
            GivenAnd();
            When();
            Then();
        }
    }
}