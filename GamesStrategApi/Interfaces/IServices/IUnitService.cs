using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Interfaces.IServices
{
    public interface IUnitService
    {
        /// <summary>
        /// Получить все юниты
        /// </summary>
        Task<List<UnitDto>> GetAllAsync();

        /// <summary>
        /// Получить юнит по ID
        /// </summary>
        Task<UnitDto?> GetByIdAsync(int id);

        /// <summary>
        /// Получить юниты определенной расы
        /// </summary>
        Task<List<UnitDto>> GetByRaceAsync(int raceId);

        /// <summary>
        /// Получить юниты определенного типа
        /// </summary>
        Task<List<UnitDto>> GetByTypeAsync(string unitType);

        /// <summary>
        /// Получить сильных юнитов (урон > 50, здоровье > 100)
        /// </summary>
        Task<List<UnitDto>> GetStrongUnitsAsync();

        /// <summary>
        /// Получить дешевых юнитов (стоимость < 100)
        /// </summary>
        Task<List<UnitDto>> GetCheapUnitsAsync();

        /// <summary>
        /// Создать нового юнита
        /// </summary>
        Task<UnitDto> CreateAsync(CreateUnitRequest request);

        /// <summary>
        /// Обновить существующего юнита
        /// </summary>
        Task<UnitDto?> UpdateAsync(int id, UpdateUnitRequest request);

        /// <summary>
        /// Удалить юнита по ID
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Рассчитать стоимость армии на основе количества юнитов
        /// </summary>
        Task<int> CalculateArmyCostAsync(Dictionary<int, int> unitQuantities);
    }
}
