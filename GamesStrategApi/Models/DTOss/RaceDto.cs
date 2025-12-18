namespace GamesStrategApi.Models.DTOss
{
    public class RaceDto
    {
        public int Id { get; set; }                 // ID расы
        public string Name { get; set; } = string.Empty;           // Название расы
        public string Description { get; set; } = string.Empty;    // Описание
        public string HomeWorldType { get; set; } = string.Empty;  // Тип родного мира
        public string UniqueBonus { get; set; } = string.Empty;    // Уникальный бонус
        public bool IsPlayable { get; set; }        // Доступна для игры
    }
}
