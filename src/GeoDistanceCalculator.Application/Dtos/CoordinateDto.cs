using System.ComponentModel.DataAnnotations;

namespace GeoDistanceCalculator.Application;

public class CoordinateDto
{
    [Range(-90, 90)]
    public double Latitude { get; }
    [Range(-180, 180)]
    public double Longitude { get; }

    public CoordinateDto(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
