using Microsoft.AspNetCore.Mvc;
using SuperMarket.Services.Benefit_Calculates.Contracts;

namespace SuperMarket.WebAPI.Controllers
{
    [Route("api/goodsBenefits")]
    [ApiController]
    public class GoodsBenefitsController : ControllerBase
    {
        private readonly BenefitCalculateService _service;

        public GoodsBenefitsController(BenefitCalculateService service)
        {
            _service = service;
        }

        [HttpGet("{goodsId}")]
        public void GetAllGoods(int goodsId)
        {
            _service.BenefitGoodsCalculate(goodsId);
        }
    }
}