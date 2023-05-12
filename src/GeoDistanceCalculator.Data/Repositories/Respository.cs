using GeoDistanceCalculator.Data.Interfaces;
using GeoDistanceCalculator.Domain.Models;

namespace GeoDistanceCalculator.Data.Repositories;

public class Respository : IRepository
{
    private readonly DataContext _dataContext;

    public Respository(DataContext dataContext)
    {
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
    }

    public async Task<CoordinateCalculation> GetCoordinteCalculationById(Guid id) =>
        await _dataContext.CoordinateCalculations.FindAsync(id);

    public async Task Create(CoordinateCalculation coordinateCalculation)
    {
        await _dataContext.CoordinateCalculations.AddAsync(coordinateCalculation);
        await _dataContext.SaveChangesAsync();
    }
}
