using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Catalog.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome não deve exceder 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(500, ErrorMessage = "Descrição não deve exceder 500 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "ImageUrl é obrigatório")]
        [StringLength(300, ErrorMessage = "ImageUrl não pode ter mais de 300 caractere")]
        public string ImageUrl { get; set; }

        public float Storage { get; set; }

        public DateTime RegisterData { get; set; }

        // Propriedades para relacionamento
        public int CategoryId { get; set; }

        // Propriedade de navegação
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
