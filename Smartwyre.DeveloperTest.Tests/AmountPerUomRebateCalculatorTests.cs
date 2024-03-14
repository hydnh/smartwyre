using FluentAssertions;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class AmountPerUomRebateCalculatorTests
{
    [Fact]
    public void Calculate_Should_ReturnUnsuccessfulResult_When_IncentiveTypeNotSupported()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var request = new CalculateRebateRequest { Volume = 5 };

        var sut = new AmountPerUomRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void Calculate_Should_ReturnUnsuccessfulResult_When_RebateAmountIsZero()
    {
        // Arrange
        var rebate = new Rebate { Amount = 0 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 5 };

        var sut = new AmountPerUomRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void Calculate_Should_ReturnUnsuccessfulResult_When_RequestVolumeIsZero()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 0 };

        var sut = new AmountPerUomRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void Calculate_Should_ReturnSuccessfulResult_With_CorrectRebateAmount_When_ConditionsMet()
    {
        // Arrange
        var rebateAmount = 10m;
        var requestVolume = 5;
        var expectedRebate = rebateAmount * requestVolume;

        var rebate = new Rebate { Amount = rebateAmount };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = requestVolume };

        var sut = new AmountPerUomRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeTrue();
        result.RebateAmount.Should().Be(expectedRebate);
    }
}