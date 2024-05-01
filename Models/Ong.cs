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

        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public List<Voluntario> Voluntarios {get; set;} = new List<Voluntario>();
        public List<Evento> Eventos {get; set;} = new List<Evento>();
        public List<Endereco> Enderecos {get; set;} = new List<Endereco>();
    }
}
