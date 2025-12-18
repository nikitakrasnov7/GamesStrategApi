using GamesStrategApi.Interfaces;
using GamesStrategApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesStrategApi.Repo
{
    public class CelestialBodyRepo : RepoBase<CelestialBody> ,  ICelestialBodyRepo
    {
        public CelestialBodyRepo(StrategContext context) : base(context)
        {
        }

        // Получить небесные тела по типу
        public async Task<IEnumerable<CelestialBody>> GetByTypeAsync(string type)
        {
            return await _dbSet
                .Where(c => c.Type.ToLower() == type.ToLower())
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        // Получить богатые ресурсами небесные тела
        public async Task<IEnumerable<CelestialBody>> GetRichBodiesAsync(int minRichness)
        {
            return await _dbSet
                .Where(c => c.ResourceRichness >= minRichness)
                .OrderByDescending(c => c.ResourceRichness)
                .ToListAsync();
        }

        // Получить небесные тела в указанной области
        public async Task<IEnumerable<CelestialBody>> GetBodiesInAreaAsync(int x, int y, int radius)
        {
            return await _dbSet
                .Where(c => Math.Abs(c.PositionX - x) <= radius &&
                           Math.Abs(c.PositionY - y) <= radius)
                .OrderBy(c => Math.Abs(c.PositionX - x) + Math.Abs(c.PositionY - y))
                .ToListAsync();
        }

        // Получить небесное тело по координатам
        public async Task<CelestialBody?> GetByCoordinatesAsync(int x, int y)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.PositionX == x && c.PositionY == y);
        }

        // Проверить, занята ли позиция
        public async Task<bool> IsPositionOccupiedAsync(int x, int y, int? excludeId = null)
        {
            var query = _dbSet.Where(c => c.PositionX == x && c.PositionY == y);

            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
