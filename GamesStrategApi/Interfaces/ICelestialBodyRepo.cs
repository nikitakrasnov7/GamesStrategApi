using GamesStrategApi.Models;

namespace GamesStrategApi.Interfaces
{
    public interface ICelestialBodyRepo : IRepository<CelestialBody>
    {
        /// <summary>
        /// Получить небесные тела определенного типа
        /// </summary>
        Task<IEnumerable<CelestialBody>> GetByTypeAsync(string type);

        /// <summary>
        /// Получить богатые ресурсами небесные тела
        /// </summary>
        Task<IEnumerable<CelestialBody>> GetRichBodiesAsync(int minRichness);

        /// <summary>
        /// Получить небесные тела в указанной области
        /// </summary>
        Task<IEnumerable<CelestialBody>> GetBodiesInAreaAsync(int x, int y, int radius);

        /// <summary>
        /// Получить небесное тело по координатам
        /// </summary>
        Task<CelestialBody?> GetByCoordinatesAsync(int x, int y);

        /// <summary>
        /// Проверить, занята ли позиция другим небесным телом
        /// </summary>
        Task<bool> IsPositionOccupiedAsync(int x, int y, int? excludeId = null);
    }
}
