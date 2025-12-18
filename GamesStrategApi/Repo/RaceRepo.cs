using GamesStrategApi.Interfaces;
using GamesStrategApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesStrategApi.Repo
{
    public class RaceRepo : RepoBase<Race>, IRaceRepo
    {
        public RaceRepo(StrategContext context) : base(context)
        {
        }

        // Получить только играбельные расы
        public async Task<IEnumerable<Race>> GetPlayableRacesAsync()
        {
            return await _dbSet
                .Where(r => r.IsPlayable)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        // Получить расы по типу родного мира
        public async Task<IEnumerable<Race>> GetRacesByHomeWorldTypeAsync(string homeWorldType)
        {
            return await _dbSet
                .Where(r => r.HomeWorldType.ToLower() == homeWorldType.ToLower())
                .ToListAsync();
        }

        // Получить все расы с их юнитами
        public async Task<IEnumerable<Race>> GetRacesWithUnitsAsync()
        {
            return await _dbSet
                .Include(r => r.Units)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        // Получить расу по ID с юнитами
        public async Task<Race?> GetRaceWithUnitsAsync(int id)
        {
            return await _dbSet
                .Include(r => r.Units)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // Переопределенный метод получения по ID с загрузкой юнитов
        public override async Task<Race?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(r => r.Units)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
