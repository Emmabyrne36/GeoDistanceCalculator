using FluentAssertions;
using GeoDistanceCalculator.Application.CommandHandlers;
using GeoDistanceCalculator.Application.Commands;
using GeoDistanceCalculator.Application.Dtos;
using GeoDistanceCalculator.Application.Interfaces;
using GeoDistanceCalculator.Data.Interfaces;
using GeoDistanceCalculator.Domain.Models;
using Moq;

namespace GeoDistanceCalculator.Application.UnitTests.CommandHandlerTests;

public class CalculateDistanceCommandHandlerTests
{
    private Mock<IRepository> _repository;
    private Mock<IGeoDistanceService> _geoDistanceService;

    public CalculateDistanceCommandHandlerTests()
    {
        CreateMocks();
    }

    [Fact]
    public void CalculateDistanceCommandHandler_NullGeoDistanceService_ThrowsArgumentNullException()
    {
        // Arrange

        // Act
        Action result = () => new CalculateDistanceCommandHandler(null, null);

        // Assert
        result.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void CalculateDistanceCommandHandler_NullRepository_ThrowsArgumentNullException()
    {
        // Arrange

        // Act
        Action result = () => new CalculateDistanceCommandHandler(_geoDistanceService.Object, null);

        // Assert
        result.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public async Task Handle_MethodCalled_DistanceCalculatedSuccessfully()
    {
        // Arrange
        var firstCoordinate = new CoordinateDto(53.297975, -6.372663);
        var secondCoordinate = new CoordinateDto(41.385101, -81.440440);
        var request = new CalculateDistanceRequestDto
        {
            FirstCoordinate = firstCoordinate,
            SecondCoordinate = secondCoordinate
        };
        var command = new CalculateDistanceCommand(request);
        var handler = new CalculateDistanceCommandHandler(_geoDistanceService.Object, _repository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        _repository.Verify(x => x.Create(It.IsAny<CoordinateCalculation>()), Times.Once);
        result.Should().NotBeNull();
        result.Distance.Should().Be(5000);
    }


    private void CreateMocks()
    {
        _repository = new Mock<IRepository>();
        _geoDistanceService = new Mock<IGeoDistanceService>();

        _geoDistanceService.Setup(x => x.CalculateDistance(It.IsAny<CoordinateDto>(), It.IsAny<CoordinateDto>(), It.IsAny<string>()))
            .Returns(5000);
    }
}
