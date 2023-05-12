using GeoDistanceCalculator.Data.Interfaces;
using GeoDistanceCalculator.Domain.Models;

namespace GeoDistanceCalculator.Data.Repositories;

/// <summary>
/// This repository is used for mocking the database connection in the integration tests.
/// It can also be used in Program.cs if the database integration is not required.
/// </summary>
public class MockRepository : IRepository
{
    public Task Create(CoordinateCalculation coordinateCalculation) => Task.CompletedTask;

    public Task<CoordinateCalculation> GetCoordinteCalculationById(Guid id) => Task.FromResult(new CoordinateCalculation());
}