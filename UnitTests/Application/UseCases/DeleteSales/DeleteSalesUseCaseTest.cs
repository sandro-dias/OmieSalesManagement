using Application.Data;
using Application.Data.Specification;
using Application.UseCases.DeleteSales;
using AutoFixture;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests.Application.UseCases.DeleteSales
{
    public class DeleteSalesUseCaseTest
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<ILogger<DeleteSalesUseCase>> loggerMock;
        private readonly DeleteSalesUseCase salesService;

        public DeleteSalesUseCaseTest()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            loggerMock = new Mock<ILogger<DeleteSalesUseCase>>();
            salesService = new DeleteSalesUseCase(unitOfWorkMock.Object, loggerMock.Object);
        }

        [Fact]
        public async Task DeleteSalesAsync_ShouldLogError_WhenSalesDoesNotExist()
        {
            // Arrange
            var input = new DeleteSalesInput { SalesId = 1 };
            unitOfWorkMock.Setup(u => u.SalesRepository.GetByIdAsync(input.SalesId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync((Sales)null);

            // Act
            await salesService.DeleteSalesAsync(input, CancellationToken.None);

            // Assert
            unitOfWorkMock.Verify(u => u.SalesRepository.DeleteAsync(It.IsAny<Sales>(), It.IsAny<CancellationToken>()), Times.Never);
            unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task DeleteSalesAsync_ShouldDeleteSalesAndProducts_WhenSalesExists()
        {
            // Arrange
            var sales = _fixture.Create<Sales>();
            var input = _fixture.Build<DeleteSalesInput>().With(x => x.SalesId, sales.SalesId).Create();
            var products = _fixture.CreateMany<Product>(1);

            unitOfWorkMock.Setup(u => u.SalesRepository.GetByIdAsync(input.SalesId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(sales); // Simula que a venda existe
            unitOfWorkMock.Setup(u => u.ProductRepository.ListAsync(It.IsAny<GetProductsBySalesId>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(products);

            // Act
            await salesService.DeleteSalesAsync(input, CancellationToken.None);

            // Assert
            unitOfWorkMock.Verify(u => u.SalesRepository.DeleteAsync(sales, It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(u => u.ProductRepository.DeleteListAsync(products, It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
