using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using NutriFoodWPF.Data;
using NutriFoodWPF.Models;
using NutriFoodWPF.Services;
using Google.Cloud.Firestore;

namespace NutriFoodWPF.Repositories
{
    public class AlimentoRepository
    {
        private readonly AlimentoService _service;
        private readonly FirestoreDb _firestoreDb;

        public AlimentoRepository()
        {
            _service = new AlimentoService();
            // O caminho para o arquivo de credenciais deve ser ajustado conforme a localização do seu arquivo JSON
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config_API", "firebase-key.json");
            // Define a variável de ambiente para as credenciais do Google Cloud
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            _firestoreDb = FirestoreDb.Create("nutrifoodwpf");
        }

        public async Task<List<Alimento>> ObterAlimentos()
        {
            var listaAlimentos = new List<Alimento>();

            // Obtém a coleção "nutrifoodwpf" do Firestore
            CollectionReference alimentosRef = _firestoreDb.Collection("nutrifoodwpf");
            QuerySnapshot snapshot = await alimentosRef.GetSnapshotAsync();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    listaAlimentos.Add(doc.ConvertTo<Alimento>());
                }
            }

            return listaAlimentos;
        }

        public async Task<bool> SalvarDados(Alimento novoAlimento)
        {
            // Valida o alimento usando o serviço
            var alimentoValidado = await _service.BuscarAlimento(novoAlimento);

            if (alimentoValidado == null)
                return false;

            if (alimentoValidado != null)
            {
                // Se a validação for bem-sucedida, salva o alimento no Firestore
                CollectionReference colRef = _firestoreDb.Collection("nutrifoodwpf");
                // Se o alimento já tiver um ID, use-o para atualizar o documento existente,
                // caso contrário, crie um novo documento
                DocumentReference docRef = string.IsNullOrEmpty(alimentoValidado.Id)
                    ? colRef.Document()
                    : colRef.Document(alimentoValidado.Id);

                await docRef.SetAsync(alimentoValidado);
                return true;
            }

            return false;
        }
    }
}
