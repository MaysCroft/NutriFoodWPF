using System;
using System.IO;
using System.Configuration;
using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace NutriFoodWPF.Data
{
    public class DatabaseFirestore
    {
        private static readonly string pastaBase = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NutriFoodWPF");

        private static readonly string caminhoBanco = Path.Combine(pastaBase, "nutrifoodwpf.db");

        private static readonly string connectionstring = $"Data Source={caminhoBanco}";

        static DatabaseFirestore()
        {
            if (!Directory.Exists(pastaBase))
            {
                Directory.CreateDirectory(pastaBase);
            }

            if (!File.Exists(caminhoBanco))
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Alimentos (
                            Id TEXT PRIMARY KEY AUTOINCREMENT,
                            Nome TEXT NOT NULL,
                            Calorias DOUBLE NOT NULL,
                            PorcaoGramas DOUBLE NOT NULL,
                            GorduraTotal DOUBLE NOT NULL,
                            GorduraSaturada DOUBLE NOT NULL,
                            Proteina DOUBLE NOT NULL,
                            Sodio DOUBLE NOT NULL,
                            Potassio DOUBLE NOT NULL
                            Colesterol DOUBLE NOT NULL,
                            Carboidratos DOUBLE NOT NULL,
                            Fibras DOUBLE NOT NULL,
                            Acucar DOUBLE NOT NULL
                        );
                    ";

                    command.ExecuteNonQuery();
                }
            }
        }

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionstring);
        }
    }
}
