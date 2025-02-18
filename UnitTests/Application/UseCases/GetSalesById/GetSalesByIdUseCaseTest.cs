using Application.Data;
using Application.Data.Repository;
using Application.Data.Specification;
using Application.UseCases.GetSalesById;
using AutoFixture;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace UnitTests.Application.UseCases.GetSalesById
{
    public class GetSalesByIdUseCaseTest
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ISalesRepository> _mockSalesRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ILogger<GetSalesByIdUseCase>> _mockLogger;
        private readonly GetSalesByIdUseCase _useCase;

        public GetSalesByIdUseCaseTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockSalesRepository = new Mock<ISalesRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<GetSalesByIdUseCase>>();

            _mockUnitOfWork.Setup(u => u.SalesRepository).Returns(_mockSalesRepository.Object);
            _mockUnitOfWork.Setup(u => u.ProductRepository).Returns(_mockProductRepository.Object);

            _useCase = new GetSalesByIdUseCase(_mockLogger.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task ShouldReturnOutputWhenSalesExists()
        {
            // Arrange
            var input = _fixture.Create<GetSalesByIdInput>();
            var sales = _fixture.Create<Sales>();
            var products = _fixture.CreateMany<Product>(1);

            _mockSalesRepository.Setup(r => r.GetByIdAsync(input.SalesId, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(sales);

            _mockProductRepository.Setup(r => r.ListAsync(It.IsAny<GetProductsBySalesId>(), It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(products);

            // Act
            var result = await _useCase.GetSalesByIdAsync(input, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sales, result.Sales);
            Assert.Equal(products, result.Products);

            result.ShouldNotBeNull();
            result.Sales.ShouldBeEquivalentTo(sales);
            result.Products.ShouldBeEquivalentTo(products);
        }

        [Fact]
        public async Task ShouldReturnEmptyOutputWhenSalesNotExists()
        {
            // Arrange
            var input = _fixture.Create<GetSalesByIdInput>();
            _mockSalesRepository.Setup(r => r.GetByIdAsync(input.SalesId, It.IsAny<CancellationToken>()))
                                .ReturnsAsync((Sales)null);

            // Act
            var result = await _useCase.GetSalesByIdAsync(input, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Sales.ShouldBeNull();
            result.Products.ShouldBeNull();
        }
    }
}
