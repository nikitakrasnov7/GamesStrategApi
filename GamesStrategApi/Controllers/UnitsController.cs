using Microsoft.AspNetCore.Mvc;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : Controller
    {
        private readonly IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        // Получить все юниты
        [HttpGet]
        public async Task<ActionResult<List<UnitDto>>> GetUnits()
        {
            try
            {
                var units = await _unitService.GetAllAsync();
                return Ok(units);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка сервера", details = ex.Message });
            }
        }

        // Получить юнит по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitDto>> GetUnit(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);

            if (unit == null)
            {
                return NotFound(new { error = $"Юнит с ID {id} не найден" });
            }

            return Ok(unit);
        }

        // Получить юниты по расе
        [HttpGet("by-race/{raceId}")]
        public async Task<ActionResult<List<UnitDto>>> GetByRace(int raceId)
        {
            try
            {
                var units = await _unitService.GetByRaceAsync(raceId);
                return Ok(units);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Получить сильных юнитов
        [HttpGet("strong")]
        public async Task<ActionResult<List<UnitDto>>> GetStrongUnits()
        {
            try
            {
                var units = await _unitService.GetStrongUnitsAsync();
                return Ok(units);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Создать нового юнита
        [HttpPost]
        public async Task<ActionResult<UnitDto>> CreateUnit([FromBody] CreateUnitRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var unit = await _unitService.CreateAsync(request);
                return CreatedAtAction(nameof(GetUnit), new { id = unit.Id }, unit);
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

        // Обновить юнита
        [HttpPut("{id}")]
        public async Task<ActionResult<UnitDto>> UpdateUnit(int id, [FromBody] UpdateUnitRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var unit = await _unitService.UpdateAsync(id, request);

                if (unit == null)
                {
                    return NotFound(new { error = $"Юнит с ID {id} не найден" });
                }

                return Ok(unit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Удалить юнита
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            try
            {
                var result = await _unitService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound(new { error = $"Юнит с ID {id} не найден" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Рассчитать стоимость армии
        [HttpPost("calculate-cost")]
        public async Task<ActionResult<int>> CalculateArmyCost([FromBody] Dictionary<int, int> unitQuantities)
        {
            try
            {
                var cost = await _unitService.CalculateArmyCostAsync(unitQuantities);
                return Ok(new { totalCost = cost });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}