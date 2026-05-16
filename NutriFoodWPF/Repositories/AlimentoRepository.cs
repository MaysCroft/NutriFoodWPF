using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using NutriFoodWPF.Models;
using NutriFoodWPF.Data;

namespace NutriFoodWPF.Repositories
{
    internal class AlimentoRepository
    {
        public void InserirAlimento(Alimento alimento)
        {
            using (var connection = DatabaseFirestore.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Alimentos 
                    (Nome, Calorias, PorcaoGramas, GorduraTotal, GorduraSaturada, 
                     Proteina, Sodio, Potassio, Colesterol, Carboidratos, Fibras, Acucar)
                    VALUES 
                    (@Nome, @Calorias, @PorcaoGramas, @GorduraTotal, 
                     @GorduraSaturada, @Proteina, @Sodio, @Potassio, @Colesterol, @Carboidratos, @Fibras, @Acucar);
                ";

                command.Parameters.AddWithValue("@Nome", alimento.Nome);
                command.Parameters.AddWithValue("@Calorias", alimento.Calorias);
                command.Parameters.AddWithValue("@PorcaoGramas", alimento.PorcaoGramas);
                command.Parameters.AddWithValue("@GorduraTotal", alimento.GorduraTotal);
                command.Parameters.AddWithValue("@GorduraSaturada", alimento.GorduraSaturada);
                command.Parameters.AddWithValue("@Proteina", alimento.Proteina);
                command.Parameters.AddWithValue("@Sodio", alimento.Sodio);
                command.Parameters.AddWithValue("@Potassio", alimento.Potassio);
                command.Parameters.AddWithValue("@Colesterol", alimento.Colesterol);
                command.Parameters.AddWithValue("@Carboidratos", alimento.Carboidratos);
                command.Parameters.AddWithValue("@Fibras", alimento.Fibras);
                command.Parameters.AddWithValue("@Acucar", alimento.Acucar);
                command.ExecuteNonQuery();
            }
        }
    }
}
