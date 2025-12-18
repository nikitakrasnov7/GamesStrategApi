namespace GamesStrategApi.Models.DTOss
{
    public class CelestialBodyDto
    {
        public int Id { get; set; }                 // ID небесного тела
        public string Name { get; set; } = string.Empty;           // Название
        public string Type { get; set; } = string.Empty;           // Тип
        public int Size { get; set; }               // Размер
        public int PositionX { get; set; }          // Координата X
        public int PositionY { get; set; }          // Координата Y
        public int ResourceRichness { get; set; }   // Богатство ресурсов
    }
}
