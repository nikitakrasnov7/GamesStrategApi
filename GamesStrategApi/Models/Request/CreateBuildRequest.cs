namespace GamesStrategApi.Models.Request
{
    public class CreateBuildRequest
    {
        // Название здания
        public string Name { get; set; } = string.Empty;

        // Тип здания
        public string BuildingType { get; set; } = string.Empty;

        // Описание здания
        public string Description { get; set; } = string.Empty;

        // Стоимость постройки
        public int ProductionCost { get; set; } = 100;

        // Бонус к доходу
        public int IncomeBonus { get; set; } = 0;

        // ID разблокирующей технологии
        public int? UnlockingTechId { get; set; }
    }
}
