using SuperMarket.Infrastructure.Application;

namespace SuperMarket.Services.Benefit_Calculates.Contracts
{
    public interface BenefitCalculateService :Service
    {
        int BenefitGoodsCalculate(int goodsId);
        int BenefitCategoryCalculate(int categoryId);
        
        
    }
}
