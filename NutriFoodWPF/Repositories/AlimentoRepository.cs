using Google.Cloud.Firestore;
using NutriFoodWPF.Data;
using NutriFoodWPF.Models;
using NutriFoodWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (alimentoValidado != null)
            {
                // Se a validação for bem-sucedida, salva o alimento no Firestore
                CollectionReference colRef = _firestoreDb.Collection("alimentos");
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
