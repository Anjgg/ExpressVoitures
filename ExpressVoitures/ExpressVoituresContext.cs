using ExpressVoitures.Dto;

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

        public ExpressVoituresContext(DbContextOptions<ExpressVoituresContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var property in builder.Model.GetEntityTypes()
                         .SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(8,2)");
            }
        }
    }
}
