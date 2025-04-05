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
        public DbSet<TypeDto> Types { get; set; }
        public DbSet<PrixDto> Prixs { get; set; }
        public DbSet<ReparationTypeDto> ReparationTypes { get; set; }

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
                .WithOne(r => r.Voiture);
            builder.Entity<VoitureDto>()
                .HasOne(v => v.Date)
                .WithOne(r => r.Voiture);
            builder.Entity<VoitureDto>()
                .HasOne(v => v.Reparation)
                .WithOne(r => r.Voiture);
            builder.Entity<ReparationTypeDto>()
                .HasKey(rt => new { rt.ReparationId, rt.TypeId });

            builder.Entity<ReparationTypeDto>()
                .HasOne(rt => rt.Reparation)
                .WithMany(r => r.ReparationTypes)
                .HasForeignKey(rt => rt.ReparationId);
            builder.Entity<ReparationTypeDto>()
                .HasOne(rt => rt.Type)
                .WithMany(t => t.ReparationTypes)
                .HasForeignKey(rt => rt.TypeId);
            builder.Entity<ReparationDto>()
                .HasMany(r => r.ReparationTypes)
                .WithOne(rt => rt.Reparation)
                .HasForeignKey(rt => rt.ReparationId);
            builder.Entity<TypeDto>()
                .HasMany(t => t.ReparationTypes)
                .WithOne(rt => rt.Type)
                .HasForeignKey(rt => rt.TypeId);
            builder.Entity<ReparationDto>()
                .HasOne(r => r.Voiture)
                .WithOne(v => v.Reparation);
            builder.Entity<PrixDto>()
                .HasOne(p => p.Voiture)
                .WithOne(v => v.Prix);
            builder.Entity<DateDto>()
                .HasOne(d => d.Voiture)
                .WithOne(v => v.Date);

            // Configuration des clés primaires
            builder.Entity<VoitureDto>()
                .HasKey(v => v.CodeVin);
            builder.Entity<DateDto>()
                .HasKey(d => d.Id);
            builder.Entity<ReparationDto>()
                .HasKey(r => r.Id);
            builder.Entity<TypeDto>()
                .HasKey(t => t.Id);
            builder.Entity<PrixDto>()
                .HasKey(p => p.Id);
            builder.Entity<ReparationTypeDto>()
                .HasKey(r => r.Id);


        }
    }
}
