using System.Windows;
using EDMats.Services;
using EDMats.Services.Implementations;
using Unity;

namespace EDMats
{
    public partial class App : Application
    {
        private static readonly IUnityContainer _container = _GetUnityContainer();

        private static IUnityContainer _GetUnityContainer()
            => new UnityContainer()
                .RegisterType<IInventoryService, InventoryService>()
                .RegisterType<IJournalReaderService, JournalReaderService>();

        internal static T EnsureDependencies<T>(T instance)
            => _container.BuildUp(instance);

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow = EnsureDependencies(new MainWindow());
            MainWindow.Show();
        }
    }
}