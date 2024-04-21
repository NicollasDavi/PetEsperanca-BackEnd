namespace PetEsperanca.Models;

public class User {

    public int Id { get; set; }
    public required string Nome{ get; set; }
    public string? Cnpj  { get; set; }
    
    public required string Cpf  { get; set; }

    public required string Tel  { get; set; }

    public required string Email { get; set; }
}