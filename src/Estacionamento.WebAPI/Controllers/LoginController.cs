using Estacionamento.Service.Dtos.Account;
using Estacionamento.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public LoginController(UserManager<IdentityUser> userManager, 
                                  SignInManager<IdentityUser> signInManager,  
                                  ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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
                UsuarioToken token = _tokenService.GerarToken(usuarioDto.Email);
                return Ok(token);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido!");
                return BadRequest(ModelState);
            }
        }
    }
}
