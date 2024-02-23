﻿using SatisfactoryProductionManager.Model.Elements;
using System.Collections.Generic;

namespace SatisfactoryProductionManager.Model.Interfaces
{
    /// <summary>
    /// Представляет класс, реализующий выдачу рецептов по запросу фабрики.
    /// </summary>
    public interface IRecipeProvider
    {
        /// <summary>
        /// Возвращает рецепт с указанным именем, если таковой имеется.
        /// Иначе вызывает исключение.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Recipe GetRecipeByName(string name);

        /// <summary>
        /// Возвращает перечисление всех репептов из указанной категории.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        IEnumerable<Recipe> GetAllRecipiesOfCategory(string category);

        /// <summary>
        /// Возвращает перечисление всех рецептов, имеющих указанный ресурс в качестве основного или побочного продукта.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        IEnumerable<Recipe> GetAllRecipiesOfProduct(string product);

        /// <summary>
        /// Возвращает перечисление всех рецептов, которые реализуются указанным станком/экстрактором.
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        IEnumerable<Recipe> GetAllRecipiesOfMachine(string machine);

        /// <summary>
        /// Возвращает перечисление всех рецептов, в которых указанный ресурс требуется в качестве ингредиента.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IEnumerable<Recipe> GetAllRecipiesOfInput(string input);
    }
}
