using FactoryManagementCore.Production;
using SatisfactoryProductionManager.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SatisfactoryProductionManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //Внедрение зависимостей для ProductionManager
            ProductionManager.Initialize
                <SatisfactoryFileRecipeProvider,
                 SatisfactoryFileSaveLoadManager>();
        }
    }

}
