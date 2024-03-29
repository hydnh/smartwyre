﻿using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public interface IProductWriteStore
{
    Task<Product> GetProduct(string productIdentifier);
}