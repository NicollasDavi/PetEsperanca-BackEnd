using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetEsperanca.Models
{
    public class Comentario
    {
        [Key]
        public Guid Id { get; set; }
  
        [ForeignKey("OngId")]
        public Guid OngId { get; set; }

        public Guid UserId { get; set; }

        public required string Comment { get; set; } 

        public int Avaliacao { get; set; }
    }
}
