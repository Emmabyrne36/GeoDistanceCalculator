using GeoDistanceCalculator.Application.Dtos;
using MediatR;

namespace GeoDistanceCalculator.Application.Commands;

public class CalculateDistanceCommand : IRequest<CalculateDistanceResponseDto>
{
    public CoordinateDto FirstCoordinate { get; set; }
    public CoordinateDto SecondCoordinate { get; set; }
    public string MeasuringUnits { get; set; }

    public CalculateDistanceCommand(CalculateDistanceRequestDto request)
    {
        FirstCoordinate = request.FirstCoordinate;
        SecondCoordinate = request.SecondCoordinate;
        MeasuringUnits = request.Units;
    }
}
