using Estacionamento.Service.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Estacionamento.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public LoginController(UserManager<IdentityUser> userManager, 
                                  SignInManager<IdentityUser> signInManager, 
                                  IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> RegistrarUsuario([FromBody] UsuarioDto usuarioDto)
        {
            var user = new IdentityUser
            {
                UserName = usuarioDto.Email,
                Email = usuarioDto.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Usuário registrado com sucesso." });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsuarioDto usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            var user = await _userManager.FindByEmailAsync(usuarioDto.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Usuário não encontrado.");
                return BadRequest(ModelState);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, usuarioDto.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(GerarToken(usuarioDto.Email));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido!");
                return BadRequest(ModelState);
            }
        }

        private UsuarioToken GerarToken(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, email),
                new Claim("meuPC", "teclado"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (email == "admin@localhost")
            {
                claims.Add(new Claim("DeletePermission", "true"));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credenciais);

            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };
        }

    }
}
