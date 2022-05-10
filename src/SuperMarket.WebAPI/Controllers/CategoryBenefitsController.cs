using Microsoft.AspNetCore.Mvc;
using SuperMarket.Services.Benefit_Calculates.Contracts;

namespace SuperMarket.WebAPI.Controllers
{
    [Route("api/categoryBenefits")]
    [ApiController]
    public class CategoryBenefitsController : ControllerBase
    {
        private readonly BenefitCalculateService _service;

        public CategoryBenefitsController(BenefitCalculateService service)
        {
            _service = service;
        }

        [HttpGet("{categoryId}")]
        public void GetAllCategory(int categoryId, int goodsId)
        {
            _service.BenefitCategoryCalculate(categoryId,goodsId);
        }

    }
}
