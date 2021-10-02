using System.Windows;
using EDMats.Storage;
using EDMats.Storage.Implementations;
using EDMats.Trading;
using EDMats.Trading.Implementations;
using Unity;

namespace EDMats
{
    public partial class App : Application
    {
        private static readonly IUnityContainer _unityContainer;

        static App()
        {
            _unityContainer = new UnityContainer()
                .RegisterSingleton<ITradeSolutionService, TradeSolutionService>()
                .RegisterSingleton<IMaterialTraderService, MaterialTraderService>()
                .RegisterSingleton<ICommanderProfileStorageHandler, CommanderProfileStorageHandler>()
                .RegisterSingleton<IStorageHandler, InMemoryStorageHandler>();
        }

        public static T Resolve<T>()
            => _unityContainer.Resolve<T>();
    }
}