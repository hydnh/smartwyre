using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public interface IRebateWriteStore
{
    Task<Rebate> GetRebate(string identifier);

    Task StoreRebate(Rebate rebate);
}