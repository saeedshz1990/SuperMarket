using FluentAssertions;
using SuperMarket.Entities;
using SuperMarket.Infrastructure.Application;
using SuperMarket.Infrastructure.Test;
using SuperMarket.Persistence.EF;
using SuperMarket.Persistence.EF.Categories;
using SuperMarket.Services.Categories;
using SuperMarket.Services.Categories.Contracts;
using SuperMarkets.Specs.Infrastructure;
using Xunit;
using static SuperMarkets.Specs.BDDHelper;

namespace SuperMarkets.Specs.Categories
{
    [Scenario("مدیریت دسته بندی")]
    [Feature("",
        AsA = "فروشنده ",
        IWantTo = " دسته بندی کالا   ",
        InOrderTo = "مدیریت دسته بندی"
    )]
    public class GetCategory : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly CategoryRepository _categoryRepository;
        private readonly CategoryService _sut;
        private readonly UnitOfWork _unitOfWork;
        private Category _category;
        private GetCategoryDto _getCategoryDto;
        private AddCategoryDto _addCategoryDto;

        public GetCategory(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _categoryRepository = new EFCategoryRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new CategoryAppService(_unitOfWork, _categoryRepository);
        }

        [Given(" دسته بندی با عنوان 'لبنیات'در فهرست دسته بندی کالا وجود دارد")]
        public void Given()
        {
            _category = new Category
            {
                Name = "لبنیات"
            };

            _context.Manipulate(_ => _.Categories.Add(_category));
        }

        [When("درخواست مشاهده فهرست کالا را میدهم")]
        public void When()
        {
            _sut.GetAll();
        }

        [Then("دسته بندی با عنوان ‘لبنیات’ در فهرست دسته بندی کالا باید وجود داشته باشد")]
        public void Then()
        {

            var expected = _sut.GetAll();
            expected.Should().HaveCount(1);
            expected.Should().Contain(_ => _.Name == _category.Name);
        }

        [Fact]
        public void Run()
        {
            Given();
            When();
            Then();
        }
    }
}
