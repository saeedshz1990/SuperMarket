using System;
using System.Linq;
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
    public class AddSalesInvoice : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly SalesInvoiceRepository _salesInvoiceRepository;
        private readonly SalesInvoiceService _sut;
        private Category _category;
        private Goods _goods;
        private SalesInvoice _salesInvoice;

        private readonly CategoryRepository _categoryRepository;

        public AddSalesInvoice(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _salesInvoiceRepository = new EFSalesInvoiceRepository(_context);
            _sut = new SalesInvoiceAppService(_salesInvoiceRepository, _unitOfWork);
            _categoryRepository = new EFCategoryRepository(_context);
        }

        [Given("الایی با کد '01' در فهرست کالا ها تعریف شده است و  6 عدد موجود است")]
        public void Given()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            var categoryId = _categoryRepository.FindById(_category.Id);
            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));
        }

        
        [When("کالای 01 را در به تعداد 2 می فروشیم")]
        public void When()
        {
            _salesInvoice = new SalesInvoice
            {
                CustomerName = "Saeed Ansari",
                SalesDate = DateTime.Now.Date,
                SalesPrice = 2000,
                GoodsId = 1,
                Count = 3
            };

            _sut.Add(_salesInvoice);
        }
        [Then("فروش کالا در لیست فروش موجود است")]

        public void Then()
        {
            _context.SalesInvoices.Count(_ => _.GoodsId == 1 && _.Count == 2);

        }

        [When("تعداد 4 کالا در لیست کالا ها باقی")]

        public void ThenAnd()
        {
            _goodsRepository.Should().Be(4);
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
}
