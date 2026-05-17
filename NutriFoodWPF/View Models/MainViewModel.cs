using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NutriFoodWPF.Models;
using NutriFoodWPF.Repositories;

namespace NutriFoodWPF.View_Models
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly AlimentoRepository _alimentoRepository;
        public ObservableCollection<Alimento> Alimentos { get; set; }
        public ICommand PesquisarCommand { get; }
        public ICommand CarregarAlimentosCommand { get; }

        public MainViewModel()
        {
            Alimentos = new ObservableCollection<Alimento>();
            PesquisarCommand = new RelayCommand(async () => await ExecutarPesquisa());
            CarregarAlimentosCommand = new RelayCommand(async () => await CarregarAlimentos());

            _alimentoRepository = new AlimentoRepository();
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
                var todosAlimentos = await _alimentoRepository.ObterAlimentos();

                Alimentos.Clear();


                foreach (var alimento in todosAlimentos)
                {
                    if (alimento.Nome?.Contains(TextoPesquisa ?? "", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        Alimentos.Add(alimento);
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
