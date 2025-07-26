//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using SuperMarketAPI.Models;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace SuperMarketAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly JwtSettings _jwtSettings;

//        public AuthController(IOptions<JwtSettings> jwtSettings)
//        {
//            _jwtSettings = jwtSettings.Value;
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginModel model)
//        {
//            // Fake login: Replace this with real user checking
//            if (model.Username == "admin" && model.Password == "1234")
//            {
//                var token = GenerateToken(model.Username);
//                return Ok(new { Token = token });
//            }

//            return Unauthorized("Invalid credentials");
//        }

//        private string GenerateToken(string username)
//        {
//            var claims = new[]
//            {
//            new Claim(JwtRegisteredClaimNames.Sub, username),
//            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//        };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: _jwtSettings.Issuer,
//                audience: _jwtSettings.Audience,
//                claims: claims,
//                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
//                signingCredentials: creds);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
