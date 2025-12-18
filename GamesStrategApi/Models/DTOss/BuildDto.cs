namespace GamesStrategApi.Models.DTOss
{
    public class BuildDto
    {
        public int Id { get; set; }                 // ID здания
        public string Name { get; set; } = string.Empty;           // Название здания
        public string BuildingType { get; set; } = string.Empty;   // Тип здания
        public string Description { get; set; } = string.Empty;    // Описание
        public int ProductionCost { get; set; }     // Стоимость постройки
        public int IncomeBonus { get; set; }        // Бонус к доходу
        public int? UnlockingTechId { get; set; }   // ID разблокирующей технологии
    }
}
