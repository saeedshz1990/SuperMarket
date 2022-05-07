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

        [Given("ورودی کالا با کد '01' وفروش 1 از 6 عدد کالا در فهرست ورودی کالا ها موجود است")]
        public void Given()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));

            _salesInvoice = new SalesInvoice
            {
                CustomerName = "Saeed Ansari",
                SalesDate = DateTime.Now.Date,
                SalesPrice = 2000,
                GoodsId = _goods.Id,
                Count = 3
            };
            _context.Manipulate(_ => _.SalesInvoices.Add(_salesInvoice));
        }
        
        [When("ورودی کالا با کد 1 را به تعداد 4  ویرایش می کنیم")]
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

            _sut.Update(_goods.Id,_updateSalesInvoiceDto);

        }

        [Then(" ورودی با کد کالای '01' و تعداد 1' و به ما داده می شود")]
        public void Then()
        {
            _context.SalesInvoices.Any(_ => _.GoodsId == 1
                                        && _.Count == 5).Should().BeTrue();
        }

        [And(" تعداد 2عدد از کالا موجود می باشد")]
        public void ThenAnd()
        {
            int goodsId = _goodsRepository.FindById(_salesInvoice.GoodsId).Id;
            _context.Goods
                .FirstOrDefault(_ => _.Id == goodsId)
                .Count.Should().Be(2);
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given()
                , _ => When()
                , _ => Then()
                , _ => ThenAnd());
        }
    }
} 