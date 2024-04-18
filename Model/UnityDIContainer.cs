using SatisfactoryProductionManager.Model.Interfaces;
using Unity;

namespace SatisfactoryProductionManager.Model
{
    public static class CommonUnityDIContainer
    {
        private static readonly UnityContainer _container;


        static CommonUnityDIContainer()
        {
            _container = new UnityContainer();
            InitializeTypeBinding();
        }


        private static void InitializeTypeBinding()
        {
            _container.RegisterType<IFactorySaveLoadManager, FileSaveLoadManager>();
            _container.RegisterType<IRecipeProvider, FileRecipeProvider>();
        }

        public static UnityContainer GetContainer()
        {
            return _container;
        }
    }
}
