using System;
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
using SuperMarket.Services.Goodses.Exceptions;
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
    public class UpdateGoodsWithDuplicateName : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly GoodsRepository _goodsRepository;
        private readonly GoodsService _sut;
        private readonly EFUnitOfWork _unitOfWork;
        private readonly CategoryRepository _categoryRepository;
        private Goods _goods;
        private Goods _secondGoods;
        private Category _category;
        private UpdateGoodsDto _updateGoodsDto;
        private AddGoodsDto _addGoodsDto;
        private Action expected;
        public UpdateGoodsWithDuplicateName(ConfigurationFixture configuration) : base(configuration)
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
            CreateOneCategory();
        }

        [And("کالایی با عنوان ‘ماست رامک’  با قیمت فروش’۲۰۰۰’  با کد کالا انحصاری’YR-190’   با موجودی ‘۱۰’  تعریف می کنم")]
        public void GivenFirstAnd()
        {
           CreateGoods();
        }

        [And("کالایی با عنوان ‘ماست قنبرزاده  با قیمت فروش’۳۰۰۰’  با کد کالا انحصاری’YR-191’   با موجودی ‘۱۵’  تعریف می کنم")]
        public void GivenSecondAnd()
        {
            CreateSecondGoods();
        }
        
        [When("کد کالا انحصاری’YR-191’   با قیمت فروش’۴۰۰۰’  با عنوان ‘ماست رامک’    با موجودی ‘۱۰’  ویرایش می کنم")]
        public void When()
        {
            CreateUpdateGoodsDto();
            expected = () => _sut.Update(_secondGoods.Id, _updateGoodsDto);
        }
        
        [Then("کالایی با عنوان ‘ماست قنبرزاده  با قیمت فروش’۳۰۰۰’  با کد کالا انحصاری’YR-191’   با موجودی ‘۱۵’ باید وجود داشته باشد")]
        public void Then()
        {
            _context.Goods.Where(_ => _.Name == _updateGoodsDto.Name).Should().HaveCount(1);
        }

        [And("خطایی با عنوان ‘عنوان کالا تکراری می باشد’ باید رخ دهد")]
        public void ThenAnd()
        {
            expected.Should().ThrowExactly<DuplicateGoodsNameInCategoryException>();
        }

        [Fact]
        public void Run()
        {
            Given();
            GivenFirstAnd();
            GivenSecondAnd();
            When();
            Then();
            ThenAnd();
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

        private void CreateSecondGoods()
        {
            _secondGoods = new Goods
            {
                Name = "ماست قنبرزاده",
                SalesPrice = 3000,
                MinimumInventory = 5,
                Count = 15,
                UniqueCode = "YK-191",
                CategoryId = _category.Id
            };
            _context.Manipulate(_ => _context.Goods.Add(_secondGoods));
        }
        private void CreateUpdateGoodsDto()
        {
            _updateGoodsDto = new UpdateGoodsDto
            {
                Name = "ماست قنبرزاده",
                SalesPrice = 3000,
                MinimumInventory = 5,
                Count = 15,
                UniqueCode = "YK-191",
                CategoryId = _category.Id
            };
        }
    }
}