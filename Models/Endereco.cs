namespace PetEsperanca.Models;

public class Endereco
{
    public string Rua { get; set; }
    public int Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string CEP { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Pais { get; set; }
    public int Id { get; set; }
    
    // Construtor
    public Endereco(string rua, int numero, string complemento, string bairro, string cep, string cidade, string estado, string pais, int id)
    {
        Rua = rua;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        CEP = cep;
        Cidade = cidade;
        Estado = estado;
        Pais = pais;
        Id = id;
    }
    
    // Construtor vazio
    public Endereco()
    {
        
    }
}