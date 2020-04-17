using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AwesomePotato.Models;

namespace AwesomePotato.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthorizationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("RegistrarRole")]
        public async Task<ActionResult> RegisterRole(RoleViewModel roleModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var role = new IdentityRole
            {
                Name = roleModel.Nome
            };

            var roleResult = await _roleManager.CreateAsync(role).ConfigureAwait(true);

            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            return Ok();
        }

        [HttpPost("AdicionarUsuarioRole")]
        public async Task<ActionResult> AddUserToRole(AddUserRoleModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = await _userManager.FindByEmailAsync(viewModel.Email).ConfigureAwait(true);

            if (user == null) return BadRequest("Usuário não cadastrado");

            bool roleExists = await _roleManager.RoleExistsAsync(viewModel.Role).ConfigureAwait(true);

            if (!roleExists) return BadRequest("Role não cadastrado");

            IdentityResult resultRole = await _userManager.AddToRoleAsync(user, viewModel.Role).ConfigureAwait(true);

            if (!resultRole.Succeeded)
            {
                return BadRequest(resultRole.Errors);
            }

            return Ok();
        }

    }
}
