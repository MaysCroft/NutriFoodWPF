using System;
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

        public AlimentoRepository(FirestoreDb db)
        {
            _service = new AlimentoService();
            _firestoreDb = db;
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
