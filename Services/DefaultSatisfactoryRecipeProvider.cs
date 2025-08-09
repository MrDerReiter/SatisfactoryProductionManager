using FactoryManagementCore;
using SatisfactoryProductionManager.Extensions;
using System.IO;

namespace SatisfactoryProductionManager
{
    public class DefaultSatisfactoryRecipeProvider(string filePath) : IRecipeProvider<SatisfactoryRecipe>
    {
        private readonly HashSet<SatisfactoryRecipe> _recipies =
            ReadRecipiesFromFile(filePath);


        private static HashSet<SatisfactoryRecipe> ReadRecipiesFromFile(string filePath)
        {
            static ResourceStream[] Convert(string[] content)
            {
                if (content is ["null"]) return [];

                return content.Length == 2 && content[1] == "null" ?
                    [content[0].ToStream()] :
                    [.. content.Select(str => str.ToStream())];
            }


            var set = new HashSet<SatisfactoryRecipe>();
            var content = File.ReadAllLines(filePath);

            for (int ptr = 0; ptr < content.Length; ptr++)
            {
                if (content[ptr] != "<--->") continue;

                string name = content[++ptr];
                string category = content[++ptr];
                string machine = content[++ptr];

                try
                {
                    var outputs = Convert(content[++ptr].Split("||"));
                    var inputs = Convert(content[++ptr].Split("||"));

                    set.Add(new SatisfactoryRecipe
                    {
                        Name = name,
                        Category = category,
                        AllowedMachines = [machine],
                        Inputs = inputs,
                        Outputs = outputs
                    });

                    ptr++;
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException
                        ($"Ошибка при обработке рецепта: {name}\n" +
                         $"Проверьте данные в текстовом файле {filePath}", ex);
                }
            }

            return set;
        }


        public SatisfactoryRecipe Get(string name)
        {
            return _recipies.FirstOrDefault(recipe => recipe.Name == name) ??
                   throw new KeyNotFoundException
                           ($"Не удалось найти в базе рецепт {name}");
        }

        public IEnumerable<SatisfactoryRecipe> GetAll() => _recipies;

        public IEnumerable<SatisfactoryRecipe> GetSubset
            (Func<SatisfactoryRecipe, bool> predicate) => _recipies.Where(predicate);
    }
}
