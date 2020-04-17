using AwesomePotato.DTOs;
using AwesomePotato.Infraestructure;
using AwesomePotato.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomePotato.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettingsDTO _appSettings;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IdentityContext _identityContext;

        public UserManagementService(SignInManager<IdentityUser> signInManager,
                                   UserManager<IdentityUser> userManager,
                                   IOptions<AppSettingsDTO> appSettings,
                                   RoleManager<IdentityRole> roleManager,
                                   IdentityContext identityContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
            _identityContext = identityContext;
        }

        public async Task<IdentityResult> Create(UserRegisterViewModel registroUsuario)
        {
            IdentityResult result = new IdentityResult();

            if (_identityContext.Users.FirstOrDefault(u => u.Email == registroUsuario.Email) == null)
            {
                var user = new IdentityUser
                {
                    UserName = registroUsuario.Email,
                    Email = registroUsuario.Email,
                    EmailConfirmed = true
                };

                result = await _userManager.CreateAsync(user, registroUsuario.Senha).ConfigureAwait(true);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false).ConfigureAwait(true);
                    await _userManager.AddToRoleAsync(user, "Usuario").ConfigureAwait(true);
                }
            }
            else
                result.Errors.Append(new IdentityError { Description = $"Email: {registroUsuario.Email} já cadastrado." });

            return result;
        }


        public string GerarJWT(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
