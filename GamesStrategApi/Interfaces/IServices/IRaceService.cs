using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Interfaces.IServices
{
    public interface IRaceService
    {
        /// <summary>
        /// Получить все расы
        /// </summary>
        Task<List<RaceDto>> GetAllAsync();

        /// <summary>
        /// Получить расу по ID
        /// </summary>
        Task<RaceDto?> GetByIdAsync(int id);

        /// <summary>
        /// Получить только играбельные расы
        /// </summary>
        Task<List<RaceDto>> GetPlayableRacesAsync();

        /// <summary>
        /// Создать новую расу
        /// </summary>
        Task<RaceDto> CreateAsync(CreateRaceRequest request);

        /// <summary>
        /// Обновить существующую расу
        /// </summary>
        Task<RaceDto?> UpdateAsync(int id, UpdateRaceRequest request);

        /// <summary>
        /// Удалить расу по ID
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Получить статистику по расам
        /// </summary>
        Task<object> GetStatsAsync();
    }
}
