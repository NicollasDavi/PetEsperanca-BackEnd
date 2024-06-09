using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetEsperanca.Models
{
    public class Endereco 
    {
        public Guid Id { get; set; }
        [ForeignKey("OngId")]
        public Guid OngId { get; set; }
        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Localidade { get; set; }

        public string UF { get; set; }

    }
}
