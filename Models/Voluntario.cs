using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetEsperanca.Models;

public class Voluntario{

    [Key]
    public required Guid voluntarioId{get;set;}

    public required string userId {get;set;}

    public required string OngId{get;set;}

    public Double HorasTrabalhadas{get;set;}
    }

