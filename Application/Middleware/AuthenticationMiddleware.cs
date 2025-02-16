//using Microsoft.IdentityModel.Tokens;
//using System.Diagnostics.CodeAnalysis;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Application.Middleware
//{
//    [ExcludeFromCodeCoverage]
//    public class AuthenticationMiddleware(ISalesmanRepository salesmanRepository)
//    {
//        private readonly ISalesmanRepository _salesmanRepository = salesmanRepository;

//        public async Task<string> AuthenticateSalesman(string name, string password)
//        {
//            var workshop = await _salesmanRepository.FirstOrDefaultAsync(new GetSalesmanByPassword(name, password));
//            if (workshop == null)
//                return string.Empty;

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(Settings.Secret);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//                    new(ClaimTypes.Name, workshop.Name),
//                }),
//                Expires = DateTime.UtcNow.AddHours(1),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }
//    }
//}
