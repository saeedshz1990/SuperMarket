using System.Linq;
using FluentAssertions;
using SuperMarket.Infrastructure.Application;
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
    public class AddCategory : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly CategoryRepository _categoryRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryService _sut;
        private AddCategoryDto _dto;
        
        public AddCategory(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _unitOfWork = new EFUnitOfWork(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new CategoryAppService(_unitOfWork, _categoryRepository);
        }

        [Given("هیچ دسته بندی در فهرست دسته بندی کالا وجود ندارد")]
        public void Given()
        {

        }

        [When("دسته بندی با عنوان ‘لبنیات’ تعریف می کنم")]
        public void When()
        {
            CreateCategoryDto();
            _sut.Add(_dto);
        }
        
        [Then("دسته بندی با عنوان ‘لبنیات’ در فهرست دسته بندی کالا باید وجود داشته باشد")]
        public void Then()
        {
            var expected = _context.Categories.FirstOrDefault();
            expected.Name.Should().Be(_dto.Name);
        }

        [Fact]
        public void Run()
        {
            Given();
            When();
            Then();
        }
        private void CreateCategoryDto()
        {
            _dto = new AddCategoryDto
            {
                Name = "لبنیات"
            };
        }
    }
}