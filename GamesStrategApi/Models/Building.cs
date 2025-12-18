using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStrategApi.Models
{
    [Table("Buildings")]
    public class Building
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }                 // ID здания

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;           // Название здания

        [Required]
        [MaxLength(50)]
        public string BuildingType { get; set; } = string.Empty;   // Тип здания

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;    // Описание здания

        public int ProductionCost { get; set; }     // Стоимость постройки

        public int IncomeBonus { get; set; }        // Бонус к доходу

        public int? UnlockingTechId { get; set; }   // ID разблокирующей технологии

        [ForeignKey("UnlockingTechId")]
        public virtual Tech? UnlockingTech { get; set; }    // Ссылка на технологию
    }
}
