using FactoryManagementCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;

namespace SatisfactoryProductionManager.Services
{
    public class SatisfactoryDBNameTranslatorRU : INameTranslator
    {
        private static readonly string _connectionString;
        private static readonly Dictionary<string, string> _dictionary;


        static SatisfactoryDBNameTranslatorRU()
        {
            _connectionString = GetConnectionString();
            _dictionary = InitializeDictionary();
        }


        private static string GetConnectionString()
        {
            var connectionString = File.ReadAllLines("config.cfg")
                                       .First(str => str.StartsWith("Data Source"))
                                       .Replace("[CurrentDirectory]", Environment.CurrentDirectory);
            return connectionString;
        }

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
            }

            return dict;
        }


        public string Translate(string name)
        {
            return _dictionary[name];
        }
    }
}
