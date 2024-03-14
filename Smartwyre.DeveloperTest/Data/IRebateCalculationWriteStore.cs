using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public interface IRebateCalculationWriteStore
{
    Task Store(RebateCalculation rebateCalculation);
}