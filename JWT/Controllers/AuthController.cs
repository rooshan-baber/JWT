using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration configuration;
        private string issuer;
        private string audience;
        private byte[] key;
        private SigningCredentials signingCredentials;
        private JwtSecurityTokenHandler tokenHandler;
        private SecurityToken token;
        private string jwtToken;
        private ClaimsIdentity subject;
        private DateTime expires;

        public SecurityTokenDescriptor TokenDescriptor { get; private set; }

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Auth([FromBody] User user)
        {
            IActionResult response = Unauthorized();
            if (user != null)
            {
                if (user.username.Equals("rooshanbaber07@gmail.com") && user.password.Equals("rooshan")) 
                {
                    issuer = configuration["Jwt:Issuer"];
                    audience = configuration["Jwt:Audience"];
                    key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                    signingCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature
                        );
                }
                
                subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.username),
                    new Claim(JwtRegisteredClaimNames.Email,user.username),
                });
                expires = DateTime.UtcNow.AddDays(1);
                TokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = expires,
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials
                };
                tokenHandler = new JwtSecurityTokenHandler();
                token = tokenHandler.CreateToken(TokenDescriptor);
                jwtToken = tokenHandler.WriteToken(token);

                return Ok(jwtToken);
            }
            return response;
        }
    }
}
