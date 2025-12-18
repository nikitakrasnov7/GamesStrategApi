using GamesStrategApi.Models;

namespace GamesStrategApi.Interfaces
{
    public interface IRaceRepo : IRepository<Race>
    {
        /// <summary>
        /// Получить только играбельные расы
        /// </summary>
        Task<IEnumerable<Race>> GetPlayableRacesAsync();

        /// <summary>
        /// Получить расы с определенным типом родного мира
        /// </summary>
        Task<IEnumerable<Race>> GetRacesByHomeWorldTypeAsync(string homeWorldType);

        /// <summary>
        /// Получить расы с их юнитами
        /// </summary>
        Task<IEnumerable<Race>> GetRacesWithUnitsAsync();

        /// <summary>
        /// Получить расу по ID с ее юнитами
        /// </summary>
        Task<Race?> GetRaceWithUnitsAsync(int id);
    }
}
