using GamesStrategApi.Interfaces;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesStrategApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с расами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        private readonly IRaceService _raceService;

        /// <summary>
        /// Конструктор
        /// </summary>
        public RacesController(IRaceService raceService)
        {
            _raceService = raceService;
        }

        /// <summary>
        /// Получить все расы
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<RaceDto>>> GetRaces()
        {
            var races = await _raceService.GetAllAsync();
            return Ok(races);
        }

        /// <summary>
        /// Получить расу по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RaceDto>> GetRace(int id)
        {
            var race = await _raceService.GetByIdAsync(id);

            if (race == null)
                return NotFound(new { message = $"Раса с ID {id} не найдена" });

            return Ok(race);
        }

        /// <summary>
        /// Создать новую расу
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RaceDto>> CreateRace(CreateRaceRequest request)
        {
            var race = await _raceService.CreateAsync(request);
            return CreatedAtAction(nameof(GetRace), new { id = race.Id }, race);
        }

        /// <summary>
        /// Обновить расу
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<RaceDto>> UpdateRace(int id, UpdateRaceRequest request)
        {
            var race = await _raceService.UpdateAsync(id, request);

            if (race == null)
                return NotFound(new { message = $"Раса с ID {id} не найдена" });

            return Ok(race);
        }

        /// <summary>
        /// Удалить расу
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var result = await _raceService.DeleteAsync(id);

            if (!result)
                return NotFound(new { message = $"Раса с ID {id} не найдена" });

            return NoContent();
        }
    }
}


