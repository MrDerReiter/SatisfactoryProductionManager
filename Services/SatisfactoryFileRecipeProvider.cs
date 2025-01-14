using FactoryManagementCore.Elements;
using FactoryManagementCore.Interfaces;
using SatisfactoryProductionManager.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SatisfactoryProductionManager.Services
{
    public class SatisfactoryFileRecipeProvider : IRecipeProvider<SatisfactoryRecipe>
    {
        private readonly HashSet<SatisfactoryRecipe> _recipies = ReadRecipiesFromFile("Recipies.cfg");


        private static HashSet<SatisfactoryRecipe> ReadRecipiesFromFile(string filePath)
        {
            var list = new HashSet<SatisfactoryRecipe>();
            var content = File.ReadAllLines(filePath, Encoding.Unicode);

            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == "<--->")
                {
                    string name = content[++i];
                    string category = content[++i];
                    string machine = content[++i];

                    try
                    {
                        string[] strOutputs = content[++i].Split("||");
                        var outputs = new List<ResourceStream>();

                        foreach (var str in strOutputs)
                            if (str != "null")
                                outputs.Add(new ResourceStream(str));

                        List<ResourceStream> inputs;

                        if (content[++i] == "null") inputs = [];
                        else
                        {
                            string[] strInputs = content[i].Split("||");
                            inputs = [];

                            foreach (var str in strInputs)
                                inputs.Add(new ResourceStream(str));
                        }

                        list.Add(new SatisfactoryRecipe(name, machine, category, inputs, outputs));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidDataException
                            ($"Ошибка при обработке рецепта: {name}\nПроверьте данные в файле .cfg с рецептами", ex);
                    }
                }
            }

            return list;
        }


        public IEnumerable<SatisfactoryRecipe> GetAll() => _recipies;

        public IEnumerable<SatisfactoryRecipe> GetAllRecipiesOfCategory(string category)
        {
            var list = _recipies.Where(x => x.Category == category);
            return list.Count() > 0 ? list
                : throw new InvalidOperationException("Не удалось найти рецепты в данной категории");
        }

        public IEnumerable<SatisfactoryRecipe> GetAllRecipiesOfInput(string input)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SatisfactoryRecipe> GetAllRecipiesOfMachine(string machine)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SatisfactoryRecipe> GetAllRecipiesOfProduct(string product)
        {
            var list = _recipies
                .Where(recipe => recipe.Product.Resource == product);

            return list.Any() ? list :
                throw new InvalidOperationException
                ("Не удалось найти ни одного подходящего рецепта для данного продукта");
        }

        public SatisfactoryRecipe GetRecipeByName(string name)
        {
            try
            {
                return _recipies.First(x => x.Name == name);
            }
            catch
            {
                throw new KeyNotFoundException($"Не удалось найти в базе рецепт {name}");
            }
        }
    }
}
