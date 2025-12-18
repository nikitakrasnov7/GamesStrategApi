namespace GamesStrategApi.Models.DTOss
{
    public class UnitDto
    {
        public int Id { get; set; }                 // ID юнита
        public string Name { get; set; } = string.Empty;           // Название
        public string UnitType { get; set; } = string.Empty;       // Тип юнита
        public int Health { get; set; }             // Здоровье
        public int Shield { get; set; }             // Щиты
        public int Damage { get; set; }             // Урон
        public int MovementSpeed { get; set; }      // Скорость движения
        public int ProductionCost { get; set; }     // Стоимость производства
        public int? RaceId { get; set; }            // ID расы
        public int? UnlockingTechId { get; set; }   // ID разблокирующей технологии
    }
}
