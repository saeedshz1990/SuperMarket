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
using SuperMarket.Services.Goodses.Contracts;
using SuperMarket.Test.Tools.Categories;
using SuperMarket.Test.Tools.EntryDocumnets;
using SuperMarket.Test.Tools.Goodses;
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
        private readonly GoodsRepository _goodsRepository;
        
        public EntryDocumentServiceTests()
        {
            _context = new EFInMemoryDatabase().CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new EntryDocumentAppservice(_unitOfWork, _entryDocumentRepository, _goodsRepository);
            _entryDocumentRepository = new EFEntryDocumentRepository(_context);
        }

        [Fact]
        public void Add_adds_EntryDocument_Properly()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));

            _addEntryDocumentDto = CreateEntryDocumentsFactory.CreateAddEntryDocument(_goods.Id,
                5 + _goods.Count, _addEntryDocumentDto.BuyPrice, _addEntryDocumentDto.DateBuy);

            _sut.Add(_addEntryDocumentDto);

            _context.EntryDocuments.Should().HaveCount(1);
        }

        [Fact]
        public void ThrowException_When_GoodsId_NotFound()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));

            _addEntryDocumentDto = CreateEntryDocumentsFactory.CreateAddEntryDocument(_goods.Id,
                5 + _goods.Count, _addEntryDocumentDto.BuyPrice, _addEntryDocumentDto.DateBuy);

            Action expected = () => _sut.Add(_addEntryDocumentDto);
            expected.Should().ThrowExactly<GoodIdNotFoundException>();
        }

        [Fact]
        public void GetAll_getall_EntryDocument_properly()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));

            _entryDocument = CreateEntryDocumentsFactory.CreateEntryDocument(_goods.Id,
                5 + _goods.Count, _addEntryDocumentDto.BuyPrice, _addEntryDocumentDto.DateBuy);

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

            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));

            _updateEntryDocumentDto = CreateEntryDocumentsFactory
                .CreateUpdateEntryDocumentDto(_goods.Id, _updateEntryDocumentDto.GoodsCount);
            _sut.Update(_entryDocument.Id, _updateEntryDocumentDto);

            _context.Should().Be(_updateEntryDocumentDto.GoodsCount);
        }
    }
}