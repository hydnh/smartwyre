using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class FixedRateRebateCalculator : RebateCalculator
{
    public FixedRateRebateCalculator(Rebate rebate) : base(rebate)
    {
    }

    public override CalculateRebateResult Calculate(CalculateRebateRequest request, Product product)
    {
        var result = new CalculateRebateResult();

        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
        {
            result.Success = false;
        }
        else if (Rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
        {
            result.Success = false;
        }
        else
        {
            result.RebateAmount += product.Price * Rebate.Percentage * request.Volume;
            result.Success = true;
        }
        
        return result;
    }
}