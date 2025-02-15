using Application.UseCases.CreateSales;
using Application.UseCases.CreateSales.Input;
using Application.UseCases.DeleteSales;
using Application.UseCases.GetSales;
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
        private readonly IGetSalesUseCase _getSalesUseCase;
        private readonly IDeleteSalesUseCase _deleteSalesUseCase;

        public SalesController(ILogger<SalesController> logger, ICreateSalesUseCase createSalesUseCase, IGetSalesUseCase getSalesUseCase, IDeleteSalesUseCase deleteSalesUseCase)
        {
            _logger = logger;
            _createSalesUseCase = createSalesUseCase;
            _getSalesUseCase = getSalesUseCase;
            _deleteSalesUseCase = deleteSalesUseCase;
        }

        [HttpPost]
        //[Authorize]
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

        [HttpGet]
        //[Authorize]
        [SwaggerOperation(
            Summary = "Busca as vendas no banco de dados",
            Description = "Esse endpoint busca as vendas mais recentes cadastradas no banco de dados para a página inicial da landing page. Para usá-lo é preciso se autenticar.")]
        [Route("api/get-sales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSales(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _getSalesUseCase.GetSalesAsync(cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to get the sales. The message returned was: {@Message}", nameof(SalesController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar vendas no banco de dados.");
            }
        }

        [HttpDelete]
        //[Authorize]
        [SwaggerOperation(
            Summary = "Delete uma venda do banco de dados",
            Description = "Esse endpoint deleta uma venda do banco de dados. Para usá-lo é preciso se autenticar.")]
        [Route("api/delete-sales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSales([FromQuery] DeleteSalesInput input, CancellationToken cancellationToken)
        {
            try
            {
                await _deleteSalesUseCase.DeleteSalesAsync(input, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to delete the sales. The message returned was: {@Message}", nameof(SalesController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao remover uma venda do banco de dados.");
            }
        }
    }
}
