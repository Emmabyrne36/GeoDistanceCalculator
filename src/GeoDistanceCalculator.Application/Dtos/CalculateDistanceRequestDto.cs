using System.ComponentModel.DataAnnotations;

namespace GeoDistanceCalculator.Application.Dtos;

public class CalculateDistanceRequestDto
{
    [Required]
    public CoordinateDto FirstCoordinate { get; set; }
    [Required]
    public CoordinateDto SecondCoordinate { get; set; }
    public string Units { get; set; }
}
