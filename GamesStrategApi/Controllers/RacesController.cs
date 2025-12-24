using Microsoft.AspNetCore.Mvc;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        private readonly IRaceService _raceService;

        public RacesController(IRaceService raceService)
        {
            _raceService = raceService;
        }

        // Получить все расы
        [HttpGet]
        public async Task<ActionResult<List<RaceDto>>> GetRaces()
        {
            var races = await _raceService.GetAllAsync();
            return Ok(races);
        }

        // Получить расу по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<RaceDto>> GetRace(int id)
        {
            var race = await _raceService.GetByIdAsync(id);

            if (race == null)
            {
                return NotFound(new { error = $"Раса с ID {id} не найдена" });
            }

            return Ok(race);
        }

        // Получить играбельные расы
        [HttpGet("playable")]
        public async Task<ActionResult<List<RaceDto>>> GetPlayableRaces()
        {
            try
            {
                var races = await _raceService.GetPlayableRacesAsync();
                return Ok(races);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ошибка сервера");
            }
        }

        // Создать новую расу
        [HttpPost]
        public async Task<ActionResult<RaceDto>> CreateRace([FromBody] CreateRaceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var race = await _raceService.CreateAsync(request);
                return CreatedAtAction(nameof(GetRace), new { id = race.Id }, race);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Обновить расу
        [HttpPut("{id}")]
        public async Task<ActionResult<RaceDto>> UpdateRace(int id, [FromBody] UpdateRaceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var race = await _raceService.UpdateAsync(id, request);

                if (race == null)
                {
                    return NotFound(new { error = $"Раса с ID {id} не найдена" });
                }

                return Ok(race);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Удалить расу
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            try
            {
                var result = await _raceService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound(new { error = $"Раса с ID {id} не найдена" });
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}