using GeoDistanceCalculator.Domain.Models;

namespace GeoDistanceCalculator.Data.Interfaces;

public interface IRepository
{
    Task<CoordinateCalculation> GetCoordinteCalculationById(Guid id);
    Task Create(CoordinateCalculation coordinateCalculation);
}
