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
    public class TechController : ControllerBase
    {
        private readonly ITechService _techService;

        /// <summary>
        /// Конструктор
        /// </summary>
        public TechController(ITechService techService)
        {
            _techService = techService;
        }

        /// <summary>
        /// Получить все технологии
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<TechDto>>> GetTechs()
        {
            var techs = await _techService.GetAllAsync();
            return Ok(techs);
        }

        /// <summary>
        /// Получить технологию по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TechDto>> GetTech(int id)
        {
            var tech = await _techService.GetByIdAsync(id);

            if (tech == null)
                return NotFound(new { message = $"Технология с ID {id} не найдена" });

            return Ok(tech);
        }

        /// <summary>
        /// Создать новую технологию
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TechDto>> CreateTech(CreateTechRequest request)
        {
            var tech = await _techService.CreateAsync(request);
            return CreatedAtAction(nameof(GetTech), new { id = tech.Id }, tech);
        }

        /// <summary>
        /// Обновить технологию
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<TechDto>> UpdateTech(int id, UpdateTechRequest request)
        {
            var tech = await _techService.UpdateAsync(id, request);

            if (tech == null)
                return NotFound(new { message = $"Технология с ID {id} не найдена" });

            return Ok(tech);
        }

        /// <summary>
        /// Удалить технологию
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTech(int id)
        {
            var result = await _techService.DeleteAsync(id);

            if (!result)
                return NotFound(new { message = $"Технология с ID {id} не найдена" });

            return NoContent();
        }
    }
}

