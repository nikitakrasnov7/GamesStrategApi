namespace GamesStrategApi.Models.Request
{
    public class UpdateRaceRequest
    {
        // Название расы
        public string Name { get; set; } = string.Empty;

        // Описание расы
        public string Description { get; set; } = string.Empty;

        // Тип родного мира
        public string HomeWorldType { get; set; } = string.Empty;

        // Уникальный бонус
        public string UniqueBonus { get; set; } = string.Empty;

        // Доступна для игры
        public bool IsPlayable { get; set; }
    }
}
