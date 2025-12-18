
using AutoMapper;
using GamesStrategApi.Interfaces;
using GamesStrategApi.Interfaces.IServices;
using GamesStrategApi.Models;
using GamesStrategApi.Models.Services;
using GamesStrategApi.Repo;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GamesStrategApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // бд
            builder.Services.AddDbContext<StrategContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // автомаппер
            builder.Services.AddAutoMapper(typeof(Program).Assembly);


            // репозитории
            builder.Services.AddScoped<IRaceRepo, RaceRepo>();
            builder.Services.AddScoped<ITechRepo, TechRepo>();
            builder.Services.AddScoped<ICelestialBodyRepo, CelestialBodyRepo>();
            builder.Services.AddScoped<IUnitsRepo, UnitRepo>();
            builder.Services.AddScoped<IBuildRepo, BuildingRepo>();


            // Сервисы
            builder.Services.AddScoped<IRaceService, RaceServices>();
            builder.Services.AddScoped<ITechService, TechServices>();
            builder.Services.AddScoped<ICelestialBodyService, CelestialBodyServices>();
            builder.Services.AddScoped<IUnitService, UnitServices>();
            builder.Services.AddScoped<IBuildService, BuildServices>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<StrategContext>();

            //    //dbContext.Database.Migrate();

            //    //if (!dbContext.Races.Any())
            //    //{
            //    //    Seed.DatabaseSeeder.Seed(dbContext);
            //    //}
            //}

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
