using System;
using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.EntryDocuments;
using SuperMarket.Persistence.EF.Goodses;
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
            _entryDocumentRepository = new EFEntryDocumentRepository(_context);
            _goodsRepository = new EFGoodsRepository(_context);
            _sut = new EntryDocumentAppService(_unitOfWork, _entryDocumentRepository, _goodsRepository);
        }

        [Fact]
        public void Add_adds_EntryDocument_Properly()
        {
            CreateOneCategory();
            CreateOneGoods();

            CreateOneEntryDocumentDto();
            _sut.Add(_addEntryDocumentDto);

            _context.EntryDocuments.Should()
                .Contain(_ => _.GoodsId == _addEntryDocumentDto.GoodsId);
            _context.EntryDocuments.Should()
                .Contain(_ => _.BuyPrice == _addEntryDocumentDto.BuyPrice);
            _context.EntryDocuments.Should()
                .Contain(_ => _.BuyPrice == _addEntryDocumentDto.BuyPrice);
            _context.EntryDocuments.Should()
                .Contain(_ => _.DateBuy == _addEntryDocumentDto.DateBuy);
            _context.EntryDocuments.Should()
                .Contain(_ => _.GoodsCount == _addEntryDocumentDto.GoodsCount);
        }
        
        [Fact]
        public void AddThrowException_When_GoodsId_NotFound_In_EntryDocuments()
        {
            CreateOneCategory();
            CreateOneGoods();
            CreateOneEntryDocument();

            CreateOneEntryDocumentDto();

            Action expected = () => _sut.Add(_addEntryDocumentDto);
            expected.Should().ThrowExactly<GoodIdNotFoundException>();
        }
        
        [Fact]
        public void GetAll_getAll_EntryDocument_properly()
        {
            CreateOneCategory();
            CreateOneGoods();

            CreateAEntryDocument();
            var expected = _sut.GetAll();

            expected.Should().HaveCount(1);
            _context.EntryDocuments.Should()
                .Contain(_ => _.BuyPrice == _entryDocument.BuyPrice);
            _context.EntryDocuments.Should()
                .Contain(_ => _.DateBuy == _entryDocument.DateBuy);
            _context.EntryDocuments.Should()
                .Contain(_ => _.GoodsCount == _entryDocument.GoodsCount);
            _context.EntryDocuments.Should()
                .Contain(_ => _.GoodsId == _entryDocument.GoodsId);
        }
        
        [Fact]
        public void Update_updates_EntryDocument_properly()
        {
            CreateOneCategory();
            CreateOneGoods();

            CreateAEntryDocument();

            CreateUpdateEntryDocumentDto();
        }

        private void CreateUpdateEntryDocumentDto()
        {
            _updateEntryDocumentDto = CreateEntryDocumentsFactory
                .CreateUpdateEntryDocumentDto(_entryDocument.Id, 5);
            _sut.Update(_entryDocument.Id, _updateEntryDocumentDto);
        }

        private void CreateOneEntryDocumentDto()
        {
            _addEntryDocumentDto = CreateEntryDocumentsFactory
                .CreateAddEntryDocumentDto(_goods.Id);
        }

        private void CreateOneGoods()
        {
            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));
        }

        private void CreateOneCategory()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));
        }

        private void CreateOneEntryDocument()
        {
            var addEnterDocumentSecond = CreateEntryDocumentsFactory.CreateEntryDocumentDto(_goods.Id);
            _context.Manipulate(_ => _.EntryDocuments.Add(addEnterDocumentSecond));
        }
        private void CreateAEntryDocument()
        {
            _entryDocument = CreateEntryDocumentsFactory.CreateEntryDocumentDto(_goods.Id);
            _context.Manipulate(_ => _.EntryDocuments.Add(_entryDocument));
        }
    }
}