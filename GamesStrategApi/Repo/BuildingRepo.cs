using GamesStrategApi.Interfaces;
using GamesStrategApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesStrategApi.Repo
{
    public class BuildingRepo : RepoBase<Building>, IBuildRepo
    {
        public BuildingRepo(StrategContext context) : base(context)
        {
        }

        // Получить здания определенного типа
        public async Task<IEnumerable<Building>> GetBuildingsByTypeAsync(string buildingType)
        {
            return await _dbSet
                .Where(b => b.BuildingType.ToLower() == buildingType.ToLower())
                .Include(b => b.UnlockingTech)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        // Получить экономические здания или здания с бонусом дохода
        public async Task<IEnumerable<Building>> GetEconomicBuildingsAsync()
        {
            return await _dbSet
                .Where(b => b.BuildingType == "Economic" || b.IncomeBonus > 0)
                .OrderByDescending(b => b.IncomeBonus)
                .ToListAsync();
        }

        // Получить здания без требований технологий
        public async Task<IEnumerable<Building>> GetAvailableWithoutTechAsync()
        {
            return await _dbSet
                .Where(b => b.UnlockingTechId == null)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        // Получить дорогие здания (стоимость выше указанной)
        public async Task<IEnumerable<Building>> GetExpensiveBuildingsAsync(int minCost)
        {
            return await _dbSet
                .Where(b => b.ProductionCost >= minCost)
                .OrderByDescending(b => b.ProductionCost)
                .ToListAsync();
        }

        // Получить все здания с информацией о технологиях
        public async Task<IEnumerable<Building>> GetBuildingsWithTechAsync()
        {
            return await _dbSet
                .Include(b => b.UnlockingTech)
                .OrderBy(b => b.BuildingType)
                .ThenBy(b => b.Name)
                .ToListAsync();
        }

        // Получить здание по ID с информацией о технологии
        public async Task<Building?> GetBuildingWithTechAsync(int id)
        {
            return await _dbSet
                .Include(b => b.UnlockingTech)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        // Переопределенный метод получения по ID с загрузкой технологии
        public override async Task<Building?> GetByIdAsync(int id)
        {
            return await GetBuildingWithTechAsync(id);
        }
    }
}
