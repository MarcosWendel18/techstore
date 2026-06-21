using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Informe o nome do usuário.")]
        public string Nome { get; set; } = "";

        [Required (ErrorMessage = "Informe o email do usuário.")]
        public string Email { get; set; } = "";

        [Required (ErrorMessage = "Informe a senha do usuário.")]
        public string Senha { get; set; } = "";

        public bool Administrador { get; set; }
    }
}