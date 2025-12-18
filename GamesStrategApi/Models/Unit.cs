using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStrategApi.Models
{
    [Table("Units")]
    public class Unit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }                 // ID юнита

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;           // Название

        [Required]
        [MaxLength(50)]
        public string UnitType { get; set; } = string.Empty;       // Тип юнита

        public int Health { get; set; }             // Здоровье

        public int Shield { get; set; }             // Щиты

        public int Damage { get; set; }             // Урон

        public int MovementSpeed { get; set; }      // Скорость движения

        public int ProductionCost { get; set; }     // Стоимость производства

        public int? RaceId { get; set; }            // ID расы

        public int? UnlockingTechId { get; set; }   // ID разблокирующей технологии

        [ForeignKey("RaceId")]
        public virtual Race? Race { get; set; }     // Ссылка на расу

        [ForeignKey("UnlockingTechId")]
        public virtual Tech? UnlockingTech { get; set; } // Ссылка на технологию
    }
}
