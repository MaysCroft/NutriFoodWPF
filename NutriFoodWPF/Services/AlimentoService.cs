using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace NutriFoodWPF.Services
{
    public class AlimentoService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "";

        public AlimentoService()
        {
            _httpClient = new HttpClient();
        }
    }
}
