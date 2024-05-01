namespace PetEsperanca.Models;

public class Evento{
    
    public int Id { get; set; }
    public required string ONGid { get; set; }
    
    public required string  Nome { get; set; }

    public required double DataInicio  { get; set; }

    public required string Objetivo { get; set; }

    public required decimal ValorDesejado {get; set;}

    public required decimal ValorAlcançado {get; set;}

    public required int NumeroDeDoacao {get; set;}
}