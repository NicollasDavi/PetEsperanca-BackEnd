using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PetEsperanca.Models;

namespace PetEsperanca.Services
{
    public class ViaCEPService
    {
        private readonly HttpClient _httpClient;

        public ViaCEPService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Endereco> GetEnderecoByCEP(string cep)
        {
            string url = $"https://viacep.com.br/ws/{cep}/json/";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            Endereco endereco = JsonConvert.DeserializeObject<Endereco>(responseBody);
            return endereco;
        }
    }
}
