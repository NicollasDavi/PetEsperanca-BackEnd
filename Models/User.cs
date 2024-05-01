using System;
using System.ComponentModel.DataAnnotations;

namespace PetEsperanca.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public required string Cpf { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public required string Tel { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public required string Email { get; set; }
    }
}
