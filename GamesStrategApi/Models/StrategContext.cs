using Microsoft.EntityFrameworkCore;

namespace GamesStrategApi.Models
{
    public class StrategContext: DbContext
    {
        public StrategContext(DbContextOptions<StrategContext> options) : base(options) 
        {
            
        }
        // DbSet'ы для таблиц
        public DbSet<Race> Races { get; set; }              // Таблица рас
        public DbSet<Tech> Techs { get; set; }              // Таблица технологий
        public DbSet<CelestialBody> CelestialBodies { get; set; } // Таблица небесных тел
        public DbSet<Unit> Units { get; set; }              // Таблица юнитов
        public DbSet<Building> Buildings { get; set; }      // Таблица зданий

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Связь технологий с предшественниками
            modelBuilder.Entity<Tech>()
                .HasOne(t => t.RequiredTech)
                .WithMany(t => t.UnlockedTechs)
                .HasForeignKey(t => t.RequiredTechId)
                .OnDelete(DeleteBehavior.Restrict); // Запрет каскадного удаления

            // Связь рас с юнитами
            modelBuilder.Entity<Race>()
                .HasMany(r => r.Units)
                .WithOne(u => u.Race)
                .HasForeignKey(u => u.RaceId)
                .OnDelete(DeleteBehavior.Cascade); // При удалении расы удалить юниты

            // Связь технологий с юнитами
            modelBuilder.Entity<Tech>()
                .HasMany(t => t.Units)
                .WithOne(u => u.UnlockingTech)
                .HasForeignKey(u => u.UnlockingTechId)
                .OnDelete(DeleteBehavior.SetNull); // При удалении технологии оставить юниты

            // Связь технологий со зданиями
            modelBuilder.Entity<Tech>()
                .HasMany(t => t.Buildings)
                .WithOne(b => b.UnlockingTech)
                .HasForeignKey(b => b.UnlockingTechId)
                .OnDelete(DeleteBehavior.SetNull); // При удалении технологии оставить здания
        }
    }
}
