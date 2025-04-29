using ExpressVoitures.Context;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using ExpressVoitures.Services;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitureTests
{
    [TestClass]
    public class VoitureServiceTests
    {
        private DbContextOptions<ExpressVoituresContext>? _contextOptions;
        private DbContextOptions<ExpressVoituresContext>? _contextEmptyOptions;
        private ExpressVoituresContext? _context;
        private ExpressVoituresService? _service;
        private List<VoitureDto> _listVoitures = new List<VoitureDto>();
        

        [TestInitialize]
        public void Setup()
        {
            _contextOptions = new DbContextOptionsBuilder<ExpressVoituresContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using (_context = new ExpressVoituresContext(_contextOptions))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();

                _listVoitures = new List<VoitureDto>() {
                    new VoitureDto {
                        Id = 1,
                        CodeVin = "1HGH41JXMN109156",
                        Marque = "Toyota",
                        Modele = "Corolla",
                        Finition = "LE",
                        AnneeFabrication = new DateTimeOffset(1985, 5, 12, 0, 0, 0, new TimeSpan()),
                        ImagePath = "/images/imagedefault.jpg",
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
                    new VoitureDto {
                        Id = 2,
                        CodeVin = "1HBH41JXMN109167",
                        Marque = "Honda",
                        Modele = "Civic",
                        Finition = "LX",
                        AnneeFabrication = new DateTimeOffset(1989, 7, 20, 0, 0, 0, new TimeSpan()),
                        ImagePath = "/images/imagedefault.jpg",
                        Date = new DateDto
                        {
                            DateAchat = new DateTimeOffset(2023, 10, 2, 0, 0, 0, new TimeSpan()),
                            DateVente = new DateTimeOffset(2025, 5, 1, 0, 0, 0, new TimeSpan())
                        },
                        Prix = new PrixDto
                        {
                            PrixAchat = 16000,
                            PrixReparation = 500,
                            PrixVente = 17000
                        },
                        Reparation = new ReparationDto
                        {
                            Types = new List<TypeDto>
                                {
                                    new TypeDto { Description = "Changement de freins", Prix = 150, Duree = 5 },
                                    new TypeDto { Description = "Révision", Prix = 350, Duree = 6 }
                                }
                        },
                    }};

                _context.Voitures.Add(_listVoitures[0]);
                _context.Voitures.Add(_listVoitures[1]);
                _context.SaveChangesAsync();
            }

            _contextEmptyOptions = new DbContextOptionsBuilder<ExpressVoituresContext>()
                .UseInMemoryDatabase("EmptyTestDatabase")
                .Options;
            using (_context = new ExpressVoituresContext(_contextEmptyOptions))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
            };
        }

        [TestMethod]
        public async Task GetAllHomeCars_ReturnsTransformedData()
        {
            // Arrange
            List<HomeCarModel> result;
            using (_context = new ExpressVoituresContext(_contextOptions!))
            {
                _service = new ExpressVoituresService(_context);

                // Act
                result = await _service.GetAllHomeCars();
            }

            for (int i = 0; i < result.Count; i++)
            {
                var model = result[i];
                var voiture = _listVoitures[i];
                // Assert
                Assert.AreEqual(voiture.Id, model.Id);
                Assert.AreEqual(voiture.Marque, model.Marque);
                Assert.AreEqual(voiture.Modele, model.Modele);
                Assert.AreEqual(voiture.AnneeFabrication, model.AnneeFabrication);
                Assert.AreEqual(voiture.ImagePath, model.ImagePath);
                Assert.AreEqual(voiture.Prix.PrixVente, model.PrixVente);
                Assert.AreEqual(voiture.Date.DateVente == null, model.IsAvailable);
            }
        }

        [TestMethod]
        public async Task GetAllHomeCars_EmptyDatabase_ReturnsEmptyList()
        {
            // Arrange
            List<HomeCarModel> result;
            using (_context = new ExpressVoituresContext(_contextEmptyOptions!))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                _service = new ExpressVoituresService(_context);
                // Act
                result = await _service.GetAllHomeCars();
            }
            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetCarAsync_ValidId_ReturnsCar()
        {
            // Arrange
            VoitureProfileModel? result;
            using (_context = new ExpressVoituresContext(_contextOptions!))
            {
                _service = new ExpressVoituresService(_context);
                // Act
                result = await _service.GetCarAsync(1);
            }
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Voiture.Id);
            Assert.AreEqual("1HGH41JXMN109156", result.Voiture.CodeVin);
        }

        [TestMethod]
        public async Task GetCarAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            VoitureProfileModel? result;
            using (_context = new ExpressVoituresContext(_contextOptions!))
            {
                _service = new ExpressVoituresService(_context);
                // Act
                result = await _service.GetCarAsync(999);
            }
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetCarAsync_EmptyDatabase_ReturnsNull()
        {
            // Arrange
            VoitureProfileModel? result;
            using (_context = new ExpressVoituresContext(_contextEmptyOptions!))
            {
                _service = new ExpressVoituresService(_context);
                // Act
                result = await _service.GetCarAsync(1);
            }
            // Assert
            Assert.IsNull(result);
        }


    }
}
