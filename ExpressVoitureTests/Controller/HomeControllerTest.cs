using ExpressVoitures.Controllers;
using ExpressVoitures.Data.Models;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ExpressVoitureTests.Controller
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IExpressVoituresService> _serviceMock = new Mock<IExpressVoituresService>();

        [TestInitialize]
        public void Initialize()
        {
            _serviceMock = new Mock<IExpressVoituresService>();
        }

        [TestMethod]
        public void TestHomeControllerConstructor()
        {
            // Act
            var controller = new HomeController(_serviceMock.Object);
            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task CarControllerIndex_ValidId_Success()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetAllHomeCars())
                .ReturnsAsync(new List<HomeCarModel>());
            var controller = new HomeController(_serviceMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task CarControllerIndex_ListNull_DontThrowException()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetAllHomeCars())
                .ReturnsAsync((List<HomeCarModel>)null!);
            var controller = new HomeController(_serviceMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestHomeControllerContact()
        {
            // Arrange
            var controller = new HomeController(_serviceMock.Object);

            // Act
            var result = controller.Contact();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
