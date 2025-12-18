using GamesStrategApi.Interfaces;
using GamesStrategApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesStrategApi.Repo
{
    public class TechRepo : RepoBase<Tech> , ITechRepo
    {
        public TechRepo(StrategContext context) : base(context)
        {
        }

        // Получить технологии по уровню
        public async Task<IEnumerable<Tech>> GetTechsByTierAsync(int tier)
        {
            return await _dbSet
                .Where(t => t.Tier == tier)
                .OrderBy(t => t.ResearchCost)
                .ToListAsync();
        }

        // Получить стартовые технологии (уровень 1 без требований)
        public async Task<IEnumerable<Tech>> GetAvailableStartingTechsAsync()
        {
            return await _dbSet
                .Where(t => t.Tier == 1 && t.RequiredTechId == null)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        // Получить полное дерево технологий
        public async Task<IEnumerable<Tech>> GetTechTreeAsync()
        {
            return await _dbSet
                .Include(t => t.RequiredTech)
                .Include(t => t.UnlockedTechs)
                .Include(t => t.Units)
                .Include(t => t.Buildings)
                .OrderBy(t => t.Tier)
                .ThenBy(t => t.ResearchCost)
                .ToListAsync();
        }

        // Получить технологии с зависимостями
        public async Task<IEnumerable<Tech>> GetTechsWithDependenciesAsync()
        {
            return await _dbSet
                .Include(t => t.RequiredTech)
                .Include(t => t.UnlockedTechs)
                .ToListAsync();
        }

        // Получить технологию с разблокируемыми элементами
        public async Task<Tech?> GetTechWithUnlocksAsync(int id)
        {
            return await _dbSet
                .Include(t => t.RequiredTech)
                .Include(t => t.UnlockedTechs)
                .Include(t => t.Units)
                .Include(t => t.Buildings)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // Переопределенный метод получения по ID с зависимостями
        public override async Task<Tech?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(t => t.RequiredTech)
                .Include(t => t.Units)
                .Include(t => t.Buildings)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
