using System.ComponentModel.DataAnnotations;

namespace PetEsperanca.Models;

public class User {
    
    public required Guid Id { get; set; }
    public required string Name{ get; set; }
    
    public required string Cpf  { get; set; }

    public required string Tel  { get; set; }

    public required string Email { get; set; }
}