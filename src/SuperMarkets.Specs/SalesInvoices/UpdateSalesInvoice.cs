using System;
using System.Linq;
using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.Categories;
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
    public class UpdateSalesInvoice : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly SalesInvoiceRepository _salesInvoiceRepository;
        private readonly SalesInvoiceService _sut;
        private Category _category;
        private Goods _goods;
        private SalesInvoice _salesInvoice;
        private UpdateSalesInvoiceDto _updateSalesInvoiceDto;

        private readonly CategoryRepository _categoryRepository;
        private readonly GoodsRepository _goodsRepository;
        
        public UpdateSalesInvoice(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _salesInvoiceRepository = new EFSalesInvoiceRepository(_context);
            _sut = new SalesInvoiceAppService(_unitOfWork, _salesInvoiceRepository,  _goodsRepository);
            _categoryRepository = new EFCategoryRepository(_context);
        }

        [Given("دسته بندی کالا با عنوان ‘لبنیات ‘  تعریف می کنیم")]
        public void Given()
        {   
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));
        }

        [And("کالایی با عنوان ‘ماست رامک’  با قیمت فروش ‘۲۰۰۰’  با کد کالا انحصاری’YR-190’ با موجودی ‘۱۰’  تعریف می کنم")]
        public void GivenFirstAnd()
        {
            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));
        }
        
        [And("کالایی با کد ‘1’  با قیمت فروش’۲۰۰۰’  در تاریخ ‘ 01/01/1400‘ با تعداد ‘۲’  می فروشیم")]
        public void GivenSecondAnd()
        {
            _salesInvoice = new SalesInvoice
            {
                CustomerName = "Saeed Ansari",
                SalesDate = DateTime.Now.Date,
                SalesPrice = 2000,
                GoodsId = _goods.Id,
                Count = 2
            };
            _context.Manipulate(_ => _.SalesInvoices.Add(_salesInvoice));
        }
        
        [When("کالایی با کد ‘1’  با قیمت فروش’۲۰۰۰’  در تاریخ ‘ 01/01/1400‘ با تعداد ‘4’  ویرایش می کنیم")]
        public void When()
        {
            _updateSalesInvoiceDto = new UpdateSalesInvoiceDto
            {
                CustomerName = "Saeed Ansari",
                SalesDate = DateTime.Now.Date,
                SalesPrice = 2000,
                GoodsId = _goods.Id,
                Count = 4
            };
           var a= _salesInvoiceRepository.FindById(_salesInvoice.Id);
            _sut.Update(a.Id,_updateSalesInvoiceDto);
        }

        [Then(" فروش کالایی با کد ‘1’  با قیمت فروش’۲۰۰۰’  در تاریخ ‘ 01/01/1400‘ با تعداد ‘۲’  در لیست فروش قرار دارد")]
        public void Then()
        {
            _context.SalesInvoices.Any(_ => _.GoodsId == _goods.Id
                                        && _.Count == _updateSalesInvoiceDto.Count).Should().BeTrue();
        }

        [And(" تنها کالایی با کد ‘1’  با قیمت فروش’۲۰۰۰’  در تاریخ ‘ 01/01/1400‘ با تعداد ‘۲’  می فروشیم")]
        public void ThenAnd()
        {
            _context.SalesInvoices
                .Should().Contain(_ => _.GoodsId == _goods.Id && _.Count == _updateSalesInvoiceDto.Count);
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given()
                ,_=>GivenFirstAnd()
                , _ => GivenSecondAnd()
                , _ => When()
                , _ => Then()
                , _ => ThenAnd());
        }
    }
} 