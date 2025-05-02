using ExpressVoitures.Controllers;
using ExpressVoitures.Data.Models;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressVoitureTests.Controller
{
    [TestClass]
    public class CarControllerTests
    {
        private Mock<IExpressVoituresService> _serviceMock = new Mock<IExpressVoituresService>();

        [TestInitialize]
        public void Initialize()
        {
            _serviceMock = new Mock<IExpressVoituresService>();
        }

        [TestMethod]
        public void TestCarControllerConstructor()
        {
            // Arrange

            // Act
            var controller = new CarController(_serviceMock.Object);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task TestCarControllerIndex_ValidId_Success()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetCarAsync(It.IsAny<int>()))
                .ReturnsAsync(new VoitureProfileModel());

            var controller = new CarController(_serviceMock.Object);
            int id = 1;

            // Act
            var result = await controller.Index(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task TestCarControllerIndex_InvalidId_Redirect()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetCarAsync(It.IsAny<int>()))
                .ReturnsAsync((VoitureProfileModel)null!);
            var controller = new CarController(_serviceMock.Object);
            int id = 1;

            // Act
            var result = await controller.Index(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task TestCarControllerCreateGet()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetNewVoitureProfileModelAsync())
                .ReturnsAsync(new VoitureProfileModel());
            var controller = new CarController(_serviceMock.Object);

            // Act
            var result = await controller.Create();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task TestCarControllerCreatePost_ValidModel_Success()
        {
            // Arrange
            var model = new VoitureProfileModel();
            _serviceMock.Setup(s => s.CreateVoitureAsync(model))
                .ReturnsAsync(model);
            var controller = new CarController(_serviceMock.Object);

            // Act
            var result = await controller.Create(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task TestCarControllerCreatePost_InvalidModel_ReturnsView()
        {
            // Arrange
            var model = new VoitureProfileModel();
            var controller = new CarController(_serviceMock.Object);
            controller.ModelState.AddModelError("Key", "Error message");

            // Act
            var result = await controller.Create(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestCarControllerCarCreated()
        {
            // Arrange
            var controller = new CarController(_serviceMock.Object);

            // Act
            var result = controller.CarCreated();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task TestCarControllerUpdateGet()
        {
            // Arrange
            var model = new VoitureProfileModel();
            _serviceMock.Setup(s => s.GetCarAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            var controller = new CarController(_serviceMock.Object);
            int id = 1;

            // Act
            var result = await controller.Update(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task TestCarControllerUpdatePost_ValidModel_Success()
        {
            // Arrange
            var model = new VoitureProfileModel();
            _serviceMock.Setup(s => s.UpdateCarAsync(model))
                .ReturnsAsync(model);
            var controller = new CarController(_serviceMock.Object);

            // Act
            var result = await controller.Update(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task TestCarControllerUpdatePost_InvalidModel_ReturnsView()
        {
            // Arrange
            var model = new VoitureProfileModel();
            var controller = new CarController(_serviceMock.Object);
            controller.ModelState.AddModelError("Key", "Error message");

            // Act
            var result = await controller.Update(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CarUpdated()
        {
            // Arrange
            var controller = new CarController(_serviceMock.Object);

            // Act
            var result = controller.CarUpdated();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task TestCarControllerDeletePost_InvalidId_ReturnsView()
        {
            // Arrange
            int id = 1;
            _serviceMock.Setup(s => s.DeleteCarAsync(id))
                .Throws(new Exception("Error deleting car"));
            var controller = new CarController(_serviceMock.Object);
            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task TestCarControllerDeletePost_ValidId()
        {
            // Arrange
            var model = new VoitureProfileModel();
            _serviceMock.Setup(s => s.GetCarAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            var controller = new CarController(_serviceMock.Object);
            int id = 1;
            // Act
            var result = await controller.Delete(id);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
