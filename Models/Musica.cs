using System.ComponentModel.DataAnnotations;

namespace Letrasdemusicas.Models
{
    public class Musica
    {
        public int Id { get; set; }
        
        public required string Name { get; set; }
     
        public required string Letra { get; set; }
    }
}
