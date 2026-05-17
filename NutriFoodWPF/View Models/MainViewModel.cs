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
                // Obtém a referência para a coleção "nutrifoodwpf" no Firestore
                CollectionReference alimentosRef = _nutrifoodbanco.Collection("nutrifoodwpf");
                // Realiza a consulta para obter todos os documentos da coleção
                QuerySnapshot snapshot = await alimentosRef.GetSnapshotAsync();
                // Limpa a coleção de alimentos antes de adicionar os novos dados
                Alimentos.Clear();

                // Itera sobre os documentos retornados pela consulta e converte cada um para um objeto Alimento
                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    // Verifica se o documento existe antes de tentar convertê-lo
                    if (doc.Exists)
                    {
                        // Converte o documento para um objeto Alimento e adiciona à coleção ObservableCollection
                        Alimento alimento = doc.ConvertTo<Alimento>();
                        // Verifica se o nome do alimento contém o texto de pesquisa (ignorando maiúsculas/minúsculas)
                        if (alimento.Nome.Contains(TextoPesquisa, StringComparison.OrdinalIgnoreCase))
                        {
                            Alimentos.Add(alimento);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar alimentos: {ex.Message}");
                throw;
            }
        }
    }
}
