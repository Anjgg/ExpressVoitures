using ExpressVoitures.Data.Dto;


//using ExpressVoitures.Database.TableDeLiaison;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Context
{
    public class ExpressVoituresContext : IdentityDbContext
    {
        public virtual DbSet<VoitureDto> Voitures { get; set; } = null!;
        public virtual DbSet<DateDto> Dates { get; set; } = null!;
        public virtual DbSet<ReparationDto> Reparations { get; set; } = null!;
        public virtual DbSet<PrixDto> Prixs { get; set; } = null!;
        public virtual DbSet<TypeDto> Types { get; set; } = null!;


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
                .HasForeignKey<PrixDto>(p => p.VoitureId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<VoitureDto>()
                .HasOne(v => v.Date)
                .WithOne(d => d.Voiture)
                .HasForeignKey<DateDto>(d => d.VoitureId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<VoitureDto>()
                .HasOne(v => v.Reparation)
                .WithOne(r => r.Voiture)
                .HasForeignKey<ReparationDto>(r => r.VoitureId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ReparationDto>()
                .HasMany(r => r.Types)
                .WithMany(t => t.Reparations)
                .UsingEntity(j => j.ToTable("ReparationType"));
        }
    }
}
