using Application.UseCases.CreateSales;
using Application.UseCases.CreateSales.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("v1")]
    [SwaggerTag("Endpoints relacionados a gestão de vendas")]
    [ExcludeFromCodeCoverage]
    public class SalesController : ControllerBase
    {
        private readonly ILogger<SalesController> _logger;
        private readonly ICreateSalesUseCase _createSalesUseCase;

        public SalesController(ILogger<SalesController> logger, ICreateSalesUseCase createSalesUseCase)
        {
            _logger = logger;
            _createSalesUseCase = createSalesUseCase;
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(
            Summary = "Cadastra uma venda no banco de dados",
            Description = "Esse endpoint recebe uma lista de produtos para registrar a venda. Para usá-lo é preciso se autenticar.")]
        [Route("api/create-sales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSales([Required][FromBody] CreateSalesInput input, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _createSalesUseCase.CreateSalesAsync(input, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to post the sales. The message returned was: {@Message}", nameof(SalesController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir venda no banco de dados.");
            }
        }
    }
}
