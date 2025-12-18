using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Interfaces.IServices
{
    public interface ICelestialBodyService
    {
        /// <summary>
        /// Получить все небесные тела
        /// </summary>
        Task<List<CelestialBodyDto>> GetAllAsync();

        /// <summary>
        /// Получить небесное тело по ID
        /// </summary>
        Task<CelestialBodyDto?> GetByIdAsync(int id);

        /// <summary>
        /// Получить небесные тела определенного типа
        /// </summary>
        Task<List<CelestialBodyDto>> GetByTypeAsync(string type);

        /// <summary>
        /// Получить богатые ресурсами небесные тела
        /// </summary>
        Task<List<CelestialBodyDto>> GetRichBodiesAsync();

        /// <summary>
        /// Найти ближайшее небесное тело к указанным координатам
        /// </summary>
        Task<CelestialBodyDto?> FindNearestAsync(int x, int y, int radius = 100);

        /// <summary>
        /// Создать новое небесное тело
        /// </summary>
        Task<CelestialBodyDto> CreateAsync(CreateCelestialBodyRequest request);

        /// <summary>
        /// Обновить существующее небесное тело
        /// </summary>
        Task<CelestialBodyDto?> UpdateAsync(int id, UpdateCelestialBodyRequest request);

        /// <summary>
        /// Удалить небесное тело по ID
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}

