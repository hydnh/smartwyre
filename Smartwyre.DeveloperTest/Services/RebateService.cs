using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IProductReadStore _productReadStore;
    private readonly IRebateReadStore _rebateReadStore;
    private readonly IRebateCalculationWriteStore _rebateCalculationWriteStore;
    private readonly IRebateCalculatorFactory _rebateCalculatorFactory;

    public RebateService(
        IRebateCalculatorFactory rebateCalculatorFactory,
        IProductReadStore productReadStore,
        IRebateReadStore rebateDataStore, 
        IRebateCalculationWriteStore rebateCalculationWriteStore
    )
    {
        _rebateCalculatorFactory = rebateCalculatorFactory;
        _productReadStore = productReadStore;
        _rebateReadStore = rebateDataStore;
        _rebateCalculationWriteStore = rebateCalculationWriteStore;
    }

    public async Task<CalculateRebateResult> Calculate(CalculateRebateRequest request)
    {
        var rebate = await _rebateReadStore.GetRebate(request.RebateIdentifier);

        if (rebate is null)
        {
            return new CalculateRebateResult();
        }

        var product = await _productReadStore.GetProduct(request.ProductIdentifier);

        if (product is null)
        {
            return new CalculateRebateResult();
        }

        var result = Calculate(request, rebate, product);

        if (result.Success)
        {
            var rebateCalculation = new RebateCalculation
            {
                RebateIdentifier = rebate.Identifier,
                Amount = result.RebateAmount,
                IncentiveType = rebate.Incentive
            };

            await _rebateCalculationWriteStore.Store(rebateCalculation);
        }

        return result;
    }

    private CalculateRebateResult Calculate(CalculateRebateRequest request, Rebate rebate, Product product)
    {
        var calculator = _rebateCalculatorFactory.Create(rebate);

        return calculator.Calculate(request, product);
    }
}
