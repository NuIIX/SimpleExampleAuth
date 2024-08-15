using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SimpleExampleAuth.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, UserName = "Tom", Email = "tom@gmail.com", Password = "12345", FirstName = "", LastName = "", DOB = new DateTime(2000, 12, 11), Phone = 891334567890 },
                    new User { Id = 2, UserName = "Bob", Email = "bob@gmail.com", Password = "55555", FirstName = "", LastName = "", DOB = null, Phone = null }
            );
        }
    }
}
