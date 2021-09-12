using System.Windows;
using EDMats.Storage;
using Unity;

namespace EDMats
{
    public partial class App : Application
    {
        private static readonly IUnityContainer _unityContainer;

        static App()
        {
            _unityContainer = new UnityContainer()
                .RegisterSingleton<IStorageHandler, InMemoryStorageHandler>();
        }

        public static T Resolve<T>()
            => _unityContainer.Resolve<T>();
    }
}