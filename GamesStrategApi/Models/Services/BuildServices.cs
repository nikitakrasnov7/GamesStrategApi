using AutoMapper;
using GamesStrategApi.Interfaces;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Models.Services
{
    public class BuildServices : IBuildService
    {
        private readonly IBuildRepo _buildingRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildServices(IBuildRepo buildingRepository, IMapper mapper)
        {
            _buildingRepository = buildingRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все здания
        /// </summary>
        public async Task<List<BuildDto>> GetAllAsync()
        {
            var buildings = await _buildingRepository.GetAllAsync();
            return _mapper.Map<List<BuildDto>>(buildings);
        }

        /// <summary>
        /// Получить здание по ID
        /// </summary>
        public async Task<BuildDto?> GetByIdAsync(int id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            return building == null ? null : _mapper.Map<BuildDto>(building);
        }

        /// <summary>
        /// Получить здания по типу
        /// </summary>
        public async Task<List<BuildDto>> GetByTypeAsync(string buildingType)
        {
            var buildings = await _buildingRepository.GetBuildingsByTypeAsync(buildingType);
            return _mapper.Map<List<BuildDto>>(buildings);
        }

        /// <summary>
        /// Создать новое здание
        /// </summary>
        public async Task<BuildDto> CreateAsync(CreateBuildRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Название здания не может быть пустым");
            }

            if (request.ProductionCost <= 0)
            {
                throw new ArgumentException("Стоимость постройки должна быть больше 0");
            }

            var building = _mapper.Map<Building>(request);
            var createdBuilding = await _buildingRepository.AddAsync(building);
            return _mapper.Map<BuildDto>(createdBuilding);
        }

        /// <summary>
        /// Обновить здание
        /// </summary>
        public async Task<BuildDto?> UpdateAsync(int id, UpdateBuildRequest request)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            if (building == null) return null;

            _mapper.Map(request, building);
            await _buildingRepository.UpdateAsync(building);

            return _mapper.Map<BuildDto>(building);
        }

        /// <summary>
        /// Удалить здание
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            if (building == null) return false;

            await _buildingRepository.DeleteAsync(building);
            return true;
        }

        /// <summary>
        /// Получить экономические здания
        /// </summary>
        public async Task<List<BuildDto>> GetEconomicBuildingsAsync()
        {
            var buildings = await _buildingRepository.GetEconomicBuildingsAsync();
            return _mapper.Map<List<BuildDto>>(buildings);
        }

        /// <summary>
        /// Получить здания без требований технологий
        /// </summary>
        public async Task<List<BuildDto>> GetAvailableWithoutTechAsync()
        {
            var buildings = await _buildingRepository.GetAvailableWithoutTechAsync();
            return _mapper.Map<List<BuildDto>>(buildings);
        }

        /// <summary>
        /// Рассчитать окупаемость здания
        /// </summary>
        public async Task<int> CalculatePaybackPeriodAsync(int buildingId)
        {
            var building = await _buildingRepository.GetByIdAsync(buildingId);
            if (building == null) return 0;

            if (building.IncomeBonus <= 0) return 0;

            return building.ProductionCost / building.IncomeBonus;
        }

        /// <summary>
        /// Получить дорогие здания
        /// </summary>
        public async Task<List<BuildDto>> GetExpensiveBuildingsAsync(int minCost = 300)
        {
            var buildings = await _buildingRepository.GetExpensiveBuildingsAsync(minCost);
            return _mapper.Map<List<BuildDto>>(buildings);
        }
    }
}
