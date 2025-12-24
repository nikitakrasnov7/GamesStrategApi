using Microsoft.AspNetCore.Mvc;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CelestialBodyController : ControllerBase
    {
        private readonly ICelestialBodyService _celestialBodyService;

        public CelestialBodyController(ICelestialBodyService celestialBodyService)
        {
            _celestialBodyService = celestialBodyService;
        }

        // Получить все небесные тела
        [HttpGet]
        public async Task<ActionResult<List<CelestialBodyDto>>> GetCelestialBodies()
        {
            try
            {
                var bodies = await _celestialBodyService.GetAllAsync();
                return Ok(bodies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        // Получить небесное тело по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CelestialBodyDto>> GetCelestialBody(int id)
        {
            var body = await _celestialBodyService.GetByIdAsync(id);

            if (body == null)
            {
                return NotFound(new { error = $"Небесное тело с ID {id} не найдено" });
            }

            return Ok(body);
        }

        // Создать новое небесное тело
        [HttpPost]
        public async Task<ActionResult<CelestialBodyDto>> CreateCelestialBody([FromBody] CreateCelestialBodyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var body = await _celestialBodyService.CreateAsync(request);
                return CreatedAtAction(nameof(GetCelestialBody), new { id = body.Id }, body);
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

        // Обновить небесное тело
        [HttpPut("{id}")]
        public async Task<ActionResult<CelestialBodyDto>> UpdateCelestialBody(int id, [FromBody] UpdateCelestialBodyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var body = await _celestialBodyService.UpdateAsync(id, request);

                if (body == null)
                {
                    return NotFound(new { error = $"Небесное тело с ID {id} не найдено" });
                }

                return Ok(body);
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

        // Удалить небесное тело
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCelestialBody(int id)
        {
            try
            {
                var result = await _celestialBodyService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound(new { error = $"Небесное тело с ID {id} не найдено" });
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

        // Дополнительные методы
        [HttpGet("by-type/{type}")]
        public async Task<ActionResult<List<CelestialBodyDto>>> GetByType(string type)
        {
            try
            {
                var bodies = await _celestialBodyService.GetByTypeAsync(type);
                return Ok(bodies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("rich")]
        public async Task<ActionResult<List<CelestialBodyDto>>> GetRichBodies()
        {
            try
            {
                var bodies = await _celestialBodyService.GetRichBodiesAsync();
                return Ok(bodies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}