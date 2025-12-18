using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Interfaces.IServices
{
    public interface ITechService
    {
        /// <summary>
        /// Получить все технологии
        /// </summary>
        Task<List<TechDto>> GetAllAsync();

        /// <summary>
        /// Получить технологию по ID
        /// </summary>
        Task<TechDto?> GetByIdAsync(int id);

        /// <summary>
        /// Получить технологии определенного уровня
        /// </summary>
        Task<List<TechDto>> GetByTierAsync(int tier);

        /// <summary>
        /// Получить стартовые технологии (уровень 1 без требований)
        /// </summary>
        Task<List<TechDto>> GetStartingTechsAsync();

        /// <summary>
        /// Создать новую технологию
        /// </summary>
        Task<TechDto> CreateAsync(CreateTechRequest request);

        /// <summary>
        /// Обновить существующую технологию
        /// </summary>
        Task<TechDto?> UpdateAsync(int id, UpdateTechRequest request);

        /// <summary>
        /// Удалить технологию по ID
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Рассчитать общую стоимость исследования нескольких технологий
        /// </summary>
        Task<int> CalculateTotalCostAsync(List<int> techIds);
    }
}
