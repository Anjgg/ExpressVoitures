using ExpressVoitures.Data.Dto;


//using ExpressVoitures.Database.TableDeLiaison;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ExpressVoitures
{
    public class ExpressVoituresContext : IdentityDbContext
    {
        public DbSet<VoitureDto> Voitures { get; set; }
        public DbSet<DateDto> Dates { get; set; }
        public DbSet<ReparationDto> Reparations { get; set; }
        public DbSet<PrixDto> Prixs { get; set; }
       

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

            // Configuration des propriétés décimales
            foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(8,2)");
            }

            // Configuration des relations
            builder.Entity<VoitureDto>()
                .HasOne(v => v.Prix)
                .WithOne(p => p.Voiture)
                .HasForeignKey<PrixDto>(p => p.CodeVin)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<VoitureDto>()
                .HasOne(v => v.Date)
                .WithOne(d => d.Voiture)
                .HasForeignKey<DateDto>(d => d.CodeVin)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<VoitureDto>()
                .HasMany(v => v.Reparations)
                .WithOne(r => r.Voiture)
                .HasForeignKey(r => r.CodeVin)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
