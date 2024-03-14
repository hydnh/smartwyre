using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests
{
    private readonly IRebateCalculatorFactory _mockRebateCalculatorFactory = Substitute.For<IRebateCalculatorFactory>();
    private readonly IRebateReadStore _mockRebateReadStore = Substitute.For<IRebateReadStore>();
    private readonly IRebateCalculationWriteStore _mockRebateCalculationWriteStore = Substitute.For<IRebateCalculationWriteStore>();
    private readonly IProductReadStore _mockProductReadStore = Substitute.For<IProductReadStore>();

    [Fact]
    public async Task Calculate_Should_ReturnUnsuccessfulResult_When_RebateIsNotFound()
    {
        // Arrange
        _mockRebateReadStore
            .GetRebate(Arg.Any<string>())
            .Returns(null as Rebate);

        var request = new CalculateRebateRequest();

        var expectedResult = new CalculateRebateResult();

        var sut = GetSut();

        // Act
        var result = await sut.Calculate(request);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Calculate_Should_ReturnUnsuccessfulResult_When_ProductIsNotFound()
    {
        // Arrange
        _mockRebateReadStore
            .GetRebate(Arg.Any<string>())
            .Returns(new Rebate());

        _mockProductReadStore
            .GetProduct(Arg.Any<string>())
            .Returns(null as Product);

        var request = new CalculateRebateRequest();

        var expectedResult = new CalculateRebateResult();

        var sut = GetSut();

        // Act
        var result = await sut.Calculate(request);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Fact]
    public async Task Calculate_Should_StoreRebateCalculation_And_ReturnSuccessfulResult_When_ProductAndRebateAreFound()
    {
        // Arrange
        var mockedCalculator = Substitute.For<IRebateCalculator>();

        _mockRebateReadStore
            .GetRebate(Arg.Any<string>())
            .Returns(new Rebate());

        _mockProductReadStore
            .GetProduct(Arg.Any<string>())
            .Returns(new Product());

        _mockRebateCalculatorFactory
            .Create(Arg.Any<Rebate>())
            .Returns(mockedCalculator);

       
        mockedCalculator
            .Calculate(Arg.Any<CalculateRebateRequest>(), Arg.Any<Product>())
            .Returns(new CalculateRebateResult { Success = true, RebateAmount = 100m });

        var request = new CalculateRebateRequest();

        var expectedResult = new CalculateRebateResult
        {
            Success = true,
            RebateAmount = 100m
        };

        var sut = GetSut();

        // Act
        var result = await sut.Calculate(request);

        // Assert
        await _mockRebateCalculationWriteStore
            .Received(1)
            .Store(Arg.Is<RebateCalculation>(x => x.Amount == 100m));

        result.Should().BeEquivalentTo(expectedResult);
    }

    private RebateService GetSut() => new(_mockRebateCalculatorFactory, _mockProductReadStore, _mockRebateReadStore, _mockRebateCalculationWriteStore);
}
