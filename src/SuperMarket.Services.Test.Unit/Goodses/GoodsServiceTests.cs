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
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

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
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Add(_category));

            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Add(_goods));

            _addGoodsDto = CreateGoodsFactory.CreateAddGoodsDto("ماست رامک", 3,
                2, 1000, "YR-190", _category.Id);
            Action expected = () => _sut.Add(_addGoodsDto);
            
            expected.Should().ThrowExactly<GoodsNameExistInThisCategoryException>();
        }

        [Fact]
        public void Add_ThrowException_When_Category_Not_Found()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));
            _addGoodsDto = CreateGoodsFactory.CreateAddGoodsDto("ماست کاله", 3,
                2, 1000, "UY-234", 2);
            Action expected = () => _sut.Add(_addGoodsDto);
            expected.Should().ThrowExactly<CategoryNotFoundException>();
        }


        [Fact]
        public void Update_updates_Goods_Properly()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Add(_goods));
            
            _updateGoodsDto = CreateGoodsFactory.CreateUpdateGoods(_category.Id);
            _sut.Update(_goods.Id, _updateGoodsDto);
            
            var expected = _context.Goods.FirstOrDefault(_ => _.Id == _goods.Id);
            expected.Name.Should().Be(_updateGoodsDto.Name);
            expected.SalesPrice.Should().Be(_updateGoodsDto.SalesPrice);
            expected.MinimumInventory.Should().Be(_updateGoodsDto.MinimumInventory);
            expected.Count.Should().Be(_updateGoodsDto.Count);
            expected.UniqueCode.Should().Be(_updateGoodsDto.UniqueCode);
            expected.CategoryId.Should().Be(_updateGoodsDto.CategoryId);
        }

        [Fact]
        public void Delete_deletes_Goods_Properly()
        {
            _category = CreateCategoryFactory.CreateCategoryDto("لبنیات");
            _context.Manipulate(_ => _.Categories.Add(_category));

            _goods = CreateGoodsFactory.CreateGoods(_category.Id);
            _context.Manipulate(_ => _.Goods.Add(_goods));

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
    }
}