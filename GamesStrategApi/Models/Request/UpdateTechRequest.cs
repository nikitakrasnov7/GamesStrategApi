namespace GamesStrategApi.Models.Request
{
    public class UpdateTechRequest
    {
        // Название технологии
        public string Name { get; set; } = string.Empty;

        // Описание технологии
        public string Description { get; set; } = string.Empty;

        // Стоимость исследования
        public int ResearchCost { get; set; }

        // Уровень технологии
        public int Tier { get; set; }

        // Путь к иконке
        public string IconPath { get; set; } = string.Empty;

        // ID требуемой технологии
        public int? RequiredTechId { get; set; }
    }
}
