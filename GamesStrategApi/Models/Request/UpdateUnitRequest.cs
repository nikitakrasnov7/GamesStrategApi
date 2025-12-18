namespace GamesStrategApi.Models.Request
{
    public class UpdateUnitRequest
    {
        // Название юнита
        public string Name { get; set; } = string.Empty;

        // Тип юнита
        public string UnitType { get; set; } = string.Empty;

        // Здоровье юнита
        public int Health { get; set; }

        // Щиты юнита
        public int Shield { get; set; }

        // Урон юнита
        public int Damage { get; set; }

        // Скорость движения
        public int MovementSpeed { get; set; }

        // Стоимость производства
        public int ProductionCost { get; set; }

        // ID расы
        public int? RaceId { get; set; }

        // ID разблокирующей технологии
        public int? UnlockingTechId { get; set; }
    }
}
