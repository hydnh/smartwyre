using System;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateCalculatorFactory : IRebateCalculatorFactory
{
    public IRebateCalculator Create(Rebate rebate)
    {
        return rebate.Incentive switch
        {
            IncentiveType.FixedRateRebate => new FixedRateRebateCalculator(rebate),
            IncentiveType.AmountPerUom => new AmountPerUomRebateCalculator(rebate),
            IncentiveType.FixedCashAmount => new FixedCashAmountRebateCalculator(rebate),
            _ => throw new ArgumentOutOfRangeException(nameof(rebate.Incentive), rebate.Incentive, $"There are no calculators associated with the IncentiveType: {rebate.Incentive}.")
        };
    }
}