using GeoDistanceCalculator.Domain.Enum;

namespace GeoDistanceCalculator.Domain.Models;

public class CoordinateCalculation
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Coordinate Coordinate1 { get; set; }
    public Coordinate Coordinate2 { get; set; }
    public double Distance { get; set; }
    public string Units { get; set; } = MeasuringUnits.Kilometers;
}
