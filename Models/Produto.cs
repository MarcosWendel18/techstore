using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = "";

        public string Categoria { get; set; } = "";

        public string Descricao { get; set; } = "";

        public decimal Preco { get; set; }

        public int Estoque { get; set; }

        public string Imagem { get; set; } = "";
    }
}