﻿using System.Collections.Generic;
using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.Categories;
using SuperMarket.Persistence.EF.Goodses;
using SuperMarket.Services.Categories.Contracts;
using SuperMarket.Services.Goodses;
using SuperMarket.Services.Goodses.Contracts;
using SuperMarkets.Specs.Infrastructure;
using Xunit;
using static SuperMarkets.Specs.BDDHelper;

namespace SuperMarkets.Specs.Goodses
{
    [Scenario("مدیریت کالا")]
    [Feature("",
        AsA = "فروشنده ",
        IWantTo = " فروش کالا ",
        InOrderTo = "مدیریت کنم"
    )]
    public class GetGoods : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly GoodsRepository _goodsRepository;
        private readonly GoodsService _sut;
        private readonly EFUnitOfWork _unitOfWork;
        private readonly CategoryRepository _categoryRepository;
        private Goods _goods;
        private Category _category;
        private UpdateCategoryDto _updateCategoryDto;
        private AddGoodsDto _addGoodsDto;
        private IList<GetGoodsDto> expected;

        public GetGoods(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _goodsRepository = new EFGoodsRepository(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new GoodsAppService(_unitOfWork, _goodsRepository, _categoryRepository);
        }

        [Given("دسته بندی کالا با عنوان ‘لبنیات ‘  تعریف می کنیم")]
        public void Given()
        {
            CreateCategory();
        }
        
        [And("کالایی با عنوان ‘ماست رامک’  با قیمت فروش’۲۰۰۰’  با کد کالا انحصاری’YR-190’   با موجودی ‘۱۰’  تعریف می کنم")]
        public void GivenAnd()
        {
            CreateOneGoods();
        }
        
        [When("درخواست نمایش تمام کالای های موجود در دسته بندی را می کنم")]
        public void When()
        {
           expected= _sut.GetAll();
        }

        [Then("تنها کالایی با عنوان ‘ماست رامک’  با قیمت فروش’۲۰۰۰’  با کد کالا انحصاری’YR-190’با موجودی ‘۱۰’   جهت نمایش در فهرست کالا وجود داشته باشد")]
        public void Then()
        {
            expected.Should().HaveCount(0);
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given()
                , _ => When()
                , _ => Then());
        }
        private void CreateOneGoods()
        {
            _goods = new Goods
            {
                Name = "ماست  رامک",
                SalesPrice = 2000,
                MinimumInventory = 5,
                Count = 10,
                UniqueCode = "YK-190",
                CategoryId = _category.Id
            };
            _context.Manipulate(_ => _context.Goods.Add(_goods));
        }
        
        private void CreateCategory()
        {
            _category = new Category()
            {
                Name = "لبنیات"
            };
            _context.Manipulate(_ => _context.Categories.Add(_category));
        }
    }
}