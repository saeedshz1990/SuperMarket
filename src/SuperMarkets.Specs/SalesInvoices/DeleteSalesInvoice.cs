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
    public class DeleteSalesInvoice : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly SalesInvoiceRepository _salesInvoiceRepository;
        private readonly SalesInvoiceService _sut;
        private Category _category;
        private Goods _goods;
        private SalesInvoice _salesInvoice;
        private readonly CategoryRepository _categoryRepository;
        public DeleteSalesInvoice(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _salesInvoiceRepository = new EFSalesInvoiceRepository(_context);
            _sut = new SalesInvoiceAppService(_salesInvoiceRepository, _unitOfWork);
            _categoryRepository = new EFCategoryRepository(_context);
        }

        [Given("ورودی کالا با کد '01' در فهرست ورودی کالا ها موجود است")]
        public void Given()
        {

            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            var categoryId = _categoryRepository.FindById(_category.Id);
            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));

            _salesInvoice = new SalesInvoice
            {
                CustomerName = "Saeed Ansari",
                SalesDate = DateTime.Now.Date,
                SalesPrice = 2000,
                GoodsId = 1,
                Count = 3
            };
            _context.Manipulate(_ => _.SalesInvoices.Add(_salesInvoice));

        }

        [When("ورودی کالای کد '01' را حذف می کنیم")]
        public void When()
        {
            _sut.Delete(_salesInvoice.Id);
        }

        [Then("ورودی کالای کد '01'وجود ندارد")]

        public void Then()
        {
            _context.SalesInvoices.FirstOrDefault(_ => _.Id == _salesInvoice.Id)
                .Should().BeNull();
        }

        [And(" تعداد 7عدد از کالا موجود می باشد")]

        public void ThenAnd()
        {
            int goodsId = _goodsRepository.FindById(_salesInvoice.GoodsId).GoodsId;
            _context.Goods
                .FirstOrDefault(_ => _.Id == goodsId).Should().Be(7);
        }


        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given()
                , _ => When()
                , _ => Then()
                , _ => ThenAnd()
            );
        }
    }
}