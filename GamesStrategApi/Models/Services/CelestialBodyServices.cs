using AutoMapper;
using GamesStrategApi.Interfaces;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Models.Services
{
    public class CelestialBodyServices : ICelestialBodyService
    {
        private readonly ICelestialBodyRepo _celestialBodyRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        public CelestialBodyServices(ICelestialBodyRepo celestialBodyRepository, IMapper mapper)
        {
            _celestialBodyRepository = celestialBodyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все небесные тела
        /// </summary>
        public async Task<List<CelestialBodyDto>> GetAllAsync()
        {
            var bodies = await _celestialBodyRepository.GetAllAsync();
            return _mapper.Map<List<CelestialBodyDto>>(bodies);
        }

        /// <summary>
        /// Получить небесное тело по ID
        /// </summary>
        public async Task<CelestialBodyDto?> GetByIdAsync(int id)
        {
            var body = await _celestialBodyRepository.GetByIdAsync(id);
            return body == null ? null : _mapper.Map<CelestialBodyDto>(body);
        }

        /// <summary>
        /// Получить небесные тела по типу
        /// </summary>
        public async Task<List<CelestialBodyDto>> GetByTypeAsync(string type)
        {
            var bodies = await _celestialBodyRepository.GetByTypeAsync(type);
            return _mapper.Map<List<CelestialBodyDto>>(bodies);
        }

        /// <summary>
        /// Создать новое небесное тело
        /// </summary>
        public async Task<CelestialBodyDto> CreateAsync(CreateCelestialBodyRequest request)
        {
            // Простая валидация: координаты должны быть уникальными
            bool occupied = await _celestialBodyRepository.IsPositionOccupiedAsync(
                request.PositionX, request.PositionY);

            if (occupied)
            {
                throw new ArgumentException("Позиция уже занята другим небесным телом");
            }

            // Простая валидация: богатство ресурсов от 1 до 10
            if (request.ResourceRichness < 1 || request.ResourceRichness > 10)
            {
                throw new ArgumentException("Богатство ресурсов должно быть от 1 до 10");
            }

            var body = _mapper.Map<CelestialBody>(request);
            var createdBody = await _celestialBodyRepository.AddAsync(body);
            return _mapper.Map<CelestialBodyDto>(createdBody);
        }

        /// <summary>
        /// Обновить небесное тело
        /// </summary>
        public async Task<CelestialBodyDto?> UpdateAsync(int id, UpdateCelestialBodyRequest request)
        {
            var body = await _celestialBodyRepository.GetByIdAsync(id);
            if (body == null) return null;

            // Простая бизнес-логика: нельзя изменить тип планеты на "BlackHole"
            if (request.Type == "BlackHole" && body.Type != "BlackHole")
            {
                throw new InvalidOperationException("Нельзя изменить тип на Черную Дыру");
            }

            _mapper.Map(request, body);
            await _celestialBodyRepository.UpdateAsync(body);

            return _mapper.Map<CelestialBodyDto>(body);
        }

        /// <summary>
        /// Удалить небесное тело
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var body = await _celestialBodyRepository.GetByIdAsync(id);
            if (body == null) return false;

            // Простая бизнес-логика: нельзя удалить богатые планеты
            if (body.ResourceRichness >= 8)
            {
                throw new InvalidOperationException("Нельзя удалить богатые ресурсами планеты");
            }

            await _celestialBodyRepository.DeleteAsync(body);
            return true;
        }

        /// <summary>
        /// Получить богатые небесные тела
        /// </summary>
        public async Task<List<CelestialBodyDto>> GetRichBodiesAsync()
        {
            var bodies = await _celestialBodyRepository.GetRichBodiesAsync(7);
            return _mapper.Map<List<CelestialBodyDto>>(bodies);
        }

        /// <summary>
        /// Найти ближайшее небесное тело к координатам
        /// </summary>
        public async Task<CelestialBodyDto?> FindNearestAsync(int x, int y, int radius = 100)
        {
            var bodies = await _celestialBodyRepository.GetBodiesInAreaAsync(x, y, radius);
            var nearest = bodies.FirstOrDefault();
            return nearest == null ? null : _mapper.Map<CelestialBodyDto>(nearest);
        }
    }
}
