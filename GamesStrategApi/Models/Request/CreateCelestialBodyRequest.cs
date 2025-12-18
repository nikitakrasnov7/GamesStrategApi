namespace GamesStrategApi.Models.Request
{
    public class CreateCelestialBodyRequest
    {
        // Название небесного тела
        public string Name { get; set; } = string.Empty;

        // Тип небесного тела
        public string Type { get; set; } = string.Empty;

        // Размер небесного тела
        public int Size { get; set; } = 1;

        // Координата X
        public int PositionX { get; set; }

        // Координата Y
        public int PositionY { get; set; }

        // Богатство ресурсов
        public int ResourceRichness { get; set; } = 1;
    }
}
