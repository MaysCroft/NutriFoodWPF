using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NutriFoodWPF.Models;

namespace NutriFoodWPF.Services
{
    public class AlimentoService
    {
        private const string BaseUrl = "https://SUAAPI.runasp.net/api/alimentos";

        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public async Task<Alimento?> BuscarAlimento(Alimento alimento)
        {
            try
            {
                var resposta = await _httpClient.PostAsJsonAsync(BaseUrl, alimento);

                if (resposta.IsSuccessStatusCode)
                {
                    return await resposta.Content.ReadFromJsonAsync<Alimento>(_jsonOptions);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw new Exception($"Erro na comunicação com a API");
            }
        }
    }
}
