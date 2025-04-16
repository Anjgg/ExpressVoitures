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
                        AnneeFabrication = new DateTimeOffset(1985, 5, 12, 0,0,0, new TimeSpan()),
                        ImagePath = "https://example.com/images/toyota_corolla.jpg",
                        Date = new DateDto
                        {
                            DateAchat = new DateTimeOffset(2023, 10, 1, 0, 0, 0, new TimeSpan())
                        },
                        Prix = new PrixDto
                        {
                            PrixAchat = 15000,
                            PrixReparation = 300,
                            PrixVente = 15800
                        },
                        Reparation = new ReparationDto
                        {
                            Types = new List<TypeDto>
                            {
                                new TypeDto { Description = "Changement d'huile", Prix = 100, Duree = 3 },
                                new TypeDto { Description = "Révision", Prix = 200, Duree = 4 }
                            }
                        },
                    },
                    new VoitureDto
                    {
                        CodeVin = "1HGBH41JXMN109187",
                        Marque = "Honda",
                        Modele = "Civic",
                        Finition = "LX",
                        AnneeFabrication = new DateTimeOffset(1995, 7, 20, 0, 0, 0, new TimeSpan()),
                        ImagePath = "https://example.com/images/honda_civic.jpg",
                        Date = new DateDto
                        {
                            DateAchat = new DateTimeOffset(2023, 10, 2, 0, 0, 0, new TimeSpan())
                        },
                        Prix = new PrixDto
                        {
                            PrixAchat = 16000,
                            PrixReparation = 800,
                            PrixVente = 17300
                        },
                        Reparation = new ReparationDto
                        {
                            Types = new List<TypeDto>
                            {
                                new TypeDto { Description = "Changement de pneus", Prix = 500, Duree = 2 },
                                new TypeDto { Description = "Réparation de frein", Prix = 300, Duree = 5 }
                            }
                        },
                    },
                    new VoitureDto
                    {
                        CodeVin = "1HGBH41JXMN109188",
                        Marque = "Ford",
                        Modele = "Fusion",
                        Finition = "SE",
                        AnneeFabrication = new DateTimeOffset(2005, 3, 15, 0, 0, 0, new TimeSpan()),
                        ImagePath = "https://example.com/images/ford_fusion.jpg",
                        Date = new DateDto
                        {
                            DateAchat = new DateTimeOffset(2023, 10, 3, 0, 0, 0, new TimeSpan())
                        },
                        Prix = new PrixDto
                        {
                            PrixAchat = 17000,
                            PrixReparation = 600,
                            PrixVente = 18100
                        },
                        Reparation = new ReparationDto
                        {
                            Types = new List<TypeDto>
                            {
                                new TypeDto { Description = "Changement de batterie", Prix = 200, Duree = 1 },
                                new TypeDto { Description = "Réparation de climatisation", Prix = 400, Duree = 6 }
                            }
                        },
                    },
                    new VoitureDto
                    {
                        CodeVin = "1HGBH41JXMN109189",
                        Marque = "Chevrolet",
                        Modele = "Malibu",
                        Finition = "LT",
                        AnneeFabrication = new DateTimeOffset(2010, 8, 25, 0, 0, 0, new TimeSpan()),
                        ImagePath = "https://example.com/images/chevrolet_malibu.jpg",
                        Date = new DateDto
                        {
                            DateAchat = new DateTimeOffset(2023, 10, 4, 0, 0, 0, new TimeSpan())
                        },
                        Prix = new PrixDto
                        {
                            PrixAchat = 18000,
                            PrixReparation = 850,
                            PrixVente = 19350
                        },
                        Reparation = new ReparationDto
                        {
                            Types = new List<TypeDto>
                            {
                                new TypeDto { Description = "Changement de filtre à air", Prix = 50, Duree = 1 },
                                new TypeDto { Description = "Réparation de transmission", Prix = 800, Duree = 10 }
                            }
                        },
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
