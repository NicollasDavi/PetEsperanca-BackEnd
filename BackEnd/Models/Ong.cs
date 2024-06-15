using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetEsperanca.Models
{
    public class Ong 
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da ONG é obrigatório.")]
        public string OngName { get; set; }

        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "Formato de CNPJ inválido.")]
        public string Cnpj { get; set; }

        public string Sobre {get; set;}

        public string Image {get; set;}
    }
}