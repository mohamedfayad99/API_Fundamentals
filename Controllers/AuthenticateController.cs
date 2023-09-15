using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }
        private class CityInfoUser
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }

            public CityInfoUser(int userId, string userName, string firstName, string lastName, string city)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                City = city;
            }
        }

        public AuthenticateController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("authenticate")]
        public  IActionResult AuthenticateMethod(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = Validateusercredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var securekey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authenticate:SecretForKey"]));
            var signingcredentials = new SigningCredentials(
               securekey, SecurityAlgorithms.HmacSha256);

            var claimsfortoken = new List<Claim>
            {
                new ("sub", user.UserId.ToString()),
                new ("givenname", user.FirstName),
                new ("familyname", user.LastName),
                new ("city", user.City)
            };

            var jwtsecuritytoken = new JwtSecurityToken(
               _configuration["Authenticate:Issuer"],
               _configuration["Authenticate:Audience"],
               claimsfortoken,
               DateTime.UtcNow,
               DateTime.UtcNow.AddHours(1),
               signingcredentials);

            var tokentoreturn =  new JwtSecurityTokenHandler().WriteToken(jwtsecuritytoken);
            return Ok(tokentoreturn);
        }


        private CityInfoUser Validateusercredentials(string? name,string? password)
        {
            return new CityInfoUser(
                1,
                name ?? "",
                "mohamed",
                "fayad",
                "komhamada"
                );
        }

    }
}
