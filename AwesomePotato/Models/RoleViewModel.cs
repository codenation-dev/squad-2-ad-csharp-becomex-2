using System.ComponentModel.DataAnnotations;

namespace AwesomePotato.Models
{
    public class RoleViewModel
    {
        [Required]
        public string Nome { get; set; }
    }
}
