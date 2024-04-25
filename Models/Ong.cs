namespace PetEsperanca.Models;

public class Ong : User{
    public required string OngName {get; set;}

    public string? Cnpj  { get; set; }
}