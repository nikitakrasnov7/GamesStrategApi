using AutoMapper;
using GamesStrategApi.Interfaces;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Models.Services
{
    public class TechServices : ITechService
    {
        private readonly ITechRepo _techRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        public TechServices(ITechRepo techRepository, IMapper mapper)
        {
            _techRepository = techRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все технологии
        /// </summary>
        public async Task<List<TechDto>> GetAllAsync()
        {
            var techs = await _techRepository.GetAllAsync();
            return _mapper.Map<List<TechDto>>(techs);
        }

        /// <summary>
        /// Получить технологию по ID
        /// </summary>
        public async Task<TechDto?> GetByIdAsync(int id)
        {
            var tech = await _techRepository.GetByIdAsync(id);
            return tech == null ? null : _mapper.Map<TechDto>(tech);
        }

        /// <summary>
        /// Получить технологии по уровню
        /// </summary>
        public async Task<List<TechDto>> GetByTierAsync(int tier)
        {
            var techs = await _techRepository.GetTechsByTierAsync(tier);
            return _mapper.Map<List<TechDto>>(techs);
        }

        /// <summary>
        /// Получить стартовые технологии (уровень 1, без требований)
        /// </summary>
        public async Task<List<TechDto>> GetStartingTechsAsync()
        {
            var techs = await _techRepository.GetAllAsync();
            var starting = techs.Where(t => t.Tier == 1 && t.RequiredTechId == null);
            return _mapper.Map<List<TechDto>>(starting);
        }

        /// <summary>
        /// Создать новую технологию
        /// </summary>
        public async Task<TechDto> CreateAsync(CreateTechRequest request)
        {
            // Простая валидация: уровень от 1 до 5
            if (request.Tier < 1 || request.Tier > 5)
            {
                throw new ArgumentException("Уровень технологии должен быть от 1 до 5");
            }

            // Простая валидация: стоимость > 0
            if (request.ResearchCost <= 0)
            {
                throw new ArgumentException("Стоимость исследования должна быть больше 0");
            }

            var tech = _mapper.Map<Tech>(request);
            var createdTech = await _techRepository.AddAsync(tech);
            return _mapper.Map<TechDto>(createdTech);
        }

        /// <summary>
        /// Обновить технологию
        /// </summary>
        public async Task<TechDto?> UpdateAsync(int id, UpdateTechRequest request)
        {
            var tech = await _techRepository.GetByIdAsync(id);
            if (tech == null) return null;

            // Простая бизнес-логика: нельзя повысить уровень существующей технологии
            if (request.Tier > tech.Tier)
            {
                throw new InvalidOperationException("Нельзя повысить уровень существующей технологии");
            }

            _mapper.Map(request, tech);
            await _techRepository.UpdateAsync(tech);

            return _mapper.Map<TechDto>(tech);
        }

        /// <summary>
        /// Удалить технологию
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var tech = await _techRepository.GetByIdAsync(id);
            if (tech == null) return false;

            // Простая бизнес-логика: нельзя удалить технологию, если она требуется другим
            var dependentTechs = await _techRepository.FindAsync(t => t.RequiredTechId == id);
            if (dependentTechs.Any())
            {
                throw new InvalidOperationException("Нельзя удалить технологию, так как она требуется другим технологиям");
            }

            await _techRepository.DeleteAsync(tech);
            return true;
        }

        /// <summary>
        /// Рассчитать общую стоимость исследования
        /// </summary>
        public async Task<int> CalculateTotalCostAsync(List<int> techIds)
        {
            int total = 0;

            foreach (var techId in techIds)
            {
                var tech = await _techRepository.GetByIdAsync(techId);
                if (tech != null)
                {
                    total += tech.ResearchCost;
                }
            }

            return total;
        }
    }
}
