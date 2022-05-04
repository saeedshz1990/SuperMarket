using System.Linq;
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
        IWantTo = "دسته بندی کالا مدیریت کنم",
        InOrderTo = "کالا های خود را دسته بندی کنم"
    )]
    public class DeleteCategory :EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryRepository _categoryRepository;
        private readonly CategoryService _sut;
        private AddCategoryDto _addCategoryDto;
        private Category _category;

        public DeleteCategory(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new CategoryAppService(_unitOfWork, _categoryRepository);
        }

        [Given("دسته بندی با عنوان 'لبنیات'در فهرست دسته بندی کالا وجود دارد")]
        public void Given()
        {
            _category = new Category
            {
                Name = "لبنیات"
            };

            _context.Manipulate(_ => _.Categories.Add(_category));
        }
        [When("دسته بندی با عنوان 'لبنیات' را حذف میکنیم")]
        public void When()
        {
            _sut.Delete(_category.Id);
        }

        [Then("دسته بندی در فهرست دسته بندی ها نباید وجود داشته باشد")]
        public void Then()
        {
            var expected = _context.Categories.FirstOrDefault();
            _context.Categories.Should().NotContain(_category.Name);
            _context.Categories.Count().Should().Be(0);
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
