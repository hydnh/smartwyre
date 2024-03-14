using FluentAssertions;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class FixedCashAmountRebateCalculatorTests
{
    [Fact]
    public void Calculate_Should_ReturnUnsuccessfulResult_When_IncentiveTypeNotSupported()
    {
        // Arrange
        var rebate = new Rebate { Amount = 100 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom }; // No FixedCashAmount support
        var request = new CalculateRebateRequest();

        var sut = new FixedCashAmountRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeFalse();
        result.RebateAmount.Should().Be(0);
    }

    [Fact]
    public void Calculate_Should_ReturnUnsuccessfulResult_When_RebateAmountIsZero()
    {
        // Arrange
        var rebate = new Rebate { Amount = 0 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var request = new CalculateRebateRequest();

        var sut = new FixedCashAmountRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeFalse();
        result.RebateAmount.Should().Be(0);
    }

    [Fact]
    public void Calculate_Should_ReturnSuccessfulResult_With_FixedRebateAmount_When_ConditionsMet()
    {
        // Arrange
        var fixedRebateAmount = 100m;
        var rebate = new Rebate { Amount = fixedRebateAmount };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var request = new CalculateRebateRequest();

        var sut = new FixedCashAmountRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeTrue();
        result.RebateAmount.Should().Be(fixedRebateAmount);
    }
}