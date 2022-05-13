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
    [Scenario("تعریف دسته بندی")]
    [Feature("",
        AsA = "فروشنده ",
        IWantTo = " دسته بندی کالا   ",
        InOrderTo = "مدیریت دسته بندی"
    )]
    public class UpdateCategory : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly CategoryRepository _categoryRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryService _sut;
        private Category _category;
        private UpdateCategoryDto _updateCategoryDto;

        public UpdateCategory(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new CategoryAppService(_unitOfWork, _categoryRepository);
        }

        [Given("یک دسته بندی با عنوان 'لبنیات'در فهرست دسته بندی کالا وجود دارد")]
        public void Given()
        {
            CreateCategory();
        }

        [When("دسته بندی با عنوان 'لبنیات' را به 'پروتئینی' ویرایش میکنم")]
        public void When()
        {
            CreateUpdateCategoryDto();
            _sut.Update(_category.Id, _updateCategoryDto);
        }

        [Then("یک دسته بندی با عنوان 'پروتئینی' باید در فهرست دسته بندی ها وجود داشته باشد")]
        public void Then()
        {
            _context.Categories
                .Should().Contain(_ => _.Name == _updateCategoryDto.Name);
        }

        [Fact]
        public void Run()
        {
            Given();
            When();
            Then();
        }

        private void CreateCategory()
        {
            _category = new Category
            {
                Name = "لبنیات"
            };

            _context.Manipulate(_ => _.Categories.Add(_category));
        }
        private void CreateUpdateCategoryDto()
        {
            _updateCategoryDto = new UpdateCategoryDto
            {
                Name = "پروتئینی"
            };
        }
    }
}