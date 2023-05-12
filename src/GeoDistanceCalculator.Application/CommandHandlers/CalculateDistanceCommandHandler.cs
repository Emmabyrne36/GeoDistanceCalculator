using GeoDistanceCalculator.Application.Commands;
using GeoDistanceCalculator.Application.Dtos;
using GeoDistanceCalculator.Application.Interfaces;
using GeoDistanceCalculator.Data.Interfaces;
using GeoDistanceCalculator.Domain.Enum;
using GeoDistanceCalculator.Domain.Models;
using MediatR;

namespace GeoDistanceCalculator.Application.CommandHandlers;

public class CalculateDistanceCommandHandler : IRequestHandler<CalculateDistanceCommand, CalculateDistanceResponseDto>
{
    private readonly IGeoDistanceService _geoDistanceService;
    private readonly IRepository _repository;

    public CalculateDistanceCommandHandler(IGeoDistanceService geoDistanceService, IRepository repository)
    {
        _geoDistanceService = geoDistanceService ?? throw new ArgumentNullException(nameof(geoDistanceService));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<CalculateDistanceResponseDto> Handle(CalculateDistanceCommand request, CancellationToken cancellationToken)
    {
        var distance = _geoDistanceService.CalculateDistance(request.FirstCoordinate, request.SecondCoordinate, request.MeasuringUnits);

        var coordinateCalculation = CreateCoordinateCalculation(request, distance);
        await _repository.Create(coordinateCalculation);

        var response = new CalculateDistanceResponseDto
        {
            Distance = distance,
            Units = coordinateCalculation.Units
        };

        return response;
    }

    private static CoordinateCalculation CreateCoordinateCalculation(CalculateDistanceCommand request, double distance)
    {
        return new CoordinateCalculation
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Distance = distance,
            Units = request.MeasuringUnits ?? MeasuringUnits.Kilometers,
            Coordinate1 = new Coordinate(request.FirstCoordinate.Latitude, request.FirstCoordinate.Longitude),
            Coordinate2 = new Coordinate(request.SecondCoordinate.Latitude, request.SecondCoordinate.Longitude)
        };
    }
}
