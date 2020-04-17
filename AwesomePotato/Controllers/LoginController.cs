using AwesomePotato.Models;
using AwesomePotato.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomePotato.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(IUserManagementService userManagementService,
                                SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManagementService = userManagementService;
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult> Registrar(UserRegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _userManagementService.Create(viewModel).ConfigureAwait(true);

            if (!result.Succeeded) return BadRequest(result.ToString());

            viewModel.Senha = string.Empty;
            viewModel.ConfirmaSenha = string.Empty;

            return Ok(viewModel);
        }

        [HttpPost("Entrar")]
        public async Task<ActionResult> Login(UserLoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Senha, false, true).ConfigureAwait(true);

            if (!result.Succeeded)
            {
                return BadRequest("Usuário ou senha inválido.");
            }

            return Ok(await _userManagementService.GerarJWT(viewModel.Email).ConfigureAwait(true));
        }
    }
}
