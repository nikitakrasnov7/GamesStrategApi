using GamesStrategApi.Models;

namespace GamesStrategApi.Seed
{
    public static class DatabaseSeeder
    {
        public static void Seed(StrategContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var races = new List<Race>
            {
                new Race { Name = "Терранская Республика", Description = "Люди, стремящиеся к экспансии",
                          HomeWorldType = "Терра", UniqueBonus = "+10% к обороне на планетах", IsPlayable = true },
                new Race { Name = "Кел-Дори Иерархия", Description = "Древняя раса насекомоподобных",
                          HomeWorldType = "Пустыня", UniqueBonus = "+1 к дальности атаки кораблей", IsPlayable = true },
                new Race { Name = "Древние Автоматоны", Description = "Искусственный интеллект, оставшийся без создателей",
                          HomeWorldType = "Металлическая", UniqueBonus = "Не требует пищи", IsPlayable = false },
                new Race { Name = "Консорциум Торговцев", Description = "Меркантильная раса торговцев",
                          HomeWorldType = "Океан", UniqueBonus = "+30% к доходу от торговли", IsPlayable = false },
                new Race { Name = "Дикие Ксеносы", Description = "Агрессивные инопланетные формы жизни",
                          HomeWorldType = "Джунгли", UniqueBonus = "Роение, +20% к урону в численном превосходстве", IsPlayable = false }
            };
            context.Races.AddRange(races);
            context.SaveChanges();

            var techs = new List<Tech>
            {
                new Tech { Name = "Гипердвигатель 1-го ур.", Description = "Позволяет перемещаться между звездами",
                          ResearchCost = 100, Tier = 1, IconPath = "/tech/hyperdrive1.png" },
                new Tech { Name = "Плазменные Батареи", Description = "Улучшенное энергетическое оружие",
                          ResearchCost = 250, Tier = 2, IconPath = "/tech/plasma.png" },
                new Tech { Name = "Колонизация", Description = "Технология колонизации новых миров",
                          ResearchCost = 80, Tier = 1, IconPath = "/tech/colonization.png" },
                new Tech { Name = "Терраформирование", Description = "Изменение климата планет",
                          ResearchCost = 500, Tier = 3, IconPath = "/tech/terraforming.png" },
                new Tech { Name = "Стелс-поля", Description = "Технология маскировки кораблей",
                          ResearchCost = 300, Tier = 2, IconPath = "/tech/stealth.png" }
            };
            context.Techs.AddRange(techs);
            context.SaveChanges();

            techs[1].RequiredTechId = techs[0].Id; 
            techs[3].RequiredTechId = techs[2].Id; 
            techs[4].RequiredTechId = techs[0].Id; 
            context.SaveChanges();

            var celestialBodies = new List<CelestialBody>
            {
                new CelestialBody { Name = "Новая Земля", Type = "Planet", Size = 10, PositionX = 100, PositionY = 150, ResourceRichness = 8 },
                new CelestialBody { Name = "Пустыня Калли", Type = "Planet", Size = 8, PositionX = 250, PositionY = 80, ResourceRichness = 5 },
                new CelestialBody { Name = "Туманность Призрак", Type = "Nebula", Size = 25, PositionX = 400, PositionY = 300, ResourceRichness = 2 },
                new CelestialBody { Name = "Пояс астероидов 'Дробилка'", Type = "Asteroid", Size = 15, PositionX = 50, PositionY = 400, ResourceRichness = 7 },
                new CelestialBody { Name = "Черная дыра 'Хаос'", Type = "BlackHole", Size = 1, PositionX = 500, PositionY = 500, ResourceRichness = 0 }
            };
            context.CelestialBodies.AddRange(celestialBodies);
            context.SaveChanges();

            var units = new List<Unit>
            {
                new Unit { Name = "Разведчик 'Зонд'", UnitType = "Scout", Health = 100, Shield = 20, Damage = 10,
                          MovementSpeed = 8, ProductionCost = 50, RaceId = races[0].Id, UnlockingTechId = techs[0].Id },
                new Unit { Name = "Линкор 'Непобедимый'", UnitType = "Capital", Health = 1000, Shield = 500, Damage = 200,
                          MovementSpeed = 2, ProductionCost = 800, RaceId = races[0].Id, UnlockingTechId = techs[1].Id },
                new Unit { Name = "Роевой Истребитель", UnitType = "Fighter", Health = 50, Shield = 0, Damage = 30,
                          MovementSpeed = 6, ProductionCost = 40, RaceId = races[4].Id, UnlockingTechId = null },
                new Unit { Name = "Торговый Фрегат", UnitType = "Transport", Health = 300, Shield = 100, Damage = 5,
                          MovementSpeed = 4, ProductionCost = 200, RaceId = races[3].Id, UnlockingTechId = techs[2].Id },
                new Unit { Name = "Колонизационный корабль", UnitType = "Colonizer", Health = 400, Shield = 150, Damage = 0,
                          MovementSpeed = 3, ProductionCost = 300, RaceId = null, UnlockingTechId = techs[2].Id }
            };
            context.Units.AddRange(units);
            context.SaveChanges();

            var buildings = new List<Building>
            {
                new Building { Name = "Шахта", BuildingType = "Economic", Description = "Добывает минералы",
                             ProductionCost = 100, IncomeBonus = 5, UnlockingTechId = null },
                new Building { Name = "Исследовательская лаборатория", BuildingType = "Scientific", Description = "Производит научные очки",
                             ProductionCost = 150, IncomeBonus = 5, UnlockingTechId = null },
                new Building { Name = "Щитовой генератор", BuildingType = "Military", Description = "Защищает планету",
                             ProductionCost = 300, IncomeBonus = 0, UnlockingTechId = techs[1].Id },
                new Building { Name = "Фабрика роботов", BuildingType = "Economic", Description = "Производит дронов",
                             ProductionCost = 400, IncomeBonus = 10, UnlockingTechId = techs[3].Id },
                new Building { Name = "Стелс-вышка", BuildingType = "Military", Description = "Обнаруживает невидимые цели",
                             ProductionCost = 250, IncomeBonus = 0, UnlockingTechId = techs[4].Id }
            };
            context.Buildings.AddRange(buildings);
            context.SaveChanges();
        }
    }
}

