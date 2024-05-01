using System;
using System.ComponentModel.DataAnnotations;

namespace PetEsperanca.Models
{
    public class Ong : User
    {
        [Key]
        public Guid OngId { get; set; }

        [Required(ErrorMessage = "O nome da ONG é obrigatório.")]
        public required string OngName { get; set; }

        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "Formato de CNPJ inválido.")]
        public required string Cnpj { get; set; }

        public Comentario[] Comentarios { get; set; } = new Comentario[0];

        public Voluntario[] Voluntarios { get; set; } = new Voluntario[0];

        public Evento[] Eventos { get; set; } = new Evento[0];

        public Endereco[] Enderecos { get; set; } = new Endereco[0];
    }
}
