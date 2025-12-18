using AutoMapper;
using GamesStrategApi.Interfaces;
using GamesStrategApi.Models;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Interfaces.IServices
{

    public class RaceServices : IRaceService
    {
        private readonly IRaceRepo _raceRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        public RaceServices(IRaceRepo raceRepository, IMapper mapper)
        {
            _raceRepository = raceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все расы
        /// </summary>
        public async Task<List<RaceDto>> GetAllAsync()
        {
            var races = await _raceRepository.GetAllAsync();
            return _mapper.Map<List<RaceDto>>(races);
        }

        /// <summary>
        /// Получить расу по ID
        /// </summary>
        public async Task<RaceDto?> GetByIdAsync(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            return race == null ? null : _mapper.Map<RaceDto>(race);
        }

        /// <summary>
        /// Получить только играбельные расы
        /// </summary>
        public async Task<List<RaceDto>> GetPlayableRacesAsync()
        {
            var races = await _raceRepository.GetPlayableRacesAsync();
            return _mapper.Map<List<RaceDto>>(races);
        }

        /// <summary>
        /// Создать новую расу
        /// </summary>
        public async Task<RaceDto> CreateAsync(CreateRaceRequest request)
        {
            // Простая валидация: имя не должно быть пустым
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Имя расы не может быть пустым");
            }

            // Простая валидация: проверка уникальности имени
            var existingRace = await _raceRepository.FirstOrDefaultAsync(r =>
                r.Name.ToLower() == request.Name.ToLower());

            if (existingRace != null)
            {
                throw new ArgumentException($"Раса с именем '{request.Name}' уже существует");
            }

            var race = _mapper.Map<Race>(request);
            var createdRace = await _raceRepository.AddAsync(race);
            return _mapper.Map<RaceDto>(createdRace);
        }

        /// <summary>
        /// Обновить расу
        /// </summary>
        public async Task<RaceDto?> UpdateAsync(int id, UpdateRaceRequest request)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null) return null;

            _mapper.Map(request, race);
            await _raceRepository.UpdateAsync(race);

            return _mapper.Map<RaceDto>(race);
        }

        /// <summary>
        /// Удалить расу
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null) return false;

            // Простая бизнес-логика: нельзя удалить играбельную расу
            if (race.IsPlayable)
            {
                throw new InvalidOperationException("Нельзя удалить играбельную расу");
            }

            await _raceRepository.DeleteAsync(race);
            return true;
        }

        /// <summary>
        /// Получить статистику по расам
        /// </summary>
        public async Task<object> GetStatsAsync()
        {
            var races = await _raceRepository.GetAllAsync();

            return new
            {
                TotalCount = races.Count(),
                PlayableCount = races.Count(r => r.IsPlayable),
                NonPlayableCount = races.Count(r => !r.IsPlayable),
                UniqueWorldTypes = races.Select(r => r.HomeWorldType).Distinct().Count()
            };
        }
    }
}

