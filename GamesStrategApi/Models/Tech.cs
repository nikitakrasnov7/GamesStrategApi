using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStrategApi.Models
{
    [Table("Techs")]
    public class Tech
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }                 // ID технологии

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;           // Название

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;    // Описание

        public int ResearchCost { get; set; }       // Стоимость исследования

        public int Tier { get; set; }               // Уровень технологии

        [MaxLength(200)]
        public string IconPath { get; set; } = string.Empty;       // Путь к иконке

        public int? RequiredTechId { get; set; }    // ID требуемой технологии

        [ForeignKey("RequiredTechId")]
        public virtual Tech? RequiredTech { get; set; } // Ссылка на предшественника

        // Коллекции связанных сущностей
        public virtual ICollection<Tech> UnlockedTechs { get; set; } = new List<Tech>(); // Разблокированные технологии
        public virtual ICollection<Unit> Units { get; set; } = new List<Unit>(); // Разблокированные юниты
        public virtual ICollection<Building> Buildings { get; set; } = new List<Building>(); // Разблокированные здания
    }
}
