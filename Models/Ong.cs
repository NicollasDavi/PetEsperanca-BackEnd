using PetEsperanca.Models;

namespace PetEsperanca.Models;

public class Ong : User{
    public required string OngId {get; set;}

    public required string OngName {get; set;}
    public string? Cnpj  { get; set; }

    public Comentario[]? Comentario {get ; set;}

    public Voluntario[]? Voluntarios {get; set;}

    public Evento[]? Eventos {get; set;}

    public Endereco[]? Enderecos {get; set;}
}