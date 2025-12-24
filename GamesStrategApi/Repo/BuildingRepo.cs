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

        public async Task<IEnumerable<Building>> GetBuildingsByTypeAsync(string buildingType)
        {
            return await _dbSet
                .Where(b => b.BuildingType.ToLower() == buildingType.ToLower())
                .Include(b => b.UnlockingTech)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }
        public async Task<IEnumerable<Building>> GetEconomicBuildingsAsync()
        {
            return await _dbSet
                .Where(b => b.BuildingType == "Economic" || b.IncomeBonus > 0)
                .OrderByDescending(b => b.IncomeBonus)
                .ToListAsync();
        }

        public async Task<IEnumerable<Building>> GetAvailableWithoutTechAsync()
        {
            return await _dbSet
                .Where(b => b.UnlockingTechId == null)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Building>> GetExpensiveBuildingsAsync(int minCost)
        {
            return await _dbSet
                .Where(b => b.ProductionCost >= minCost)
                .OrderByDescending(b => b.ProductionCost)
                .ToListAsync();
        }

        public async Task<IEnumerable<Building>> GetBuildingsWithTechAsync()
        {
            return await _dbSet
                .Include(b => b.UnlockingTech)
                .OrderBy(b => b.BuildingType)
                .ThenBy(b => b.Name)
                .ToListAsync();
        }

        public async Task<Building?> GetBuildingWithTechAsync(int id)
        {
            return await _dbSet
                .Include(b => b.UnlockingTech)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public override async Task<Building?> GetByIdAsync(int id)
        {
            return await GetBuildingWithTechAsync(id);
        }
    }
}
