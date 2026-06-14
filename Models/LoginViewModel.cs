using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string Senha { get; set; } = "";
    }
}