using System.ComponentModel.DataAnnotations;

namespace Letrasdemusicas.Models
{
    public class Musica
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da música é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Name { get;  set;} = string.Empty;

        [Required(ErrorMessage = "A letra da música é obrigatória")]
        public string Letra { get;  set;} = string.Empty;

    }    
}
