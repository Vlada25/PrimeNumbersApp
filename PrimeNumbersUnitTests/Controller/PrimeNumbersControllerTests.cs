using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PrimeNumbersApp.Controllers;
using PrimeNumbersApp.DAL.Interfaces.Repositories;
using PrimeNumbersApp.DAL.Models;

namespace PrimeNumbersUnitTests.Controller
{
    public class PrimeNumbersControllerTests
    {
        private readonly Mock<IPrimeNumberRepository> _primeNumberRepositoryMock;
        private readonly PrimeNumbersController _primeNumbersController;

        public PrimeNumbersControllerTests()
        {
            _primeNumberRepositoryMock = new Mock<IPrimeNumberRepository>();
            _primeNumbersController = new PrimeNumbersController(_primeNumberRepositoryMock.Object);
        }


        [Fact]
        public async Task IsThisAPrimeNumber_PrimeNumberIsExistsInDb_ReturnStatusCode200()
        {
            // Arrange
            int number = 3;
            string message = $"Number {number} is prime";
            PrimeNumber primeNumber = new PrimeNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
            };

            _primeNumberRepositoryMock.Setup(x => x.GetByNumber(number))
                .ReturnsAsync(primeNumber);

            // Act
            var response = await _primeNumbersController.IsThisAPrimeNumber(number);

            // Assert
            var result = response as OkObjectResult;
            var resultMessage = result.Value as string;

            response.Should().BeOfType<OkObjectResult>();
            result.Value.Should().NotBeNull();
            resultMessage.Should().BeEquivalentTo(message);
        }


        [Fact]
        public async Task IsThisAPrimeNumber_PrimeNumberIsNotExistsInDb_ReturnStatusCode200()
        {
            // Arrange
            int number = 3;
            string message = $"Number {number} is prime";
            PrimeNumber primeNumber = new PrimeNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
            };

            _primeNumberRepositoryMock.Setup(x => x.GetByNumber(number))
                .ReturnsAsync((PrimeNumber)null);
            _primeNumberRepositoryMock.Setup(x => x.Create(primeNumber));

            // Act
            var response = await _primeNumbersController.IsThisAPrimeNumber(number);

            // Assert
            var result = response as OkObjectResult;
            var resultMessage = result.Value as string;

            response.Should().BeOfType<OkObjectResult>();
            result.Value.Should().NotBeNull();
            resultMessage.Should().BeEquivalentTo(message);
        }

        [Fact]
        public async Task IsThisAPrimeNumber_NumberIsNotPrime_ReturnStatusCode400()
        {
            // Arrange
            int number = 4;
            string message = $"Number {number} is not prime";
            PrimeNumber primeNumber = new PrimeNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
            };

            _primeNumberRepositoryMock.Setup(x => x.GetByNumber(number))
                .ReturnsAsync((PrimeNumber)null);

            // Act
            var response = await _primeNumbersController.IsThisAPrimeNumber(number);

            // Assert
            var result = response as BadRequestObjectResult;
            var resultMessage = result.Value as string;

            response.Should().BeOfType<BadRequestObjectResult>();
            result.Value.Should().NotBeNull();
            resultMessage.Should().BeEquivalentTo(message);
        }

        [Fact]
        public async Task IsThisAPrimeNumber_NumberIsZero_ReturnStatusCode400()
        {
            // Arrange
            int number = 0;
            string message = $"Number {number} is not prime";

            // Act
            var response = await _primeNumbersController.IsThisAPrimeNumber(number);

            // Assert
            var result = response as BadRequestObjectResult;
            var resultMessage = result.Value as string;

            response.Should().BeOfType<BadRequestObjectResult>();
            result.Value.Should().NotBeNull();
            resultMessage.Should().BeEquivalentTo(message);
        }

        [Fact]
        public async Task IsThisAPrimeNumber_NumberIsOne_ReturnStatusCode200()
        {
            // Arrange
            int number = 1;
            string message = $"Number {number} is neither prime nor composite";

            // Act
            var response = await _primeNumbersController.IsThisAPrimeNumber(number);

            // Assert
            var result = response as OkObjectResult;
            var resultMessage = result.Value as string;

            response.Should().BeOfType<OkObjectResult>();
            result.Value.Should().NotBeNull();
            resultMessage.Should().BeEquivalentTo(message);
        }
    }
}