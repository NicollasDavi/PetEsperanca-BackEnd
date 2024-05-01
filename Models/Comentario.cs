using System;
using System.ComponentModel.DataAnnotations;

namespace PetEsperanca.Models
{
    public class Comentario
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Id da ONG é obrigatório.")]
        public Guid OngId { get; set; }

        [Required(ErrorMessage = "O Id do usuário é obrigatório.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "O comentário é obrigatório.")]
        public required string Comment { get; set; }

        [Required(ErrorMessage = "A avaliação é obrigatória.")]
        [Range(1, 5, ErrorMessage = "A avaliação deve estar entre 1 e 5.")]
        public int Avaliacao { get; set; }
    }
}
