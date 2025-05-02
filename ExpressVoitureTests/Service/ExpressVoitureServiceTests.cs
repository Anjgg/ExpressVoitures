using ExpressVoitures.Context;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using ExpressVoitures.Services;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitureTests.Service
{
    [TestClass]
    public class VoitureServiceTests
    {
        private ExpressVoituresContext? _context;
        private ExpressVoituresService? _service;

        [TestMethod]
        public async Task GetAllHomeCars_ReturnsTransformedData()
        {
            // Arrange
            var options = GetOptionsDb();
            using (_context = new ExpressVoituresContext(options))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                await SeedDb(1);
                await SeedDb(2);
                _service = new ExpressVoituresService(_context);

                // Act
                var listResult = await _service.GetAllHomeCars();

                // Assert
                Assert.IsNotNull(listResult);
                Assert.AreEqual(2, listResult.Count);
                Assert.AreEqual(1, listResult[0].Id);
                Assert.AreEqual("Marque1", listResult[0].Marque);
                Assert.AreEqual("Modele1", listResult[0].Modele);
                Assert.AreEqual(2, listResult[1].Id);
                Assert.AreEqual("Marque2", listResult[1].Marque);
                Assert.AreEqual("Modele2", listResult[1].Modele);
            }
        }

        [TestMethod]
        public async Task GetAllHomeCars_EmptyDatabase_ReturnsEmptyList()
        {
            // Arrange
            var options = GetOptionsDb();
            using (_context = new ExpressVoituresContext(options))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                _service = new ExpressVoituresService(_context);
                // Act
                var result = await _service.GetAllHomeCars();

                // Assert
                Assert.AreEqual(0, result.Count);
            }
        }

        [TestMethod]
        public async Task GetCarAsync_ValidId_ReturnsCar()
        {
            // Arrange
            var options = GetOptionsDb();

            using (_context = new ExpressVoituresContext(options))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                await SeedDb(1);
                _service = new ExpressVoituresService(_context);

                // Act
                var result = await _service.GetCarAsync(1);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Voiture.Id);
                Assert.AreEqual("TESTCASENUM1", result.Voiture.CodeVin);
            }
        }

        [TestMethod]
        public async Task GetCarAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            var options = GetOptionsDb();

            using (_context = new ExpressVoituresContext(options))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                await SeedDb(1);
                _service = new ExpressVoituresService(_context);

                // Act
                var result = await _service.GetCarAsync(999);

                // Assert
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public async Task CreateVoitureAsync_Success()
        {
            // Arrange
            VoitureProfileModel voitureProfileModel = new VoitureProfileModel
            {
                Voiture = new VoitureModel
                {
                    CodeVin = "TESTCASECREATE",
                    Marque = "Toyota",
                    Modele = "Corolla",
                    Finition = "LE",
                    AnneeFabrication = new DateTimeOffset(1985, 5, 12, 0, 0, 0, new TimeSpan()),
                    ImagePath = "/images/imagedefault.jpg"
                },
                Date = new DateModel
                {
                    DateAchat = new DateTimeOffset(2023, 10, 1, 0, 0, 0, new TimeSpan())
                },
                Prix = new PrixModel
                {
                    PrixAchat = 15000,
                    PrixReparation = 300,
                    PrixVente = 15800
                }

            };
            var options = GetOptionsDb();

            using (_context = new ExpressVoituresContext(options))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                _service = new ExpressVoituresService(_context);

                // Act
                var result = await _service.CreateVoitureAsync(voitureProfileModel);

                // Assert
                var createdCar = await _context.Voitures.FindAsync(result.Voiture.Id);
                Assert.IsNotNull(result);
                Assert.IsNotNull(createdCar);
                Assert.AreEqual(1, _context.Voitures.Count());
                Assert.AreEqual(1, result.Voiture.Id);
                Assert.AreEqual(voitureProfileModel.Voiture.CodeVin, createdCar.CodeVin);
            }
        }

        [TestMethod]
        public async Task UpdateCarAsync_Success()
        {
            // Arrange
            VoitureProfileModel voitureProfileModel = new VoitureProfileModel
            {
                Voiture = new VoitureModel
                {
                    Id = 1,
                    CodeVin = "TESTCASEUPDATE",
                    Marque = "UPDATE1",
                    Modele = "UPDATE1",
                    Finition = "UPDATE1",
                },

            };
            var options = GetOptionsDb();

            using (_context = new ExpressVoituresContext(options))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                await SeedDb(1);
                _service = new ExpressVoituresService(_context);

                // Act
                var result = await _service.UpdateCarAsync(voitureProfileModel);

                // Assert
                var updatedCar = await _context.Voitures.FindAsync(voitureProfileModel.Voiture.Id);
                Assert.IsNotNull(result);
                Assert.IsNotNull(updatedCar);
                Assert.AreEqual(updatedCar.CodeVin, result.Voiture.CodeVin);
                Assert.AreEqual(updatedCar.Marque, result.Voiture.Marque);
                Assert.AreEqual(updatedCar.Modele, result.Voiture.Modele);
                Assert.AreEqual(updatedCar.Finition, result.Voiture.Finition);
            }
        }

        [TestMethod]
        public async Task UpdateCarAsync_InvalidId_ReturnException()
        {
            // Arrange
            VoitureProfileModel voitureProfileModel = new VoitureProfileModel
            {
                Voiture = new VoitureModel
                {
                    Id = 999,
                }
            };
            var options = GetOptionsDb();

            using (_context = new ExpressVoituresContext(options))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                await SeedDb(1);
                _service = new ExpressVoituresService(_context);

                // Act & Assert
                var resultException = await Assert.ThrowsExceptionAsync<Exception>(async () =>
                {
                    await _service.UpdateCarAsync(voitureProfileModel);
                });

                Assert.AreEqual($"Voiture with Id {voitureProfileModel.Voiture.Id} not found.", resultException.Message);
            }
        }

        [TestMethod]
        public async Task DeleteCarAsync_Success()
        {
            // Arrange
            var options = GetOptionsDb();

            using (_context = new ExpressVoituresContext(options))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                await SeedDb(1);
                await SeedDb(2);
                _service = new ExpressVoituresService(_context);

                // Act
                await _service.DeleteCarAsync(1);

                // Assert
                var listAllCars = await _context.Voitures.ToListAsync();
                Assert.AreEqual(1, listAllCars.Count);
                Assert.AreEqual(2, listAllCars[0].Id);
            }
        }

        private async Task SeedDb(int id)
        {
            var voitureDto = new VoitureDto
            {
                Id = id,
                CodeVin = $"TESTCASENUM{id}",
                Marque = $"Marque{id}",
                Modele = $"Modele{id}",
                Finition = $"Finition{id}",
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
            };

            _context!.Voitures.Add(voitureDto);
            await _context.SaveChangesAsync();
        }

        private DbContextOptions<ExpressVoituresContext> GetOptionsDb() => new DbContextOptionsBuilder<ExpressVoituresContext>()
                                                                            .UseInMemoryDatabase($"TestDatabase_{Guid.NewGuid()}")
                                                                            .Options;
    }
}
