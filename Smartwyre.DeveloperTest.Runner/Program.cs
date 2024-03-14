using System;
using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

internal class Program
{
    private static async Task Main()
    {
        var request = CaptureInputs();

        var rebateService = CreateRebateService();

        var result = await rebateService.Calculate(request);

        DisplayResult(result);

        Console.Read();
    }

    private static void DisplayResult(CalculateRebateResult result)
    {
        Console.WriteLine();
        Console.WriteLine($"Rebate Amount: {result.RebateAmount}");
    }

    private static CalculateRebateRequest CaptureInputs()
    {
        Console.Write("Enter Rebate Identifier: ");
        var rebateIdentifier = Console.ReadLine();

        Console.Write("Enter Product Identifier: ");
        var productIdentifier = Console.ReadLine();

        Console.Write("Enter Volume: ");
        decimal volume;

        while (!decimal.TryParse(Console.ReadLine(), out volume) || volume < 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid positive number for Volume.");
        }

        return new CalculateRebateRequest
        {
            RebateIdentifier = rebateIdentifier,
            ProductIdentifier = productIdentifier,
            Volume = volume
        };
    }

    private static IRebateService CreateRebateService()
    {
        return new RebateService(new RebateCalculatorFactory(), new ProductDataStore(), new RebateDataStore(), new RebateCalculationStore());
    }
}
