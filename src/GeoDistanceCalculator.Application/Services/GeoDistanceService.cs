using GeoDistanceCalculator.Application.Interfaces;
using GeoDistanceCalculator.Domain.Enum;

namespace GeoDistanceCalculator.Application.Services;

public class GeoDistanceService : IGeoDistanceService
{
    private const double EarthRadiusKm = 6371.0;


    public double CalculateDistance(CoordinateDto coordinate1, CoordinateDto coordinate2, string units = MeasuringUnits.Kilometers)
    {
        // Haversine formula
        var dLat = ToRadians(coordinate2.Latitude - coordinate1.Latitude);
        var dLon = ToRadians(coordinate2.Longitude - coordinate1.Longitude);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(coordinate1.Latitude)) * Math.Cos(ToRadians(coordinate2.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        var distance = EarthRadiusKm * c;

        if (string.Equals(units, MeasuringUnits.Miles))
        {
            distance *= 0.621371;
        }

        return Math.Round(distance, 6);
    }

    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}
