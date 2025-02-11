using ExpressVoitures.Database.Dto;
using ExpressVoitures.Database.TableDeLiaison;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures
{
    public class ExpressVoituresContext : IdentityDbContext
    {
        
        
        public DbSet<OperationDto> Operations { get; set; }
        public DbSet<VoitureDto> Voitures { get; set; }
        public DbSet<ReparationDto> Reparations { get; set; }
        //public DbSet<OperationReparationDto> OperationReparations { get; set; }

        public ExpressVoituresContext(DbContextOptions<ExpressVoituresContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OperationDto>()
                .HasOne<VoitureDto>(s => s.VoitureDto)
                .WithOne(ad => ad.OperationDto)
                .HasForeignKey<VoitureDto>(ad => ad.OperationId);

            modelBuilder.Entity<VoitureDto>()
                .HasOne<OperationDto>(s => s.OperationDto)
                .WithOne(ad => ad.VoitureDto)
                .HasForeignKey<VoitureDto>(ad => ad.OperationId);

            modelBuilder.Entity<OperationDto>()
                .HasMany(s => s.ReparationDtos)
                .WithMany(ad => ad.OperationDtos)
                .UsingEntity(j => j.ToTable("OperationReparationDto"));


            modelBuilder.Entity<ReparationDto>()
                .HasMany(s => s.OperationDtos)
                .WithMany(ad => ad.ReparationDtos);

            // Fix decimal precision and scale
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var decimalProperties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));
                foreach (var property in decimalProperties)
                {
                    property.SetPrecision(8);
                    property.SetScale(2);
                }
            }

        }
    }
}
