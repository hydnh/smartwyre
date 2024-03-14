namespace Smartwyre.DeveloperTest.Types;

public record CalculateRebateRequest
{
    public string RebateIdentifier { get; init; } = string.Empty;

    public string ProductIdentifier { get; init; } = string.Empty;

    public decimal Volume { get; init; }
}
