using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class FixedCashAmountRebateCalculator : RebateCalculator
{
    public FixedCashAmountRebateCalculator(Rebate rebate) : base(rebate)
    {
    }

    public override CalculateRebateResult Calculate(CalculateRebateRequest request, Product product)
    {
        var result = new CalculateRebateResult();
        
        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
        {
            result.Success = false;
        }
        else if (Rebate.Amount == 0)
        {
            result.Success = false;
        }
        else
        {
            result.RebateAmount = Rebate.Amount;
            result.Success = true;
        }
        
        return result;
    }
}