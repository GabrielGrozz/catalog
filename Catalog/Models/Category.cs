using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Models
{
    [Table("categories")]
    public class Category
    {
        //inicialização da coleção de produtos
        public Category() 
        {
             Products = new Collection<Product>();
        }

        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome não deve exceder 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ImageUrl é obrigatório")]
        [StringLength(300, ErrorMessage = "ImageUrl não pode ter mais de 300 caractere")]
        public string ImageUrl { get; set; }

        //aqui usamos um ICollection por ele ser mais flexivel que um list
        public ICollection<Product>? Products { get; set;}
    }
}
