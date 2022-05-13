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
using System.Linq;
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
    public class UpdateGoods : EFDataContextDatabaseFixture
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
        private UpdateGoodsDto _updateGoodsDto;

        public UpdateGoods(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _goodsRepository = new EFGoodsRepository(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new GoodsAppService(_unitOfWork, _goodsRepository, _categoryRepository);
        }

        [Given("کالایی با عنوان 'ماست رامک'در فهرست کالا وجود دارد")]
        public void Given()
        {
            CreateOneCategory();
        }
        
        [And("کالایی با عنوان ‘ماست رامک’  با قیمت فروش’۲۰۰۰’  با کد کالا انحصاری’YR-190’   با موجودی ‘۱۰’  تعریف می کنم")]
        public void GivenAnd()
        {
            CreateGoods();
        }
        
        [When("کد کالا انحصاری’YR-190’با قیمت فروش’4۰۰۰’  با عنوان ‘ماست رامک’  با موجودی ‘۱۰’  ویرایش می کنم")]
        public void When()
        {
            CreateUpdatedGoodsDto();
            _sut.Update(_goods.Id, _updateGoodsDto);
        }
        
        [Then("تنها کالایی با عنوان ‘ماست رامک’  با قیمت فروش’۴۰۰۰’  با کد کالا انحصاری’YR-190’  با موجودی ‘۱۰’  باید وجود داشته باشد")]
        public void Then()
        {
            var expected = _context.Goods.Any(_ => _.UniqueCode == "YK-141" && _.Name == "ماست کاله");
            expected.Should().BeFalse();
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given()
                , _ => GivenAnd()
                , _ => When()
                , _ => Then()
            );
        }

        private void CreateOneCategory()
        {
            _category = new Category()
            {
                Name = "لبنیات"
            };
            _context.Manipulate(_ => _context.Categories.Add(_category));
        }
        
        private void CreateGoods()
        {
            _goods = new Goods
            {
                Name = "ماست رامک",
                SalesPrice = 2000,
                MinimumInventory = 5,
                Count = 10,
                UniqueCode = "YK-190",
                CategoryId = _category.Id
            };
            _context.Manipulate(_ => _context.Goods.Add(_goods));
        }

        private void CreateUpdatedGoodsDto()
        {
            _updateGoodsDto = new UpdateGoodsDto
            {
                Name = "ماست کاله",
                SalesPrice = 4000,
                MinimumInventory = 5,
                Count = 10,
                UniqueCode = "YK-191",
                CategoryId = _category.Id
            };
        }
    }
}