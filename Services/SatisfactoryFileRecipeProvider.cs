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
        private readonly string _path;
        private HashSet<SatisfactoryRecipe> _recipies;


        public SatisfactoryFileRecipeProvider()
           : this($"{Environment.CurrentDirectory}\\Recipies.txt") { }

        public SatisfactoryFileRecipeProvider(string path)
        {
            _path = path;
            _recipies = ReadRecipiesFromFile(_path);
        }


        private HashSet<SatisfactoryRecipe> ReadRecipiesFromFile(string filePath)
        {
            var list = new HashSet<SatisfactoryRecipe>();
            var content = File.ReadAllLines(filePath, Encoding.Unicode);

            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == "<--->")
                {
                    string name = content[i + 1];
                    string category = content[i + 2];
                    string machine = content[i + 3];

                    string[] strOutputs = content[i + 4].Split("||");
                    var outputs = new ResourceStream[strOutputs[1] == "null" ? 1 : 2];
                    for (int j = 0; j < outputs.Length; j++)
                    {
                        string outRes = strOutputs[j].Split(' ')[0];
                        double outCount = double.Parse(strOutputs[j].Split(' ')[1], CultureInfo.InvariantCulture);
                        outputs[j] = new ResourceStream(outRes, outCount);
                    }

                    string[] strInputs = content[i + 5].Split("||");
                    var inputs = new ResourceStream[strInputs.Length];
                    for (int j = 0; j < strInputs.Length; j++)
                    {
                        string inpRes = strInputs[j].Split(' ')[0];
                        double inpCount = double.Parse(strInputs[j].Split(' ')[1], CultureInfo.InvariantCulture);
                        inputs[j] = new ResourceStream(inpRes, inpCount);
                    }

                    list.Add(new SatisfactoryRecipe(name, machine, category, inputs, outputs));
                    i += 5;
                }
            }

            return list;
        }


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
            var list = _recipies.Where(recipe => recipe.Product.Resource == product);
            return list.Count() > 0 ? list : throw new InvalidOperationException("Не удалось найти ни одного подходящего рецепта для данного продукта");
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
