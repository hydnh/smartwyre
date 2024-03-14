using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductReadStore, IProductWriteStore
{
    public Task<Product> GetProduct(string identifier)
    {
        return Task.FromResult(new Product());
    }

    public Task StoreProduct(Product product)
    {
        return Task.CompletedTask;
    }
}
