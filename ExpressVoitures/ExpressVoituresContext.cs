using ExpressVoitures.Data.Entities;


//using ExpressVoitures.Database.TableDeLiaison;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ExpressVoitures
{
    public class ExpressVoituresContext : IdentityDbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<EventHistory> EventHistorys { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Price> Prices { get; set; }
       

        public ExpressVoituresContext()
        {
        }

        public ExpressVoituresContext(DbContextOptions<ExpressVoituresContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Setting decimal properties
            foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(8,2)");
            }

            // Relationship configuration
            builder.Entity<Car>()
                .HasOne(v => v.Price)
                .WithOne(p => p.Car)
                .HasForeignKey<Price>(p => p.CarId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Car>()
                .HasOne(v => v.EventHistory)
                .WithOne(d => d.Car)
                .HasForeignKey<EventHistory>(d => d.CarId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Car>()
                .HasMany(v => v.Repairs)
                .WithOne(r => r.Car)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
