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
    public class UnitsController : Controller
    {
        private readonly IUnitService _unitService;

        /// <summary>
        /// Конструктор
        /// </summary>
        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        /// <summary>
        /// Получить все юниты
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<UnitDto>>> GetUnits()
        {
            var units = await _unitService.GetAllAsync();
            return Ok(units);
        }

        /// <summary>
        /// Получить юнит по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitDto>> GetUnit(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);

            if (unit == null)
                return NotFound(new { message = $"Юнит с ID {id} не найден" });

            return Ok(unit);
        }

        /// <summary>
        /// Создать нового юнита
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UnitDto>> CreateUnit(CreateUnitRequest request)
        {
            var unit = await _unitService.CreateAsync(request);
            return CreatedAtAction(nameof(GetUnit), new { id = unit.Id }, unit);
        }

        /// <summary>
        /// Обновить юнита
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<UnitDto>> UpdateUnit(int id, UpdateUnitRequest request)
        {
            var unit = await _unitService.UpdateAsync(id, request);

            if (unit == null)
                return NotFound(new { message = $"Юнит с ID {id} не найден" });

            return Ok(unit);
        }

        /// <summary>
        /// Удалить юнита
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var result = await _unitService.DeleteAsync(id);

            if (!result)
                return NotFound(new { message = $"Юнит с ID {id} не найден" });

            return NoContent();
        }
    }
}

