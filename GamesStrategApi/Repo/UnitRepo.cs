using GamesStrategApi.Interfaces;
using GamesStrategApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesStrategApi.Repo
{
    public class UnitRepo : RepoBase<Unit>, IUnitsRepo
    {
        public UnitRepo(StrategContext context) : base(context)
        {
        }

        // Получить юниты по расе
        public async Task<IEnumerable<Unit>> GetUnitsByRaceAsync(int? raceId)
        {
            var query = _dbSet.AsQueryable();

            if (raceId.HasValue)
            {
                query = query.Where(u => u.RaceId == raceId.Value);
            }
            else
            {
                query = query.Where(u => u.RaceId == null);
            }

            return await query
                .Include(u => u.Race)
                .Include(u => u.UnlockingTech)
                .OrderBy(u => u.Name)
                .ToListAsync();
        }

        // Получить юниты по типу
        public async Task<IEnumerable<Unit>> GetUnitsByTypeAsync(string unitType)
        {
            return await _dbSet
                .Where(u => u.UnitType.ToLower() == unitType.ToLower())
                .Include(u => u.Race)
                .OrderBy(u => u.Name)
                .ToListAsync();
        }

        // Получить мощных юнитов (урон и здоровье выше указанных)
        public async Task<IEnumerable<Unit>> GetPowerfulUnitsAsync(int minDamage, int minHealth)
        {
            return await _dbSet
                .Where(u => u.Damage >= minDamage && u.Health >= minHealth)
                .Include(u => u.Race)
                .OrderByDescending(u => u.Damage)
                .ThenByDescending(u => u.Health)
                .ToListAsync();
        }

        // Получить дешевых юнитов (стоимость ниже указанной)
        public async Task<IEnumerable<Unit>> GetCheapUnitsAsync(int maxCost)
        {
            return await _dbSet
                .Where(u => u.ProductionCost <= maxCost)
                .Include(u => u.Race)
                .OrderBy(u => u.ProductionCost)
                .ToListAsync();
        }

        // Получить юнитов с полной информацией
        public async Task<IEnumerable<Unit>> GetUnitsWithDetailsAsync()
        {
            return await _dbSet
                .Include(u => u.Race)
                .Include(u => u.UnlockingTech)
                .OrderBy(u => u.UnitType)
                .ThenBy(u => u.Name)
                .ToListAsync();
        }

        // Получить юнита по ID с полной информацией
        public async Task<Unit?> GetUnitWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(u => u.Race)
                .Include(u => u.UnlockingTech)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Переопределенный метод получения по ID с загрузкой связанных данных
        public override async Task<Unit?> GetByIdAsync(int id)
        {
            return await GetUnitWithDetailsAsync(id);
        }
    }
}
