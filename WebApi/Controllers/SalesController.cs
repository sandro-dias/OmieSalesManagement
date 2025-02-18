using Application.UseCases.CreateSales;
using Application.UseCases.DeleteSales;
using Application.UseCases.GetSales;
using Application.UseCases.GetSalesById;
using Application.UseCases.UpdateSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("v1")]
    [SwaggerTag("Endpoints relacionados a gestão de vendas. Para usar  esses endpoints é preciso se autenticar.")]
    [ExcludeFromCodeCoverage]
    public class SalesController(ILogger<SalesController> logger) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        [SwaggerOperation(
            Summary = "Cadastra uma venda no banco de dados",
            Description = "Esse endpoint recebe uma lista de produtos para registrar a venda. Para usá-lo é preciso se autenticar.")]
        [Route("api/create-sales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSales([FromServices] ICreateSalesUseCase createSalesUseCase, [Required][FromBody] CreateSalesInput input, CancellationToken cancellationToken)
        {
            try
            {
                var result = await createSalesUseCase.CreateSalesAsync(input, cancellationToken);
                return result.Errors is null ? Ok(result) : BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                logger.LogError("[{ClassName}] It was not possible to post the sales. The message returned was: {@Message}", nameof(SalesController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir venda no banco de dados.");
            }
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(
            Summary = "Busca as vendas no banco de dados",
            Description = "Esse endpoint busca as vendas mais recentes cadastradas no banco de dados para a página inicial da landing page. Para usá-lo é preciso se autenticar.")]
        [Route("api/get-sales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSales([FromServices] IGetSalesUseCase getSalesUseCase, CancellationToken cancellationToken)
        {
            try
            {
                var result = await getSalesUseCase.GetSalesAsync(cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError("[{ClassName}] It was not possible to get the sales. The message returned was: {@Message}", nameof(SalesController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar vendas no banco de dados.");
            }
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(
            Summary = "Busca uma venda no banco de dados pelo seu Id",
            Description = "Esse endpoint busca uma venda no banco de dados pelo seu Id para possibilitar sua atualização. Para usá-lo é preciso se autenticar.")]
        [Route("api/get-sales-by-id/{salesId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalesById([FromServices] IGetSalesByIdUseCase getSalesByIdUseCase, [FromRoute] long salesId,CancellationToken cancellationToken)
        {
            try
            {
                var result = await getSalesByIdUseCase.GetSalesByIdAsync(new GetSalesByIdInput(salesId), cancellationToken);
                return result.Sales is not null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError("[{ClassName}] It was not possible to get the sale. The message returned was: {@Message}", nameof(SalesController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar a venda no banco de dados.");
            }
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation(
            Summary = "Atualiza uma venda do banco de dados",
            Description = "Esse endpoint atualiza uma venda do banco de dados. Para usá-lo é preciso se autenticar.")]
        [Route("api/update-sales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSales([FromServices] IUpdateSalesUseCase updateSalesUseCase, [FromBody] UpdateSalesInput input, CancellationToken cancellationToken)
        {
            try
            {
                var result = await updateSalesUseCase.UpdateSalesAsync(input, cancellationToken);
                return result > 0 ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError("[{ClassName}] It was not possible to update the sales. The message returned was: {@Message}", nameof(SalesController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar uma venda do banco de dados.");
            }
        }

        [HttpDelete]
        [Authorize]
        [SwaggerOperation(
            Summary = "Delete uma venda do banco de dados",
            Description = "Esse endpoint deleta uma venda do banco de dados. Para usá-lo é preciso se autenticar.")]
        [Route("api/delete-sales")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSales([FromServices] IDeleteSalesUseCase deleteSalesUseCase, [FromQuery] DeleteSalesInput input, CancellationToken cancellationToken)
        {
            try
            {
                var result = await deleteSalesUseCase.DeleteSalesAsync(input, cancellationToken);
                return result > 0 ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError("[{ClassName}] It was not possible to delete the sales. The message returned was: {@Message}", nameof(SalesController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao remover uma venda do banco de dados.");
            }
        }
    }
}
