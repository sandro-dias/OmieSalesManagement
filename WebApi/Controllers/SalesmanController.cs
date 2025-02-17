using Application.Middleware;
using Application.UseCases.CreateSalesman;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("v1")]
    [SwaggerTag("Endpoints relacionados a gestão de acesso dos vendedores")]
    [ExcludeFromCodeCoverage]
    public class SalesmanController(ILogger<SalesmanController> logger, ICreateSalesmanUseCase createSalesmanUseCase) : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastra um vendedor no banco de dados",
            Description = "Esse endpoint recebe os dados de um vendedor para futura autenticação.")]
        [Route("api/create-salesman")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSalesman([Required][FromBody] CreateSalesmanInput input, CancellationToken cancellationToken)
        {
            try
            {
                await createSalesmanUseCase.CreateSalesmanAsync(input, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError("[{ClassName}] It was not possible to post the salesman. The message returned was: {@Message}", nameof(SalesmanController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir vendedor no banco de dados.");
            }
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Busca um vendedor no banco de dados",
            Description = "Esse endpoint busca um vendedor no banco de dados para autenticar e usar as demais funcionalidades.")]
        [Route("api/authenticate-salesman")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticateSalesman([FromServices] SalesmanAuthentication middleware, [FromHeader][Required] string salesman, [FromHeader][Required] string password, CancellationToken cancellationToken)
        {
            try
            {
                var token = await middleware.AuthenticateSalesman(salesman, password);
                if (string.IsNullOrEmpty(token))
                    return StatusCode(StatusCodes.Status404NotFound, "A senha inserida para este vendedor está incorreta");

                return Ok(token);
            }
            catch (Exception ex)
            {
                logger.LogError("[{ClassName}] It was not possible to get the salesman. The message returned was: {@Message}", nameof(SalesmanController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar vendedor no banco de dados para autenticação.");
            }
        }
    }
}
