namespace GamesStrategApi.Models.DTOss
{
    public class TechDto
    {
        public int Id { get; set; }                 // ID технологии
        public string Name { get; set; } = string.Empty;           // Название
        public string Description { get; set; } = string.Empty;    // Описание
        public int ResearchCost { get; set; }       // Стоимость исследования
        public int Tier { get; set; }               // Уровень технологии
        public string IconPath { get; set; } = string.Empty;       // Путь к иконке
        public int? RequiredTechId { get; set; }    // ID требуемой технологии
    }
}
