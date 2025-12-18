using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStrategApi.Models
{
    [Table("Races")]
    public class Race
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }                 // ID расы

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;           // Название расы

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;    // Описание расы

        [Required]
        [MaxLength(50)]
        public string HomeWorldType { get; set; } = string.Empty;  // Тип родного мира

        [MaxLength(200)]
        public string UniqueBonus { get; set; } = string.Empty;    // Уникальный бонус

        public bool IsPlayable { get; set; } = true; // Доступна для игры

        public virtual ICollection<Unit> Units { get; set; } = new List<Unit>(); // Связанные юниты
    }
}
