using GamesStrategApi.Interfaces;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesStrategApi.Controllers
{
    public class BuildsController : Controller
    {
        private readonly IBuildService _buildingService;

        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildsController(IBuildService buildingService)
        {
            _buildingService = buildingService;
        }

        /// <summary>
        /// Получить все здания
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<BuildDto>>> GetBuildings()
        {
            var buildings = await _buildingService.GetAllAsync();
            return Ok(buildings);
        }

        /// <summary>
        /// Получить здание по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildDto>> GetBuilding(int id)
        {
            var building = await _buildingService.GetByIdAsync(id);

            if (building == null)
                return NotFound(new { message = $"Здание с ID {id} не найдено" });

            return Ok(building);
        }

        /// <summary>
        /// Создать новое здание
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BuildDto>> CreateBuilding(CreateBuildRequest request)
        {
            var building = await _buildingService.CreateAsync(request);
            return CreatedAtAction(nameof(GetBuilding), new { id = building.Id }, building);
        }

        /// <summary>
        /// Обновить здание
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<BuildDto>> UpdateBuilding(int id, UpdateBuildRequest request)
        {
            var building = await _buildingService.UpdateAsync(id, request);

            if (building == null)
                return NotFound(new { message = $"Здание с ID {id} не найдено" });

            return Ok(building);
        }

        /// <summary>
        /// Удалить здание
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            var result = await _buildingService.DeleteAsync(id);

            if (!result)
                return NotFound(new { message = $"Здание с ID {id} не найдено" });

            return NoContent();
        }

    }
}
