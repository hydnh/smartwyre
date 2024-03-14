using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public interface IProductReadStore
{
    Task<Product> GetProduct(string identifier);

    Task StoreProduct(Product rebate);
}