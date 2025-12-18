using GamesStrategApi.Models;

namespace GamesStrategApi.Interfaces
{
    public interface IUnitsRepo : IRepository<Unit>
    {
        /// <summary>
        /// Получить юнитов определенной расы
        /// </summary>
        Task<IEnumerable<Unit>> GetUnitsByRaceAsync(int? raceId);

        /// <summary>
        /// Получить юнитов определенного типа
        /// </summary>
        Task<IEnumerable<Unit>> GetUnitsByTypeAsync(string unitType);

        /// <summary>
        /// Получить мощных юнитов (урон и здоровье выше указанных)
        /// </summary>
        Task<IEnumerable<Unit>> GetPowerfulUnitsAsync(int minDamage, int minHealth);

        /// <summary>
        /// Получить дешевых юнитов (стоимость ниже указанной)
        /// </summary>
        Task<IEnumerable<Unit>> GetCheapUnitsAsync(int maxCost);

        /// <summary>
        /// Получить юнитов с полной информацией
        /// </summary>
        Task<IEnumerable<Unit>> GetUnitsWithDetailsAsync();

        /// <summary>
        /// Получить юнита по ID с полной информацией
        /// </summary>
        Task<Unit?> GetUnitWithDetailsAsync(int id);
    }
}
