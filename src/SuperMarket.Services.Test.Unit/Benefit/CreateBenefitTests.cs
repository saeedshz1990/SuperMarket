using System;
using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.Categories;
using SuperMarket.Persistence.EF.EntryDocuments;
using SuperMarket.Persistence.EF.Goodses;
using SuperMarket.Persistence.EF.SalesInvoices;
using SuperMarket.Services.Benefit_Calculates;
using SuperMarket.Services.Benefit_Calculates.Contracts;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.EntryDocuments;
using SuperMarket.Services.EntryDocuments.Contracts;
using SuperMarket.Services.Goodses.Contracts;
using SuperMarket.Services.SalesInvoices.Contracts;
using SuperMarket.Test.Tools.Categories;
using SuperMarket.Test.Tools.EntryDocumnets;
using SuperMarket.Test.Tools.Goodses;
using SuperMarket.Test.Tools.SaleInvoices;
using Xunit;

namespace SuperMarket.Services.Test.Unit.Benefit
{
    public class CreateBenefitTests
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly EntryDocumentRepository _entryDocumentRepository;
        private readonly GoodsRepository _goodsRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly SalesInvoiceRepository _salesInvoiceRepository;
        private readonly BenefitCalculateService _sut;

        private Category _category;
        private Goods _goods;
        private EntryDocument _entryDocument;
        private SalesInvoice _salesInvoice;

        public CreateBenefitTests()
        {
            _context = new EFInMemoryDatabase().CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_context);
            _entryDocumentRepository = new EFEntryDocumentRepository(_context);
            _goodsRepository = new EFGoodsRepository(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _salesInvoiceRepository = new EFSalesInvoiceRepository(_context);
            _sut = new BenefitCalculateAppService(_unitOfWork, _categoryRepository,
                _goodsRepository,_entryDocumentRepository,_salesInvoiceRepository);            
        }

        [Fact]
        public void CalculateGoods_calculateGoodses_all_in_Category_properly()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_=>_.Categories.Add(_category));

            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_=>_.Goods.Add(_goods));

            _entryDocument = CreateEntryDocumentsFactory.CreateEntryDocumentDto(_goods.Id);
            _context.Manipulate(_=>_.EntryDocuments.Add(_entryDocument));

            _salesInvoice = CreateSalesInvoiceFactory.CreateSalesInvoice(_goods.Id);
            _context.Manipulate(_=>_.SalesInvoices.Add(_salesInvoice));

            var expected = _sut.BenefitGoodsCalculate(_goods.Id);
            expected.Should().Be(0);
        }

        [Fact]
        public void CategoryBenefit_categoriesBenefit_allGoodses_properly()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));

            _entryDocument = CreateEntryDocumentsFactory.CreateEntryDocumentDto(_goods.Id);
            _context.Manipulate(_ => _.EntryDocuments.Add(_entryDocument));

            _salesInvoice = CreateSalesInvoiceFactory.CreateSalesInvoice(_goods.Id);
            _context.Manipulate(_ => _.SalesInvoices.Add(_salesInvoice));

            var expected = _sut.BenefitCategoryCalculate(_category.Id, _goods.Id);
            expected.Should().Be(0);
        }

    }
}
