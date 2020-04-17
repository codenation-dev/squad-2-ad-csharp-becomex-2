using AwesomePotato.DTOs;
using AwesomePotato.Infraestructure;
using AwesomePotato.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
                    await _signInManager.SignInAsync(user, false).ConfigureAwait(true);

            }

            return result;
        }
        public async Task<List<Claim>> GetAllUserClaims(IdentityUser user)
        {
            var claims = new List<Claim>();

            var resultRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(true);
            var roles = _roleManager.Roles.Where(x => resultRoles.Contains(x.Name)).ToList();

            foreach (var role in roles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role).ConfigureAwait(true);
                claims.AddRange(roleClaims);
            }

            claims.AddRange(await _userManager.GetClaimsAsync(user).ConfigureAwait(true));

            return claims;
        }


        public async Task<string> GerarJWT(string email)
        {
            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(true);

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(await GetAllUserClaims(user).ConfigureAwait(true));

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(true);
            foreach (var role in roles)
            {
                identityClaims.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
                Subject = identityClaims
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
