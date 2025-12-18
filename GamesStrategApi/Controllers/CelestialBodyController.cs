using GamesStrategApi.Interfaces;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesStrategApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CelestialBodyController : ControllerBase
    {
        private readonly ICelestialBodyService _celestialBodyService;

        /// <summary>
        /// Конструктор
        /// </summary>
        public CelestialBodyController(ICelestialBodyService celestialBodyService)
        {
            _celestialBodyService = celestialBodyService;
        }

        /// <summary>
        /// Получить все небесные тела
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<CelestialBodyDto>>> GetCelestialBodies()
        {
            var bodies = await _celestialBodyService.GetAllAsync();
            return Ok(bodies);
        }

        /// <summary>
        /// Получить небесное тело по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CelestialBodyDto>> GetCelestialBody(int id)
        {
            var body = await _celestialBodyService.GetByIdAsync(id);

            if (body == null)
                return NotFound(new { message = $"Небесное тело с ID {id} не найдено" });

            return Ok(body);
        }

        /// <summary>
        /// Создать новое небесное тело
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CelestialBodyDto>> CreateCelestialBody(CreateCelestialBodyRequest request)
        {
            var body = await _celestialBodyService.CreateAsync(request);
            return CreatedAtAction(nameof(GetCelestialBody), new { id = body.Id }, body);
        }

        /// <summary>
        /// Обновить небесное тело
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<CelestialBodyDto>> UpdateCelestialBody(int id, UpdateCelestialBodyRequest request)
        {
            var body = await _celestialBodyService.UpdateAsync(id, request);

            if (body == null)
                return NotFound(new { message = $"Небесное тело с ID {id} не найдено" });

            return Ok(body);
        }

        /// <summary>
        /// Удалить небесное тело
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCelestialBody(int id)
        {
            var result = await _celestialBodyService.DeleteAsync(id);

            if (!result)
                return NotFound(new { message = $"Небесное тело с ID {id} не найдено" });

            return NoContent();
        }
    }
}
