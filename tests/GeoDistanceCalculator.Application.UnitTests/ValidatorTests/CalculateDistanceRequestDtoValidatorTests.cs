using FluentAssertions;
using GeoDistanceCalculator.Application.Dtos;
using GeoDistanceCalculator.Application.Validators;

namespace GeoDistanceCalculator.Application.UnitTests.ValidatorTests;

public class CalculateDistanceRequestDtoValidatorTests
{
    [Fact]
    public void CalculateDistanceRequestDtoValidator_EmptyCoordinate_ErrorsFound()
    {
        // Arrange
        var dto = new CalculateDistanceRequestDto
        {
            FirstCoordinate = null,
            SecondCoordinate = null
        };
        var validator = new CalculateDistanceRequestDtoValidator();

        // Act
        var result = validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorMessage.Should().Be(GetErrorMessage(nameof(CalculateDistanceRequestDto.FirstCoordinate)));
        result.Errors[1].ErrorMessage.Should().Be(GetErrorMessage(nameof(CalculateDistanceRequestDto.SecondCoordinate)));
    }

    [Fact]
    public void CalculateDistanceRequestDtoValidator_ValidRequest_NoErrorsFound()
    {
        // Arrange
        var firstCoordinate = new CoordinateDto(53.297975, -6.372663);
        var secondCoordinate = new CoordinateDto(41.385101, -81.440440);
        var dto = new CalculateDistanceRequestDto
        {
            FirstCoordinate = firstCoordinate,
            SecondCoordinate = secondCoordinate
        };
        var validator = new CalculateDistanceRequestDtoValidator();

        // Act
        var result = validator.Validate(dto);

        // Assert
        result.Errors.Count.Should().Be(0);
        result.IsValid.Should().BeTrue();
    }

    private static string GetErrorMessage(string name) => $"A valid value for {name} must be provided";
}
