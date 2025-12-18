namespace GamesStrategApi.Models.Request
{
    public class CreateUnitRequest
    {
        // Название юнита
        public string Name { get; set; } = string.Empty;

        // Тип юнита
        public string UnitType { get; set; } = string.Empty;

        // Здоровье юнита
        public int Health { get; set; } = 100;

        // Щиты юнита
        public int Shield { get; set; } = 0;

        // Урон юнита
        public int Damage { get; set; } = 10;

        // Скорость движения
        public int MovementSpeed { get; set; } = 5;

        // Стоимость производства
        public int ProductionCost { get; set; } = 100;

        // ID расы
        public int? RaceId { get; set; }

        // ID разблокирующей технологии
        public int? UnlockingTechId { get; set; }
    }
}
