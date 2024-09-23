using Data.DbContext;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PostgresRepository : IDataRepository
{
    private readonly PostgresDbContext _context;

    public PostgresRepository(PostgresDbContext context)
    {
        _context = context;
    }

    public async Task StoreSensorData(SensorData data)
    {
        await _context.SensorData.AddAsync(data);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<SensorData>> GetRecentSensorData()
    {
        return await _context.SensorData
            .OrderByDescending(d => d.Timestamp)
            .Take(10)
            .ToListAsync();
    }
}