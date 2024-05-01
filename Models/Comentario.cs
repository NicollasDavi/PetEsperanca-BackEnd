namespace PetEsperanca.Models;

public class Comentario{
    public required Guid Id {get; set;}
    public required string OngId {get; set;}
    public required string UserId {get; set;}
    public required string Comment {get; set;}
    public required string Avaliacao {get; set;}
}