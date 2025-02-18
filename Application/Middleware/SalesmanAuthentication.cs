using Application.Data.Repository;
using Application.Data.Specification;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Middleware
{
    [ExcludeFromCodeCoverage]
    public class SalesmanAuthentication(ISalesmanRepository salesmanRepository)
    {
        private readonly ISalesmanRepository _salesmanRepository = salesmanRepository;

        public async Task<string> AuthenticateSalesman(string name, string password)
        {
            var salesman = await _salesmanRepository.FirstOrDefaultAsync(new GetSalesmanByPassword(name, password));
            if (salesman == null)
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, salesman.Name),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
