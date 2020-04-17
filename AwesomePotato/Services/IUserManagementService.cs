using AwesomePotato.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AwesomePotato.Services
{
    public interface IUserManagementService
    {
        Task<IdentityResult> Create(UserRegisterViewModel registroUsuario);
        Task<List<Claim>> GetAllUserClaims(IdentityUser user);
        Task<string> GerarJWT(string email);
    }
}
