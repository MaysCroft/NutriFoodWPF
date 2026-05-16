using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NutriFoodWPF.Models;
using Google.Cloud.Firestore;

namespace NutriFoodWPF.View_Models
{
    internal class MainViewModel : BaseViewModel
    {
        private FirestoreDb _nutrifoodbanco { get; set; }
        public ObservableCollection<Alimento> Alimentos { get; set; }
        public ICommand PesquisarCommand { get; }
        public ICommand CarregarAlimentosCommand { get; }

        public MainViewModel()
        {
            Alimentos = new ObservableCollection<Alimento>();
            PesquisarCommand = new RelayCommand(async () => await ExecutarPesquisa());
            CarregarAlimentosCommand = new RelayCommand(async () => await CarregarAlimentos());

            IniciarFirestore();
        }

        private string _textoPesquisa;
        public string TextoPesquisa
        {
            get => _textoPesquisa;
            set
            {
                _textoPesquisa = value;
                OnPropertyChanged(nameof(TextoPesquisa));
            }
        }

        private void IniciarFirestore()
        {
            // O caminho para o arquivo de credenciais deve ser ajustado conforme a localização do seu arquivo JSON
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config_API", "firebase-key.json");
            // Define a variável de ambiente para as credenciais do Google Cloud
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            // Inicializa a instância do FirestoreDb
            _nutrifoodbanco = FirestoreDb.Create("nutrifoodwpf");
        }

        private async Task ExecutarPesquisa()
        {
            if (string.IsNullOrWhiteSpace(TextoPesquisa)) 
                return;

            await CarregarAlimentos();
        }

        private async Task CarregarAlimentos()
        {
            try
            {
                CollectionReference alimentosRef = _nutrifoodbanco.Collection("alimentos");
                QuerySnapshot snapshot = await alimentosRef.GetSnapshotAsync();
                Alimentos.Clear();

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    if (doc.Exists)
                    {
                        Alimento alimento = doc.ConvertTo<Alimento>();
                        Alimentos.Add(alimento);
                    }
                }
            }
            catch
            {
                throw new Exception("Erro ao carregar dados");
            }
        }
    }
}
