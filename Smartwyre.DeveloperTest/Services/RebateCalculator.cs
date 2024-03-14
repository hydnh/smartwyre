using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public abstract class RebateCalculator : IRebateCalculator
{
    protected readonly Rebate Rebate;

    protected RebateCalculator(Rebate rebate)
    {
        Rebate = rebate;
    }

    public abstract CalculateRebateResult Calculate(CalculateRebateRequest request, Product product);
}