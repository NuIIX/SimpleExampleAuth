using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SimpleExampleAuth.Models
{
    public class CarContext : DbContext
    {
        public DbSet<Car> Cars { get; set; } = null!;
        public CarContext(DbContextOptions<CarContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasData(
                    new Car { Id = 1, Brand = "Toyota", Model = "Corolla", EngineCapacity = 1.8f, EnginePower = 140, CPP = "Sedan", MaxSpeed = 180.0f },
                    new Car { Id = 2, Brand = "Ford", Model = "Mustang", EngineCapacity = 5.0f, EnginePower = 450, CPP = "Coupe", MaxSpeed = 250.0f }
            );
        }
    }
}
