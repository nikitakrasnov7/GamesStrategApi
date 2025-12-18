using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStrategApi.Models
{
    [Table("CelestialBodies")]
    public class CelestialBody
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }                 // ID небесного тела

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;           // Название

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;           // Тип небесного тела

        public int Size { get; set; }               // Размер

        public int PositionX { get; set; }          // Координата X

        public int PositionY { get; set; }          // Координата Y

        [Range(1, 10)]
        public int ResourceRichness { get; set; } = 1; // Богатство ресурсов (1-10)
    }
}
