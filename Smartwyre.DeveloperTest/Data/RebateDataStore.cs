using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateReadStore, IRebateWriteStore
{
    public Task<Rebate> GetRebate(string identifier)
    {
        // Access database to retrieve account, code removed for brevity 
        return Task.FromResult(new Rebate());
    }

    public Task StoreRebate(Rebate rebate)
    {
        // Update account in database, code removed for brevity
        return Task.CompletedTask;
    }
}
