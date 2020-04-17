using System.ComponentModel.DataAnnotations;

namespace AwesomePotato.Models
{
    public class AddUserRoleModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
