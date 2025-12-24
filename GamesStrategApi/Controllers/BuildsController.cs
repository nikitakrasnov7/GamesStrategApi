using Microsoft.AspNetCore.Mvc;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildsController : Controller
    {
        private readonly IBuildService _buildingService;

        public BuildsController(IBuildService buildingService)
        {
            _buildingService = buildingService;
        }

        // Получить все здания
        [HttpGet]
        public async Task<ActionResult<List<BuildDto>>> GetBuildings()
        {
            try
            {
                var buildings = await _buildingService.GetAllAsync();
                return Ok(buildings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при получении зданий", details = ex.Message });
            }
        }

        // Получить здание по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildDto>> GetBuilding(int id)
        {
            try
            {
                var building = await _buildingService.GetByIdAsync(id);

                if (building == null)
                {
                    return NotFound(new { error = $"Здание с ID {id} не найдено" });
                }

                return Ok(building);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при получении здания", details = ex.Message });
            }
        }

        // Создать новое здание
        [HttpPost]
        public async Task<ActionResult<BuildDto>> CreateBuilding([FromBody] CreateBuildRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var building = await _buildingService.CreateAsync(request);
                return CreatedAtAction(nameof(GetBuilding), new { id = building.Id }, building);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при создании здания", details = ex.Message });
            }
        }

        // Обновить здание
        [HttpPut("{id}")]
        public async Task<ActionResult<BuildDto>> UpdateBuilding(int id, [FromBody] UpdateBuildRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var building = await _buildingService.UpdateAsync(id, request);

                if (building == null)
                {
                    return NotFound(new { error = $"Здание с ID {id} не найдено" });
                }

                return Ok(building);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при обновлении здания", details = ex.Message });
            }
        }

        // Удалить здание
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            try
            {
                var result = await _buildingService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound(new { error = $"Здание с ID {id} не найдено" });
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при удалении здания", details = ex.Message });
            }
        }

        // Дополнительные эндпоинты
        [HttpGet("economic")]
        public async Task<ActionResult<List<BuildDto>>> GetEconomicBuildings()
        {
            try
            {
                var buildings = await _buildingService.GetEconomicBuildingsAsync();
                return Ok(buildings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при получении экономических зданий", details = ex.Message });
            }
        }

        [HttpGet("by-type/{type}")]
        public async Task<ActionResult<List<BuildDto>>> GetByType(string type)
        {
            try
            {
                var buildings = await _buildingService.GetByTypeAsync(type);
                return Ok(buildings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ошибка при получении зданий по типу", details = ex.Message });
            }
        }
    }
}