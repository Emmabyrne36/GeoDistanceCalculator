namespace GeoDistanceCalculator.Domain.Models;

public class Coordinate
{
    public Guid Id { get; set; }
    public double Latitude { get; }
    public double Longitude { get; }

    public Coordinate()
    {
    }

    public Coordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}