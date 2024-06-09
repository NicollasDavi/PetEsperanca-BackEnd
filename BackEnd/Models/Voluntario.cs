using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetEsperanca.Models
{
   public class Voluntario
    {
        [Key]
        public Guid VoluntarioId { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid OngId { get; set; }
        public Ong Ong { get; set; }

        public double HorasTrabalhadas { get; set; }
    }
}
