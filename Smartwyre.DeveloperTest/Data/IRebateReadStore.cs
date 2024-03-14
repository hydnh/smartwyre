using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public interface IRebateReadStore
{
    Task<Rebate> GetRebate(string identifier);
}