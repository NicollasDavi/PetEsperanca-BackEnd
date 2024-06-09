using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetEsperanca.Models;

public class Evento{
    
    [Key]
    public Guid Id { get; set; }
    public required string ONGid { get; set; }
    
    public required string  Nome { get; set; }

    public required double DataInicio  { get; set; }

    public required string Objetivo { get; set; }

    public required decimal ValorDesejado {get; set;}

    public required decimal ValorAlcancado {get; set;}

    public required int NumeroDeDoacao {get; set;}
}