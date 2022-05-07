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
    public class GetAllEntryDocument : EFDataContextDatabaseFixture
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
        private AddEntryDocumentDto _addEntryDocumentDto;

        public GetAllEntryDocument(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _entryDocumentRepository = new EFEntryDocumentRepository(_context);
            _sut = new EntryDocumentAppservice(_unitOfWork, _entryDocumentRepository, _goodsRepository);
            _goodsRepository = new EFGoodsRepository(_context);
            _goodsService = new GoodsAppService(_unitOfWork, _goodsRepository,_categoryRepository);
            _categoryRepository = new EFCategoryRepository(_context);
            _categoryService = new CategoryAppService(_unitOfWork, _categoryRepository);
        }

        [Given("کالایی با کد '01' در فهرست کالا ها تعریف شده است و هیچ کالایی موجود 	نیست")]
        public void Given()
        {
            _category = new Category { Name = "خشکبار" };
            _context.Manipulate(_ => _.Categories.Add(_category));
        }

        [And("ایجاد کالا")]
        public void GivenFirstAnd()
        {
            _goods = new Goods
            {
                Name = "ماست موسیر",
                CategoryId = _category.Id,
                Count = 5,
                SalesPrice = 1890,
                UniqueCode = "kj-313",
                MinimumInventory = 5,
            };
            _context.Manipulate(_ => _.Goods.Add(_goods));
        }

        [When("ورودی کالای کد '01' را به تعداد '5' و قیمت 100 وارد می کنیم")]
        public void GivenSecondAnd()
        {
            _addEntryDocumentDto = new AddEntryDocumentDto
            {
                GoodsId = _goods.Id,
                BuyPrice = 2000,
                DateBuy = DateTime.Now.Date,
                GoodsCount = 5
            };
            _sut.Add(_addEntryDocumentDto);
            //_context.Manipulate(_ => _.EntryDocuments.Add(_entryDocument));
        }

        [When("میخواهیم لیست تمامی ورودی ها را دریافت کنیم")]
        public void When()
        {
            _sut.GetAll();
        }
        
        [Then("لیست تمامی ورودی ها باید به شرح زیر باشد")]
        public void Then()
        {
            _context.EntryDocuments.Should().HaveCount(1);
            //_context.EntryDocuments
            //    .Should()
            //    .Contain(_ => _.GoodsId == _goods.Id
            //                   && _.BuyPrice == _entryDocument.BuyPrice
            //                   && _.DateBuy == _entryDocument.DateBuy
            //                   && _.GoodsCount == _entryDocument.GoodsCount);
        }

        [Fact]
        public void Run()
        {
            Given();
            GivenFirstAnd();
            GivenSecondAnd();
            When();
            Then();
            
        }        
    }
}
