namespace PetEsperanca.Models;

public class Evento{
    
    public int Id { get; set; }
    public required string ONGid { get; set; }
    
    public required string  Nome { get; set; }

    public required double DataInicio  { get; set; }

    public required string Objetivo { get; set; }

    public required decimal ValorDesejado {get; set;}

    public required decimal ValorAlcancado {get; set;}

    public required int NumeroDeDoacao {get; set;}

    public void RegrasNegocio(){
        if (DataInicio < DateTime.UtcNow.ToOADate())
        {
            throw new InvalidOperationException ("Data de início deve ser uma data atual ou futura.");
        }
        if (ValorAlcancado >= ValorDesejado) {
            Console.WriteLine("Parabéns por atingir ou ultrapassar a meta!");
        }
        if (ValorDesejado < 0 || ValorAlcancado < 0 || NumeroDeDoacao < 0) {
            throw new InvalidOperationException("Valores não podem ser negativos.");
        }
        
    }
}