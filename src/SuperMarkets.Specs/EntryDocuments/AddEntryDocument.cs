using System;
using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.Categories;
using SuperMarket.Persistence.EF.EntryDocuments;
using SuperMarket.Persistence.EF.Goodses;
using SuperMarket.Services.Categories;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.EntryDocuments;
using SuperMarket.Services.EntryDocuments.Contracts;
using SuperMarket.Services.Goodses;
using SuperMarket.Services.Goodses.Contracts;
using SuperMarkets.Specs.Infrastructure;
using Xunit;
using static SuperMarkets.Specs.BDDHelper;

namespace SuperMarkets.Specs.EntryDocuments
{
    [Scenario("مدیریت سند فروش")]
    [Feature("",
        AsA = "فروشنده ",
        IWantTo = " اسناد ورود کالا",
        InOrderTo = "مدیریت کنم"
    )]
    public class AddEntryDocument : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly EntryDocumentRepository _entryDocumentRepository;
        private readonly EntryDocumentService _sut;

        private readonly GoodsRepository _goodsRepository;
        private readonly GoodsService _goodsService;

        private readonly CategoryRepository _categoryRepository;
        private readonly CategoryService _categoryService;

        private Category _category;
        private Goods _goods;
        private EntryDocument _entryDocument;
        private AddGoodsDto _addGoodsDto;
        Action expected;

        public AddEntryDocument(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _entryDocumentRepository = new EFEntryDocumentRepository(_context);
            _sut = new EntryDocumentAppservice(_unitOfWork, _entryDocumentRepository);
            _goodsRepository = new EFGoodsRepository(_context);
            _goodsService = new GoodsAppService(_unitOfWork, _goodsRepository, _categoryRepository);
            _categoryRepository = new EFCategoryRepository(_context);
            _categoryService = new CategoryAppService(_unitOfWork, _categoryRepository);
        }
        
        [Given("دسته بندی کالا با عنوان ‘لبنیات ‘  تعریف می کنیم")]
        public void Given()
        {
            _category = new Category { Name = "لبنیات" };
            _context.Manipulate(_ => _.Categories.Add(_category));
        }

        [And("کالایی با عنوان ‘ماست رامک’  با قیمت خرید’۲۰۰۰’  با کد کالا انحصاری’YR-190’   با موجودی ‘۱۰’  تعریف می کنم")]
        public void GivenAnd()
        {
            _addGoodsDto = new AddGoodsDto
            {
                Name = "ماست رامک",
                CategoryId = 1,
                Count = 10,
                SalesPrice = 2000,
                UniqueCode = "YR-190",
                MinimumInventory = 5,
            };
            _goodsService.Add(_addGoodsDto);
        }

        [When("کالایی با کد ‘۱۰۰’  با قیمت خرید ‘۱۰۰۰’  با موجودی ‘۷’ درتاریخ ‘ 01/01/1400‘ وارد میکنم")]
        public void When()
        {
            _entryDocument = new EntryDocument
            {
                GoodsId = _goods.Id,
                DateBuy = DateTime.Now.Date,
                GoodsCount = 7 + _goods.Count
            };
            _context.Manipulate(_ => _.Goods.Add(_goods));
        }
        
        [Then("سند کالایی با عنوان ‘ماست رامک’  با قیمت خرید ‘۱۰۰۰’  با موجودی ‘۷’ درتاریخ ‘ 01/01/1400‘ باید وجود داشته باشد")]
        public void Then()
        {
            _context.EntryDocuments.Should().Contain(_ => _.GoodsId == _goods.Id);
            _context.EntryDocuments.Should().Contain(_ => _.BuyPrice == _entryDocument.BuyPrice);
            _context.EntryDocuments.Should().Contain(_ => _.DateBuy == _entryDocument.DateBuy.Date);
            _context.EntryDocuments.Should().Contain(_ => _.GoodsCount == _goods.Count + _entryDocument.GoodsCount);
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
