using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateCalculator
{
    CalculateRebateResult Calculate(CalculateRebateRequest request, Product product);
}