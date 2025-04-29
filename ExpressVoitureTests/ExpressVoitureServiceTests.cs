using AutoFixture;
using ExpressVoitures.Context;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ExpressVoitureTests
{
    [TestClass]
    public class VoitureServiceTests
    {
        private IExpressVoituresService? _voitureService;
        private Mock<DbSet<VoitureDto>> _mockVoitureSet = new Mock<DbSet<VoitureDto>>();
        private Mock<ExpressVoituresContext>? _mockContext;
        private IFixture _fixture = new Fixture(); 

        [TestInitialize]
        public void Setup()
        {
            // Initialisation des mocks
            _mockVoitureSet = new Mock<DbSet<VoitureDto>>();
            _mockContext = new Mock<ExpressVoituresContext>();

            // Configuration du mock DbContext
            _mockContext.Setup(c => c.Voitures).Returns(_mockVoitureSet.Object);

            // Création du service avec le mock du contexte
            _voitureService = new ExpressVoituresService(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetAllHomeCars_ReturnsTransformedData()
        {
            // Arrange
            var a = _fixture.Create<DateTimeOffset>();
            var b = _fixture.Create<DateTimeOffset>();
            var testData = new List<VoitureDto>
            {
                new VoitureDto
                {
                    Id = 1,
                    Marque = "Toyota",
                    Modele = "Corolla",
                    AnneeFabrication = a,
                    ImagePath = "/images/toyota-corolla.jpg",
                    Prix = new PrixDto { PrixVente = 15000 },
                    Date = new DateDto { DateVente = null }
                },
                new VoitureDto
                {
                    Id = 2,
                    Marque = "Honda",
                    Modele = "Civic",
                    AnneeFabrication = b,
                    ImagePath = "/images/honda-civic.jpg",
                    Prix = new PrixDto { PrixVente = 14000 },
                    Date = new DateDto { DateVente = DateTime.Now }
                }
            };

            var queryableData = testData.AsQueryable();
            _mockVoitureSet.As<IQueryable<VoitureDto>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            _mockVoitureSet.As<IQueryable<VoitureDto>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            _mockVoitureSet.As<IQueryable<VoitureDto>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            _mockVoitureSet.As<IQueryable<VoitureDto>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());

            // Simulation de ToListAsync()
            _mockVoitureSet.Setup(m => m.Include(It.IsAny<string>())).Returns(_mockVoitureSet.Object);
            _mockVoitureSet.Setup(m => m.ToListAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(testData);

            // Act
            var result = await _voitureService.GetAllHomeCars();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            // Vérification des propriétés transformées
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Toyota", result[0].Marque);
            Assert.AreEqual("Corolla", result[0].Modele);
            Assert.AreEqual(a, result[0].AnneeFabrication);
            Assert.AreEqual("/images/toyota-corolla.jpg", result[0].ImagePath);
            Assert.AreEqual(15000, result[0].PrixVente);
            Assert.IsTrue(result[0].IsAvailable);

            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("Honda", result[1].Marque);
            Assert.AreEqual("Civic", result[1].Modele);
            Assert.AreEqual(b, result[1].AnneeFabrication);
            Assert.AreEqual("/images/honda-civic.jpg", result[1].ImagePath);
            Assert.AreEqual(14000, result[1].PrixVente);
            Assert.IsFalse(result[1].IsAvailable);
        }
    }
}
