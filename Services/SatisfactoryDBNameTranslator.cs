using FactoryManagementCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace SatisfactoryProductionManager.Services
{
    public class SatisfactoryDBNameTranslatorRU : INameTranslator
    {
        private static readonly string _connectionString =
             "Data Source=(LocalDB)\\MSSQLLocalDB;" +
            $"AttachDbFilename={Environment.CurrentDirectory}\\SatisfactoryStuffDB.mdf;" +
             "Integrated Security=True;";

        private static readonly Dictionary<string, string> _dictionary = InitializeDictionary();


        private static Dictionary<string, string> InitializeDictionary()
        {
            var connection = new SqlConnection(_connectionString);

            var dict = new Dictionary<string, string>();
            var selectCommand = new SqlCommand
                ("SELECT * FROM [TranslationStrings]", connection);

            using (connection)
            {
                connection.Open();

                var reader = selectCommand.ExecuteReader();
                while (reader.Read())
                    dict.Add(reader.GetString("InnerString"), reader.GetString("ExternalStringRU"));

                return dict;
            }
        }

        public string Translate(string name)
        {
            try { return _dictionary[name]; }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Не удалось перевести строку; обнаружено некорректное поведение словаря.");

                throw new InvalidDataException();
            }
        }
    }
}
