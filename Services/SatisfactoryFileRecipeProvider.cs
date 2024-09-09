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
        private HashSet<SatisfactoryRecipe> _recipies = ReadRecipiesFromFile("Recipies.cfg");


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
                        var outputs = new ResourceStream[strOutputs[1] == "null" ? 1 : 2];
                        for (int j = 0; j < outputs.Length; j++)
                        {
                            string outRes = strOutputs[j].Split(' ')[0];
                            double outCount = double.Parse(strOutputs[j].Split(' ')[1], CultureInfo.InvariantCulture);
                            outputs[j] = new ResourceStream(outRes, outCount);
                        }

                        string[] strInputs = content[++i].Split("||");
                        var inputs = new ResourceStream[strInputs.Length];
                        for (int j = 0; j < strInputs.Length; j++)
                        {
                            string inpRes = strInputs[j].Split(' ')[0];
                            double inpCount = double.Parse(strInputs[j].Split(' ')[1], CultureInfo.InvariantCulture);
                            inputs[j] = new ResourceStream(inpRes, inpCount);
                        }

                        list.Add(new SatisfactoryRecipe(name, machine, category, inputs, outputs));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidDataException
                            ($"Error during parsing recipe: {name}\nCheck data in .cfg file", ex);
                    }
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
