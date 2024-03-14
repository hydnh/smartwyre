using FluentAssertions;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class FixedRateRebateCalculatorTests
{
    [Fact]
    public void Calculate_Should_ReturnUnsuccessfulResult_When_IncentiveTypeNotSupported()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0.1m }; // 10% rebate
        var product = new Product { Price = 100, SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 1 };

        var sut = new FixedRateRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void Calculate_Should_ReturnUnsuccessfulResult_When_RebatePercentageIsZero()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0 };
        var product = new Product { Price = 100, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };
        var request = new CalculateRebateRequest { Volume = 1 };

        var sut = new FixedRateRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void Calculate_Should_ReturnUnsuccessfulResult_When_ProductPriceIsZero()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0.1m };
        var product = new Product { Price = 0, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };
        var request = new CalculateRebateRequest { Volume = 1 };

        var sut = new FixedRateRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void Calculate_Should_ReturnUnsuccessfulResult_When_RequestVolumeIsZero()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0.1m };
        var product = new Product { Price = 100, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };
        var request = new CalculateRebateRequest { Volume = 0 };

        var sut = new FixedRateRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void Calculate_Should_ReturnSuccessfulResult_With_CorrectRebateAmount_When_ConditionsMet()
    {
        // Arrange
        var rebatePercentage = 0.1m; // 10% rebate
        var productPrice = 100m;
        var requestVolume = 5;
        var expectedRebate = productPrice * rebatePercentage * requestVolume;

        var rebate = new Rebate { Percentage = rebatePercentage };
        var product = new Product { Price = productPrice, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };
        var request = new CalculateRebateRequest { Volume = requestVolume };

        var sut = new FixedRateRebateCalculator(rebate);

        // Act
        var result = sut.Calculate(request, product);

        // Assert
        result.Success.Should().BeTrue();
        result.RebateAmount.Should().Be(expectedRebate);
    }
}