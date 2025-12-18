using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Interfaces.IServices
{
    public interface IBuildService
    {
        /// <summary>
        /// Получить все здания
        /// </summary>
        Task<List<BuildDto>> GetAllAsync();

        /// <summary>
        /// Получить здание по ID
        /// </summary>
        Task<BuildDto?> GetByIdAsync(int id);

        /// <summary>
        /// Получить здания определенного типа
        /// </summary>
        Task<List<BuildDto>> GetByTypeAsync(string buildingType);

        /// <summary>
        /// Получить экономические здания
        /// </summary>
        Task<List<BuildDto>> GetEconomicBuildingsAsync();

        /// <summary>
        /// Получить здания, доступные без исследования технологий
        /// </summary>
        Task<List<BuildDto>> GetAvailableWithoutTechAsync();

        /// <summary>
        /// Получить дорогие здания (стоимость выше указанной)
        /// </summary>
        Task<List<BuildDto>> GetExpensiveBuildingsAsync(int minCost = 300);

        /// <summary>
        /// Создать новое здание
        /// </summary>
        Task<BuildDto> CreateAsync(CreateBuildRequest request);

        /// <summary>
        /// Обновить существующее здание
        /// </summary>
        Task<BuildDto?> UpdateAsync(int id, UpdateBuildRequest request);

        /// <summary>
        /// Удалить здание по ID
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Рассчитать период окупаемости здания (в месяцах)
        /// </summary>
        Task<int> CalculatePaybackPeriodAsync(int buildingId);
    }
}
