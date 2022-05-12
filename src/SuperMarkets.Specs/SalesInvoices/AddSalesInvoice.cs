using System;
using System.Linq;
using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.Categories;
using SuperMarket.Persistence.EF.Goodses;
using SuperMarket.Persistence.EF.SalesInvoices;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.Goodses.Contracts;
using SuperMarket.Services.SalesInvoices;
using SuperMarket.Services.SalesInvoices.Contracts;
using SuperMarket.Test.Tools.Categories;
using SuperMarket.Test.Tools.Goodses;
using SuperMarkets.Specs.Infrastructure;
using Xunit;
using static SuperMarkets.Specs.BDDHelper;

namespace SuperMarkets.Specs.SalesInvoices
{
    [Scenario("مدیریت سند فروش")]
    [Feature("",
        AsA = "فروشنده ",
        IWantTo = " اسناد ورود کالا",
        InOrderTo = "مدیریت کنم"
    )]
    public class AddSalesInvoice : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly SalesInvoiceRepository _salesInvoiceRepository;
        private readonly SalesInvoiceService _sut;
        private Category _category;
        private Goods _goods;
        private SalesInvoice _salesInvoice;
        private AddSalesInvoiceDto _addSalesInvoiceDto;

        private readonly CategoryRepository _categoryRepository;
        private readonly GoodsRepository _goodsRepository;
        public AddSalesInvoice(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _salesInvoiceRepository = new EFSalesInvoiceRepository(_context);
            _sut = new SalesInvoiceAppService(_unitOfWork, _salesInvoiceRepository, _goodsRepository);
            _categoryRepository = new EFCategoryRepository(_context);
            _goodsRepository = new EFGoodsRepository(_context);
        }

        [Given("دسته بندی کالا با عنوان ‘لبنیات ‘  تعریف می کنیم")]
        public void Given()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));
        }

        [And("کالایی با عنوان ‘ماست رامک’  با قیمت فروش ‘۲۰۰۰’  با کد کالا انحصاری’YR-190’ با موجودی ‘۱۰’  تعریف می کنم")]
        public void GivenAnd()
        {
            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));
        }

        [When("فرض می کنیم : کالایی با کد ‘1’  با قیمت فروش’۲۰۰۰’  در تاریخ ‘ 01/01/1400‘ با تعداد ‘۲’  می فروشیم")]
        public void When()
        {
            _addSalesInvoiceDto = new AddSalesInvoiceDto
            {
                CustomerName = "Saeed Ansari",
                SalesDate = DateTime.Now.Date,
                SalesPrice = 2000,
                GoodsId = _goods.Id,
                Count = 3
            };
            _sut.Add(_addSalesInvoiceDto);
        }
        
        [Then(": فروش کالایی با کد ‘1’  با قیمت فروش’۲۰۰۰’  در تاریخ ‘ 01/01/1400‘ با تعداد ‘۲’  در لیست فروش قرار دارد")]
        public void Then()
        {
            _context.SalesInvoices.Count(_ => _.GoodsId == _goods.Id && _.Count == _goods.Count);
        }

        [When("تنها کالایی با کد ‘1’  با قیمت فروش’۲۰۰۰’  در تاریخ ‘ 01/01/1400‘ با تعداد ‘۲’  می فروشیم")]
        public void ThenAnd()
        {
            _context.SalesInvoices.Should().HaveCount(1);
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given()
                , _ => GivenAnd()
                , _ => When()
                , _ => Then()
                , _ => ThenAnd());

        }
    }
}