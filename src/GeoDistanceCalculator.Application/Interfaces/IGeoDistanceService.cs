using GeoDistanceCalculator.Domain.Enum;

namespace GeoDistanceCalculator.Application.Interfaces;

public interface IGeoDistanceService
{
    double CalculateDistance(CoordinateDto coordinate1, CoordinateDto coordinate2, string units = MeasuringUnits.Kilometers);
}
