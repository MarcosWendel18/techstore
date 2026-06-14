using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto.")]
        [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = "";
        [Required(ErrorMessage = "Informe a categoria do produto.")]
        [StringLength(100, ErrorMessage = "A categoria do produto deve ter no máximo 100 caracteres.")]
        public string Categoria { get; set; } = "";
        [Required(ErrorMessage = "Informe a descrição do produto.")]
        [StringLength(500, ErrorMessage = "A descrição do produto deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; } = "";
        [Required(ErrorMessage = "Informe o preço do produto.")]
        [Range(0.01, 999999, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Informe o estoque do produto.")]
        [Range(0, 9999, ErrorMessage = "O estoque deve ser um número inteiro positivo.")]
        public int Estoque { get; set; }

        public string Imagem { get; set; } = "";
    }
}