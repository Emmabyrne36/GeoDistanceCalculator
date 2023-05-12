using GeoDistanceCalculator.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoDistanceCalculator.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DataContext()
    {
    }

    public DbSet<CoordinateCalculation> CoordinateCalculations { get; set; }
    public DbSet<Coordinate> Coordinates { get; set; }
}
