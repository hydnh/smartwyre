using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateCalculationStore : IRebateCalculationWriteStore, IRebateCalculationReadStore
{
    public Task Store(RebateCalculation rebateCalculation)
    {
        // TODO: Implement actual storage in db
        return Task.CompletedTask;
    }

    public Task<RebateCalculation> Get(string identifier)
    {
        // TODO: Implement actual retrieval from db
        return Task.FromResult(new RebateCalculation());
    }
}