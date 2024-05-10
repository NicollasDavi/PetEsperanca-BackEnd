using System.Threading.Tasks;
using PetEsperanca.Services;

namespace PetEsperanca.Models
{
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
        
        
        public Endereco() {}

        
        public async Task PreencherEnderecoPorCEP(ViaCEPService viaCEPService)
        {
            if (!string.IsNullOrEmpty(CEP))
            {
                Endereco enderecoObtido = await viaCEPService.GetEnderecoByCEP(CEP);
                
                if (enderecoObtido != null)
                {
                    Rua = enderecoObtido.Rua;
                    Bairro = enderecoObtido.Bairro;
                    Cidade = enderecoObtido.Cidade;
                    Estado = enderecoObtido.Estado;
                    Pais = enderecoObtido.Pais;
                    Complemento = enderecoObtido.Complemento;
                }
            }
        }
    }
}
 