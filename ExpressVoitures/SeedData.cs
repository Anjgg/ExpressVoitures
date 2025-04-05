using ExpressVoitures.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ExpressVoituresContext(
                serviceProvider.GetRequiredService<DbContextOptions<ExpressVoituresContext>>()))
            {
                
                if (context.Voitures.Any())
                {
                    return;   
                }

                context.Voitures.AddRange(
                    new VoitureDto
                    {
                        CodeVin = "1HGBH41JXMN109186",
                        Marque = "Toyota",
                        Modele = "Corolla",
                        Finition = "LE",
                        Annee = 2020,
                        ImagePath = "https://example.com/images/toyota_corolla.jpg",
                    },
                    new VoitureDto
                    {
                        CodeVin = "1HGBH41JXMN109187",
                        Marque = "Honda",
                        Modele = "Civic",
                        Finition = "LX",
                        Annee = 2020,
                    },
                    new VoitureDto
                    {
                        CodeVin = "1HGBH41JXMN109188",
                        Marque = "Ford",
                        Modele = "Fusion",
                        Finition = "SE",
                        Annee = 2020,
                    },
                    new VoitureDto
                    {
                        CodeVin = "1HGBH41JXMN109189",
                        Marque = "Chevrolet",
                        Modele = "Malibu",
                        Finition = "LT",
                        Annee = 2020,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
