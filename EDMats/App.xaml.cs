using System.Windows;
using EDMats.Services;
using EDMats.Services.Implementations;
using Unity;

namespace EDMats
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow = Container.BuildUp(new MainWindow());
            MainWindow.Show();
        }

        public static IUnityContainer Container { get; } = _GetUnityContainer();

        private static IUnityContainer _GetUnityContainer()
            => new UnityContainer()
                .RegisterType<IInventoryService, InventoryService>()
                .RegisterType<IJournalReaderService, JournalReaderService>();
    }
}