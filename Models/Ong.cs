using System.ComponentModel.DataAnnotations;

namespace PetEsperanca.Models
{
    public class Ong 
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da ONG é obrigatório.")]
        public required string OngName { get; set; }

        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "Formato de CNPJ inválido.")]
        public required string Cnpj { get; set; }

        public ICollection<Comentario> Comentario { get; set;} = new List<Comentario>();
        public ICollection<Voluntario> Voluntarios {get; set;} = new List<Voluntario>();
        public List<Evento> Eventos {get; set;} = new List<Evento>();
    }
}
