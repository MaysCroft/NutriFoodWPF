using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NutriFoodWPF.Models;
using System.Net.Http.Json;

namespace NutriFoodWPF.View_Models
{
    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<string> Alimentos { get; set; }
        public ICommand CarregarAlimentosCommand { get; }

        public MainViewModel()
        {
            Alimentos = new ObservableCollection<string>();
            CarregarAlimentosCommand = new RelayCommand(CarregarAlimentos);
        }

        private async void CarregarAlimentos()
        {
            var httpClient = new HttpClient();

            var dados = await httpClient.GetFromJsonAsync<List<Alimento>>(
                "https://localhost:7257/api/v1/alimentos");

            Alimentos.Clear();

            foreach (var alimento in dados)
            {
                Alimentos.Add(alimento.Nome);
                Alimentos.Add(alimento.Calorias.ToString());
                Alimentos.Add(alimento.PorcaoGramas.ToString());
                Alimentos.Add(alimento.GorduraTotal.ToString());
                Alimentos.Add(alimento.GorduraSaturada.ToString());
                Alimentos.Add(alimento.Proteina.ToString());
                Alimentos.Add(alimento.Sodio.ToString());
                Alimentos.Add(alimento.Potassio.ToString());
                Alimentos.Add(alimento.Colesterol.ToString());
                Alimentos.Add(alimento.Carboidratos.ToString());
                Alimentos.Add(alimento.Fibras.ToString());
                Alimentos.Add(alimento.Acucar.ToString());
            }
        }
    }
}
