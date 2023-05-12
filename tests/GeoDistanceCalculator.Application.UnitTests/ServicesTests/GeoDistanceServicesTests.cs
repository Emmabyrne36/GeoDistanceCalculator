using FluentAssertions;
using GeoDistanceCalculator.Application.Services;
using GeoDistanceCalculator.Domain.Enum;

namespace GeoDistanceCalculator.Application.UnitTests.ServicesTests;

public class GeoDistanceServicesTests
{
    [Theory]
    [InlineData(41.385101, -81.440440, 5536.338682)]
    [InlineData(53.360821, -6.251155, 10.674408)]
    public void CalculateDistance_MethodCalled_ResultReturned(double latitude2, double longitude2, double expectedResult)
    {
        // Arrange
        var firstCoordinate = new CoordinateDto(53.297975, -6.372663);
        var secondCoordinate = new CoordinateDto(latitude2, longitude2);

        // Act
        var result = new GeoDistanceService().CalculateDistance(firstCoordinate, secondCoordinate);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void CalculateDistance_MethodCalledWithMilesAsTheUnits_ResultReturnedInMiles()
    {
        // Arrange
        var firstCoordinate = new CoordinateDto(53.297975, -6.372663);
        var secondCoordinate = new CoordinateDto(41.385101, -81.440440);

        // Act
        var result = new GeoDistanceService().CalculateDistance(firstCoordinate, secondCoordinate, MeasuringUnits.Miles);

        // Assert
        result.Should().Be(3440.120303);
    }
}
