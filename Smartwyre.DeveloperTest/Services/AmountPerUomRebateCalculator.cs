using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class AmountPerUomRebateCalculator : RebateCalculator
{
    public AmountPerUomRebateCalculator(Rebate rebate) : base(rebate)
    {
    }

    public override CalculateRebateResult Calculate(CalculateRebateRequest request, Product product)
    {
        var result = new CalculateRebateResult();

        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
        {
            result.Success = false;
        }
        else if (Rebate.Amount == 0 || request.Volume == 0)
        {
            result.Success = false;
        }
        else
        {
            result.RebateAmount += Rebate.Amount * request.Volume;
            result.Success = true;
        }

        return result;
    }
}