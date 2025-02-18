using Application.Data;
using Application.Data.Repository;
using Application.Data.Specification;
using Application.UseCases.UpdateSales;
using AutoFixture;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests.Application.UseCases.UpdateSales
{
    public class UpdateSalesUseCaseTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ISalesRepository> _mockSalesRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ILogger<UpdateSalesUseCase>> _mockLogger;
        private readonly UpdateSalesUseCase _useCase;

        public UpdateSalesUseCaseTests()
        {
            _fixture = new Fixture();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockSalesRepository = new Mock<ISalesRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<UpdateSalesUseCase>>();

            // Configurar o UnitOfWork para retornar os repositórios mockados
            _mockUnitOfWork.Setup(u => u.SalesRepository).Returns(_mockSalesRepository.Object);
            _mockUnitOfWork.Setup(u => u.ProductRepository).Returns(_mockProductRepository.Object);

            _useCase = new UpdateSalesUseCase(_mockUnitOfWork.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task ShouldNotUpdateWhenSalesDoesNotExist()
        {
            // Arrange
            var input = _fixture.Create<UpdateSalesInput>();

            _mockSalesRepository.Setup(r => r.GetByIdAsync(input.SalesId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Sales)null);

            // Act
            await _useCase.UpdateSalesAsync(input, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ShouldUpdateSalesAndProductsCorrectly()
        {
            // Arrange
            var sales = _fixture.Create<Sales>();
            var input = _fixture.Create<UpdateSalesInput>();
            var existingProducts = _fixture.CreateMany<Product>().ToList();

            _mockSalesRepository.Setup(r => r.GetByIdAsync(input.SalesId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(sales);

            _mockProductRepository.Setup(r => r.ListAsync(It.IsAny<GetProductsBySalesId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingProducts);

            // Act
            await _useCase.UpdateSalesAsync(input, CancellationToken.None);

            // Assert
            _mockSalesRepository.Verify(r => r.UpdateAsync(sales, It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}