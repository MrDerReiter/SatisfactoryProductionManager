using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SatisfactoryProductionManager.Model
{
    public class FileRecipeProvider : IRecipeProvider
    {
        private readonly string _path = $"{Environment.CurrentDirectory}\\Recipies.txt";
        private List<Recipe> _recipies;


        public FileRecipeProvider()
        {
            _recipies = ReadRecipiesFromFile(_path);
        }


        private List<Recipe> ReadRecipiesFromFile(string filePath)
        {
            var list = new List<Recipe>();
            var content = File.ReadAllLines(filePath,Encoding.Unicode);

            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == "<--->")
                {
                    string name = content[i + 1];
                    string category = content[i + 2];
                    string machine = content[i + 3];

                    string[] outputs = content[i + 4].Split("||");
                    string prodRes = outputs[0].Split(' ')[0];
                    double prodCount = double.Parse(outputs[0].Split(' ')[1], CultureInfo.InvariantCulture);
                    var product = new ResourceStream(prodRes, prodCount);

                    ResourceStream? byproduct;
                    if (outputs[1] == "null") byproduct = null;
                    else
                    {
                        string byprRes = outputs[1].Split(' ')[0];
                        double byprCount = double.Parse(outputs[1].Split(' ')[1], CultureInfo.InvariantCulture);

                        byproduct = new ResourceStream(byprRes, byprCount);
                    }

                    string[] strInputs = content[i + 5].Split("||");
                    var inputs = new ResourceStream[strInputs.Length];
                    for (int j = 0; j < strInputs.Length; j++)
                    {
                        string inpRes = strInputs[j].Split(' ')[0];
                        double inpCount = double.Parse(strInputs[j].Split(' ')[1], CultureInfo.InvariantCulture);
                        inputs[j] = new ResourceStream(inpRes, inpCount);
                    }

                    var recipe = new Recipe(name, machine, category, product, inputs, byproduct);
                    list.Add(recipe);
                    i += 5;
                }
            }

            return list;
        }


        public IEnumerable<Recipe> GetAllRecipiesOfCategory(string category)
        {
            var list = _recipies.FindAll(x => x.Category == category);
            return list.Count > 0 ? list 
                : throw new InvalidOperationException("Не удалось найти рецепты в данной категории");
        }

        public IEnumerable<Recipe> GetAllRecipiesOfInput(string input)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recipe> GetAllRecipiesOfMachine(string machine)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recipe> GetAllRecipiesOfProduct(string product)
        {
            var list = _recipies.FindAll
                (x => x.Product.Resource == product || x.Byproduct?.Resource == product);
            return list.Count > 0 ? list : throw new InvalidOperationException("Не удалось найти ни одного подходящего рецепта для данного продукта");
        }

        public Recipe GetRecipeByName(string name)
        {
            return _recipies.Find(x => x.Name == name)
                ?? throw new ArgumentException($"Не удалось найти рецепт с названием {name}");
        }
    }
}
