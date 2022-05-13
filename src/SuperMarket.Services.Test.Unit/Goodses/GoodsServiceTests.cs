using System;
using System.Linq;
using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.Categories;
using SuperMarket.Persistence.EF.Goodses;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.Categories.Exceptions;
using SuperMarket.Services.Goodses;
using SuperMarket.Services.Goodses.Contracts;
using SuperMarket.Services.Goodses.Exceptions;
using SuperMarket.Test.Tools.Categories;
using SuperMarket.Test.Tools.Goodses;
using Xunit;

namespace SuperMarket.Services.Test.Unit.Goodses
{
    public class GoodsServiceTests
    {
        private readonly EFDataContext _context;
        private readonly GoodsService _sut;
        private readonly GoodsRepository _goodsRepository;
        private readonly UnitOfWork _unitOfWork;
        private Goods _goods;
        private Goods _secondGoods;
        private AddGoodsDto _addGoodsDto;
        private GetGoodsDto _getGoodsDto;
        private UpdateGoodsDto _updateGoodsDto;
        private Category _category;
        private readonly CategoryRepository _categoryRepository;

        public GoodsServiceTests()
        {
            _context = new EFInMemoryDatabase().CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_context);
            _goodsRepository = new EFGoodsRepository(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new GoodsAppService(_unitOfWork, _goodsRepository, _categoryRepository);
        }

        [Fact]
        public void Add_adds_Goods_Properly()
        {
            CreateOneCategory();

            _addGoodsDto = CreateGoodsFactory.CreateAddGoods(_category.Id);
            _sut.Add(_addGoodsDto);

            var expected = _context.Goods.FirstOrDefault();

            expected.Name.Should().Be(_addGoodsDto.Name);
            expected.SalesPrice.Should().Be(_addGoodsDto.SalesPrice);
            expected.MinimumInventory.Should().Be(_addGoodsDto.MinimumInventory);
            expected.Count.Should().Be(_addGoodsDto.Count);
            expected.UniqueCode.Should().Be(_addGoodsDto.UniqueCode);
            expected.Category.Id.Should().Be(_category.Id);
        }

        [Fact]
        public void Add_ThrowException_When_DuplicateGoodsName_In_OneCategory_WhenAddingGoods()
        {
            CreateOneCategory();
            CreateAGoodsDto();
            CreateOneAddGoodsDto();

            Action expected = () => _sut.Add(_addGoodsDto);

            expected.Should().ThrowExactly<GoodsNameExistInThisCategoryException>();
        }

        [Fact]
        public void Add_ThrowException_When_Category_Not_Found()
        {
            CreateOneCategory();
            CreateOneGoods();

            CreateOneGoodsDto();
            Action expected = () => _sut.Add(_addGoodsDto);

            expected.Should().ThrowExactly<CategoryNotFoundException>();
        }

        [Fact]
        public void Update_updates_Goods_Properly()
        {
            CreateOneCategory();
            CreateOneGoods();

            CreateUpdateGoodsDto();
            _sut.Update(_goods.Id, _updateGoodsDto);

            _context.Goods.Should()
                .Contain(_ => _.Id == _goods.Id);
            _context.Goods.Should()
                .Contain(_ => _.Name == _updateGoodsDto.Name);
            _context.Goods.Should()
                .Contain(_ => _.SalesPrice == _updateGoodsDto.SalesPrice);
            _context.Goods.Should()
                .Contain(_ => _.MinimumInventory == _updateGoodsDto.MinimumInventory);
            _context.Goods.Should()
                .Contain(_ => _.Count == _updateGoodsDto.Count);
            _context.Goods.Should()
                .Contain(_ => _.UniqueCode == _updateGoodsDto.UniqueCode);
            _context.Goods.Should()
                .Contain(_ => _.CategoryId == _category.Id);
        }
        
        [Fact]
        public void Delete_deletes_Goods_Properly()
        {
            CreateOneCategory();

            CreateOneGoods();

            _sut.Delete(_goods.Id);
            _context.Goods.Count().Should().Be(0);
            _context.Goods.Should().NotContain(_ => _.Name == _goods.Name);
        }

        [Fact]
        public void Delete_ThrowException_When_GoodsId_NotExist()
        {
            var fakeGoodsId = 878;
            Action expected = () => _sut.Delete(fakeGoodsId);

            expected.Should().ThrowExactly<GoodsIdNotExistInThisCategoryException>();
        }

        private void CreateOneCategory()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("Dummy");
            _context.Manipulate(_ => _.Categories.Add(_category));
        }

        private void CreateOneGoods()
        {
            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Add(_goods));
        }
        private void CreateOneGodsYouthrtGhanbarzadeh()
        {
            _secondGoods = new Goods
            {
                Name = "ماست قنبرزاده",
                SalesPrice = 3000,
                MinimumInventory = 5,
                Count = 15,
                UniqueCode = "YK-191",
                CategoryId = _category.Id
            };
            _context.Manipulate(_ => _.Goods.Add(_secondGoods));
        }

        private void CreateOneGoodsDto()
        {
            _addGoodsDto = CreateGoodsFactory.CreateAddGoodsDto("ماست کاله", 3,
                2, 1000, "UY-234", 2);
        }
        private void CreateOneAddGoodsDto()
        {
            _addGoodsDto = new AddGoodsDto
            {
                Name = _goods.Name,
                Count = _goods.Count,
                MinimumInventory = _goods.MinimumInventory,
                SalesPrice = _goods.SalesPrice,
                UniqueCode = _goods.UniqueCode,
                CategoryId = _category.Id
            };
        }
        private void CreateUpdateGoodsDto()
        {
            _updateGoodsDto = new UpdateGoodsDto
            {
                Name = "ماست قنبرزاده",
                SalesPrice = 3000,
                MinimumInventory = 5,
                Count = 15,
                UniqueCode = "YK-191",
                CategoryId = _category.Id
            };
        }
        private void CreateAGoodsDto()
        {
            _goods = new Goods
            {
                Name = "Dummy",
                Count = 18,
                MinimumInventory = 9,
                SalesPrice = 7800,
                UniqueCode = "YD-999",
                CategoryId = _category.Id
            };
            _context.Manipulate(_ => _.Goods.Add(_goods));
        }
    }
}