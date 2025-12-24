using Microsoft.AspNetCore.Mvc;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechController : ControllerBase
    {
        private readonly ITechService _techService;

        public TechController(ITechService techService)
        {
            _techService = techService;
        }

        // Получить все технологии
        [HttpGet]
        public async Task<ActionResult<List<TechDto>>> GetTechs()
        {
            var techs = await _techService.GetAllAsync();
            return Ok(techs);
        }

        // Получить технологию по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TechDto>> GetTech(int id)
        {
            var tech = await _techService.GetByIdAsync(id);

            if (tech == null)
            {
                return NotFound(new { error = $"Технология с ID {id} не найдена" });
            }

            return Ok(tech);
        }

        // Получить стартовые технологии
        [HttpGet("starting")]
        public async Task<ActionResult<List<TechDto>>> GetStartingTechs()
        {
            try
            {
                var techs = await _techService.GetStartingTechsAsync();
                return Ok(techs);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ошибка при получении стартовых технологий");
            }
        }

        // Создать новую технологию
        [HttpPost]
        public async Task<ActionResult<TechDto>> CreateTech([FromBody] CreateTechRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var tech = await _techService.CreateAsync(request);
                return CreatedAtAction(nameof(GetTech), new { id = tech.Id }, tech);
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

        // Обновить технологию
        [HttpPut("{id}")]
        public async Task<ActionResult<TechDto>> UpdateTech(int id, [FromBody] UpdateTechRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var tech = await _techService.UpdateAsync(id, request);

                if (tech == null)
                {
                    return NotFound(new { error = $"Технология с ID {id} не найдена" });
                }

                return Ok(tech);
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

        // Удалить технологию
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTech(int id)
        {
            try
            {
                var result = await _techService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound(new { error = $"Технология с ID {id} не найдена" });
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