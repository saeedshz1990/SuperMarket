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
using SuperMarket.Services.SalesInvoices.Exceptions;
using SuperMarket.Test.Tools.Categories;
using SuperMarket.Test.Tools.Goodses;
using Xunit;

namespace SuperMarket.Services.Test.Unit.SalesInvoices
{
    public class SalesInvoiceServiceTests
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private SalesInvoiceRepository _salesInvoiceRepository;
        private readonly SalesInvoiceService _sut;

        private readonly GoodsRepository _goodsRepository;
        private readonly CategoryRepository _categoryRepository;

        private SalesInvoice _salesInvoice;
        private AddSalesInvoiceDto _addSalesInvoiceDto;
        private UpdateSalesInvoiceDto _updateSalesInvoiceDto;
        private GetSalesInvoiceDto _getSalesInvoiceDto;
        private Goods _goods;
        private Category _category;

        public SalesInvoiceServiceTests()
        {
            _context = new EFInMemoryDatabase().CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_context);
            _salesInvoiceRepository = new EFSalesInvoiceRepository(_context);
            _sut = new SalesInvoiceAppService(_unitOfWork, _salesInvoiceRepository,_goodsRepository);
            _categoryRepository = new EFCategoryRepository(_context);
            _goodsRepository = new EFGoodsRepository(_context);
        }

        [Fact]
        public void Add_adds_salesInvoice_properly()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            int categoryId = _categoryRepository.FindByName(_category.Name).Id;
            _goods = CreateGoodsFactory.CreateGoods(categoryId);
            _context.Manipulate(_ => _.Goods.Add(_goods));

            _addSalesInvoiceDto = new AddSalesInvoiceDto
            {
                CustomerName = "Saeed Ansari",
                SalesDate = DateTime.Now.Date,
                SalesPrice = 2000,
                GoodsId = _goods.Id,
                Count = 3
            };
            _sut.Add(_addSalesInvoiceDto);

            _context.SalesInvoices.Should().HaveCount(3);
        }

        [Fact]
        public void Throw_Exception_When_GoodsId_NotFound()
        {
            _addSalesInvoiceDto = new AddSalesInvoiceDto
            {
                CustomerName = "Saeed Ansari",
                SalesDate = DateTime.Now.Date,
                SalesPrice = 2000,
                GoodsId = _goods.Id,
                Count = 3
            };
            Action expected = () => _sut.Add(_addSalesInvoiceDto);
            expected.Should().ThrowExactly<GoodsIdNotFoundForSaleInvoicesException>();
        }

        [Fact]
        public void Delete_deletes_SalesInvoices_properly()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            int categoryId = _categoryRepository.FindByName(_category.Name).Id;
            _goods = CreateGoodsFactory.CreateGoods(categoryId);
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
            _sut.Delete(_salesInvoice.Id);

            _context.SalesInvoices.FirstOrDefault(_ => _.Id == _salesInvoice.Id).Should().BeNull();
            _context.SalesInvoices.Should().HaveCount(0);

        }

        [Fact]
        public void ThrowException_When_GoodsId_doesNotExist()
        {
            int fakeId = 2;
            Action expected = () => _sut.Delete(fakeId);
            expected.Should().ThrowExactly<SalesInvoiceNotFoundException>();
        }
        
       [Fact]
        public void Update_updates_SalesInvoices_Properly()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            int categoryId = _categoryRepository.FindByName(_category.Name).Id;
            _goods = CreateGoodsFactory.CreateGoods(categoryId);
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
            _updateSalesInvoiceDto = new UpdateSalesInvoiceDto
            {
                CustomerName = "Saeed Ansari",
                SalesDate = DateTime.Now.Date,
                SalesPrice = 2000,
                GoodsId = _goods.Id,
                Count = 5
            };

            _sut.Update(_salesInvoice.Id, _updateSalesInvoiceDto);
        }

        [Fact]
        public void ThrowException_When_SalesInvoiceId_doesNotExist()
        {
            _updateSalesInvoiceDto = new UpdateSalesInvoiceDto
            {
                CustomerName = "Saeed Ansari",
                SalesDate = DateTime.Now.Date,
                SalesPrice = 2000,
                GoodsId = 1,
                Count = 5
            };
            
            Action expected = () => _sut.Update(80, _updateSalesInvoiceDto);
    
             expected.Should().ThrowExactly<SalesInvoicesNotExistException>();
        }

        [Fact]
        public void GetAll_gets_all_SalesInvoices_Properly()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            int categoryId = _categoryRepository.FindByName(_category.Name).Id;
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
            var expected = _sut.GetAll();

            expected.Should().HaveCount(1);
            expected.Should().Contain(_ => _.GoodsId == _salesInvoice.GoodsId
                                           && _.CustomerName == _salesInvoice.CustomerName
                                           && _.SalesDate == _salesInvoice.SalesDate
                                           && _.SalesPrice == _salesInvoice.SalesPrice
                                           && _.Count == _salesInvoice.Count);
        }
    }
}
