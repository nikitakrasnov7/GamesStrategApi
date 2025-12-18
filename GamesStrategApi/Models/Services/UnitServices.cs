using AutoMapper;
using GamesStrategApi.Interfaces;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Models.Services
{
    public class UnitServices : IUnitService
    {
        private readonly IUnitsRepo _unitRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        public UnitServices(IUnitsRepo unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все юниты
        /// </summary>
        public async Task<List<UnitDto>> GetAllAsync()
        {
            var units = await _unitRepository.GetAllAsync();
            return _mapper.Map<List<UnitDto>>(units);
        }

        /// <summary>
        /// Получить юнит по ID
        /// </summary>
        public async Task<UnitDto?> GetByIdAsync(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            return unit == null ? null : _mapper.Map<UnitDto>(unit);
        }

        /// <summary>
        /// Получить юниты по расе
        /// </summary>
        public async Task<List<UnitDto>> GetByRaceAsync(int raceId)
        {
            var units = await _unitRepository.GetUnitsByRaceAsync(raceId);
            return _mapper.Map<List<UnitDto>>(units);
        }

        /// <summary>
        /// Получить юниты по типу
        /// </summary>
        public async Task<List<UnitDto>> GetByTypeAsync(string unitType)
        {
            var units = await _unitRepository.GetUnitsByTypeAsync(unitType);
            return _mapper.Map<List<UnitDto>>(units);
        }

        /// <summary>
        /// Создать нового юнита
        /// </summary>
        public async Task<UnitDto> CreateAsync(CreateUnitRequest request)
        {
            // Простая валидация: здоровье > 0
            if (request.Health <= 0)
            {
                throw new ArgumentException("Здоровье должно быть больше 0");
            }

            // Простая валидация: урон >= 0
            if (request.Damage < 0)
            {
                throw new ArgumentException("Урон не может быть отрицательным");
            }

            // Простая валидация: стоимость > 0
            if (request.ProductionCost <= 0)
            {
                throw new ArgumentException("Стоимость производства должна быть больше 0");
            }

            var unit = _mapper.Map<Unit>(request);
            var createdUnit = await _unitRepository.AddAsync(unit);
            return _mapper.Map<UnitDto>(createdUnit);
        }

        /// <summary>
        /// Обновить юнита
        /// </summary>
        public async Task<UnitDto?> UpdateAsync(int id, UpdateUnitRequest request)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            if (unit == null) return null;

            _mapper.Map(request, unit);
            await _unitRepository.UpdateAsync(unit);

            return _mapper.Map<UnitDto>(unit);
        }

        /// <summary>
        /// Удалить юнита
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            if (unit == null) return false;

            await _unitRepository.DeleteAsync(unit);
            return true;
        }

        /// <summary>
        /// Получить сильных юнитов (урон > 50, здоровье > 100)
        /// </summary>
        public async Task<List<UnitDto>> GetStrongUnitsAsync()
        {
            var units = await _unitRepository.GetPowerfulUnitsAsync(50, 100);
            return _mapper.Map<List<UnitDto>>(units);
        }

        /// <summary>
        /// Получить дешевых юнитов (стоимость < 100)
        /// </summary>
        public async Task<List<UnitDto>> GetCheapUnitsAsync()
        {
            var units = await _unitRepository.GetCheapUnitsAsync(100);
            return _mapper.Map<List<UnitDto>>(units);
        }

        /// <summary>
        /// Рассчитать стоимость армии
        /// </summary>
        public async Task<int> CalculateArmyCostAsync(Dictionary<int, int> unitQuantities)
        {
            int totalCost = 0;

            foreach (var kvp in unitQuantities)
            {
                var unit = await _unitRepository.GetByIdAsync(kvp.Key);
                if (unit != null)
                {
                    totalCost += unit.ProductionCost * kvp.Value;
                }
            }

            return totalCost;
        }
    }
}
