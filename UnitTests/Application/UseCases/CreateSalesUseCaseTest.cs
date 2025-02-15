using Application.Data;
using Application.UseCases.CreateSales;
using Application.UseCases.CreateSales.Input;
using AutoFixture;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace UnitTests.Application.UseCases;
public class CreateSalesUseCaseTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IValidator<ProductInput>> productValidatorMock;
    private readonly Mock<IValidator<CreateSalesInput>> salesValidatorMock;
    private readonly Mock<ILogger<CreateSalesUseCase>> loggerMock;
    private readonly Mock<IUnitOfWork> unitOfWorkMock;
    private readonly CreateSalesUseCase createSalesUseCase;

    public CreateSalesUseCaseTests()
    {
        productValidatorMock = new Mock<IValidator<ProductInput>>();
        salesValidatorMock = new Mock<IValidator<CreateSalesInput>>();
        loggerMock = new Mock<ILogger<CreateSalesUseCase>>();
        unitOfWorkMock = new Mock<IUnitOfWork>();

        createSalesUseCase = new CreateSalesUseCase(
            salesValidatorMock.Object,
            productValidatorMock.Object,
            loggerMock.Object,
            unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateSalesAsync_ShouldReturnDefault_WhenProductValidationFails()
    {
        // Arrange
        var input = new CreateSalesInput
        {
            Products =
            [
                new ProductInput { Quantity = 1, UnitValue = 10 }
            ],
            Customer = "Customer A",
        };

        var fakeValidationFailure = new ValidationFailure("Name", "Name should not be null");
        var productValidationResult = new ValidationResult(new List<ValidationFailure>() { fakeValidationFailure });
        productValidatorMock.Setup(v => v.Validate(It.IsAny<ProductInput>())).Returns(productValidationResult);

        // Act
        var result = await createSalesUseCase.CreateSalesAsync(input, CancellationToken.None);

        // Assert
        result.SalesId.ShouldBeNull();
        result.Errors.ShouldNotBeNull();
        unitOfWorkMock.Verify(u => u.CommitAsync(CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task CreateSalesAsync_ShouldReturnSalesId_WhenSuccessful()
    {
        // Arrange
        var productInput = _fixture.CreateMany<ProductInput>(1).ToList();
        var createSalesInput = _fixture.Build<CreateSalesInput>().With(x => x.Products, productInput).Create();

        var productValidationResult = new ValidationResult();
        var salesValidationResult = new ValidationResult();

        productValidatorMock.Setup(v => v.Validate(It.IsAny<ProductInput>())).Returns(productValidationResult);
        salesValidatorMock.Setup(v => v.Validate(It.IsAny<CreateSalesInput>())).Returns(salesValidationResult);
        var sales = Sales.CreateSales(createSalesInput.Customer, createSalesInput.Value);
        unitOfWorkMock.Setup(u => u.SalesRepository.AddAsync(It.IsAny<Sales>(), It.IsAny<CancellationToken>())).ReturnsAsync(sales);
        unitOfWorkMock.Setup(u => u.ProductRepository.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()));

        // Act
        var result = await createSalesUseCase.CreateSalesAsync(createSalesInput, CancellationToken.None);

        // Assert
        result.SalesId.ShouldBe(sales.SalesId);
        result.Errors.ShouldBeNull();
        unitOfWorkMock.Verify(u => u.CommitAsync(CancellationToken.None), Times.Once);
    }
}