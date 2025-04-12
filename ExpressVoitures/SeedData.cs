using ExpressVoitures.Data.Entities;
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
                
                if (context.Cars.Any())
                {
                    return;   
                }

                context.Cars.AddRange(
                    new Car
                    {
                        VinCode = "1HGBH41JXMN109186",
                        Brand = "Toyota",
                        Model = "Corolla",
                        Version = "LE",
                        ManufactureDate = new DateTime(2019, 05, 09, 9, 15, 0),
                        ImagePath = "https://example.com/images/toyota_corolla.jpg",
                        Repairs = new List<Repair>
                        {
                            new Repair
                            {
                                Description = "Oil change",
                                Price = 150,
                                DaysOfWork = 3
                            },
                            new Repair
                            {
                                Description = "Tire rotation",
                                Price = 100,
                                DaysOfWork = 2
                            }
                        },
                        EventHistory = new EventHistory
                        {
                            Purchase = new DateTimeOffset(new DateTime(2021, 01, 01, 0, 0, 0), TimeSpan.FromHours(-5)),
                        },
                        Price = new Price
                        {
                            Purchase = 20000,
                        }
                    },
                    new Car
                    {
                        VinCode = "1HGBH41JXMN109187",
                        Brand = "Honda",
                        Model = "Civic",
                        Version = "LX",
                        ManufactureDate = new DateTime(2016, 04, 03, 2, 15, 0),
                        ImagePath = "https://example.com/images/honda_civic.jpg",
                        Repairs = new List<Repair>
                        {
                            new Repair
                            {
                                Description = "Brake pads replacement",
                                Price = 200,
                                DaysOfWork = 4
                            },
                            new Repair
                            {
                                Description = "Battery replacement",
                                Price = 120,
                                DaysOfWork = 1
                            }
                        },
                        EventHistory = new EventHistory
                        {
                            Purchase = new DateTimeOffset(new DateTime(2022, 06, 01, 0, 0, 0), TimeSpan.FromHours(-5)),
                        },
                        Price = new Price
                        {
                            Purchase = 22000,
                        }
                    },
                    new Car
                    {
                        VinCode = "1HGBH41JXMN109188",
                        Brand = "Ford",
                        Model = "Fusion",
                        Version = "SE",
                        ManufactureDate = new DateTime(2018, 11, 15, 10, 30, 0),
                        ImagePath = "https://example.com/images/ford_fusion.jpg",
                        Repairs = new List<Repair>
                        {
                            new Repair
                            {
                                Description = "Transmission fluid change",
                                Price = 250,
                                DaysOfWork = 5
                            },
                            new Repair
                            {
                                Description = "Windshield wiper replacement",
                                Price = 50,
                                DaysOfWork = 1
                            }
                        },
                        EventHistory = new EventHistory
                        {
                            Purchase = new DateTimeOffset(new DateTime(2023, 01, 01, 0, 0, 0), TimeSpan.FromHours(-5)),
                        },
                        Price = new Price
                        {
                            Purchase = 24000,
                        }
                    },
                    new Car
                    {
                        VinCode = "1HGBH41JXMN109189",
                        Brand = "Chevrolet",
                        Model = "Malibu",
                        Version = "LT",
                        ManufactureDate = new DateTime(2017, 08, 20, 14, 45, 0),
                        ImagePath = "https://example.com/images/chevrolet_malibu.jpg",
                        Repairs = new List<Repair>
                        {
                            new Repair
                            {
                                Description = "Fuel filter replacement",
                                Price = 80,
                                DaysOfWork = 2
                            },
                            new Repair
                            {
                                Description = "Air filter replacement",
                                Price = 60,
                                DaysOfWork = 1
                            }
                        },
                        EventHistory = new EventHistory
                        {
                            Purchase = new DateTime(2021, 03, 15, 14, 45, 0),
                        },
                        Price = new Price
                        {
                            Purchase = 21000,
                        }
                    },
                    new Car
                    {
                        VinCode = "1HGBH41JXMN109190",
                        Brand = "Nissan",
                        Model = "Altima",
                        Version = "SV",
                        ManufactureDate = new DateTime(2015, 09, 25, 8, 0, 0),
                        ImagePath = "https://example.com/images/nissan_altima.jpg",
                        Repairs = new List<Repair>
                        {
                            new Repair
                            {
                                Description = "Spark plug replacement",
                                Price = 100,
                                DaysOfWork = 3
                            },
                            new Repair
                            {
                                Description = "Coolant flush",
                                Price = 120,
                                DaysOfWork = 2
                            }
                        },
                        EventHistory = new EventHistory
                        {
                            Purchase = new DateTime(2019, 11, 20, 8, 0, 0),
                        },
                        Price = new Price
                        {
                            Purchase = 23000,
                        }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
