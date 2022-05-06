using System;
using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.EntryDocuments;
using SuperMarket.Services.EntryDocuments;
using SuperMarket.Services.EntryDocuments.Contracts;
using SuperMarket.Services.EntryDocuments.Exceptions;
using Xunit;

namespace SuperMarket.Services.Test.Unit.EntryDocuments
{
    public class EntryDocumentServiceTests
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly EntryDocumentService _sut;
        private readonly EntryDocumentRepository _entryDocumentRepository;
        private Category _category;
        private Goods _goods;
        private AddEntryDocumentDto _addEntryDocumentDto;
        private EntryDocument _entryDocument;
        private UpdateEntryDocumentDto _updateEntryDocumentDto;

        public EntryDocumentServiceTests()
        {
            _context = new EFInMemoryDatabase().CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new EntryDocumentAppservice(_unitOfWork, _entryDocumentRepository);
            _entryDocumentRepository = new EFEntryDocumentRepository(_context);
        }

        [Fact]
        public void Add_adds_EntryDocument_Properly()
        {
            _category = new Category { Name = "خشکبار" };
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = new Goods
            {
                Name = "پسته بسته بندی قنبرزاده",
                CategoryId = _category.Id,
                SalesPrice = 10900,
                MinimumInventory = 5,
                Count = 17,
                UniqueCode = "YU-987"
            };
            _context.Manipulate(_ => _.Goods.Add(_goods));

            _addEntryDocumentDto = new AddEntryDocumentDto
            {
                GoodsId = _goods.Id,
                GoodsCount = 5 + _goods.Count,
                BuyPrice = _addEntryDocumentDto.BuyPrice,
                DateBuy = DateTime.Now.Date,
            };

            _sut.Add(_addEntryDocumentDto);

            _context.EntryDocuments.Should().HaveCount(1);
        }

        [Fact]
        public void ThrowException_When_GoodsId_NotFound()
        {
            _category = new Category { Name = "خشکبار" };
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = new Goods
            {
                Name = "پسته بسته بندی قنبرزاده",
                CategoryId = _category.Id,
                SalesPrice = 10900,
                MinimumInventory = 5,
                Count = 17,
                UniqueCode = "YU-987"
            };
            _context.Manipulate(_ => _.Goods.Add(_goods));
            
            _addEntryDocumentDto = new AddEntryDocumentDto
            {
                GoodsId = _goods.Id,
                GoodsCount = 5 + _goods.Count,
                BuyPrice = _addEntryDocumentDto.BuyPrice,
                DateBuy = DateTime.Now.Date,
            };

            Action expected = () => _sut.Add(_addEntryDocumentDto);
            expected.Should().ThrowExactly<GoodIdNotFoundException>();
        }

        [Fact]
        public void GetAll_getall_EntryDocument_properly()
        {
            _category = new Category { Name = "خشکبار" };
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = new Goods
            {
                Name = "پسته بسته بندی قنبرزاده",
                CategoryId = _category.Id,
                SalesPrice = 10900,
                MinimumInventory = 5,
                Count = 17,
                UniqueCode = "YU-987"
            };
            _context.Manipulate(_ => _.Goods.Add(_goods));

            _entryDocument = new EntryDocument
            {
                GoodsId = _goods.Id,
                GoodsCount = 5 + _goods.Count,
                BuyPrice = 12000,
                DateBuy = DateTime.Now.Date,
            };

            var expected = _sut.GetAll();

            expected.Should().HaveCount(1);
            _context.EntryDocuments.Should().Contain(_ => _.BuyPrice == _entryDocument.BuyPrice);
            _context.EntryDocuments.Should().Contain(_ => _.DateBuy == _entryDocument.DateBuy);
            _context.EntryDocuments.Should().Contain(_ => _.GoodsCount == _goods.Count + _entryDocument.GoodsCount);
            _context.EntryDocuments.Should().Contain(_ => _.GoodsId == _entryDocument.GoodsId);
        }

        [Fact]
        public void Update_updates_EntryDocument_properly()
        {

            _category = new Category { Name = "خشکبار" };
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = new Goods
            {
                Name = "پسته بسته بندی قنبرزاده",
                CategoryId = _category.Id,
                SalesPrice = 10900,
                MinimumInventory = 5,
                Count = 17,
                UniqueCode = "YU-987"
            };
            _context.Manipulate(_ => _.Goods.Add(_goods));

            _updateEntryDocumentDto = new UpdateEntryDocumentDto
            {
                GoodsCount=_updateEntryDocumentDto.GoodsCount
            };
            _sut.Update(_entryDocument.Id, _updateEntryDocumentDto);

            _context.Should().Be(_updateEntryDocumentDto.GoodsCount);
        }
    }
}