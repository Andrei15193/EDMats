using System.Windows;
using System.Windows.Threading;
using EDMats.Services;
using EDMats.Services.Implementations;
using EDMats.Storage;
using Unity;

namespace EDMats
{
    public partial class App : Application
    {
        private static readonly IUnityContainer _container = _GetUnityContainer();

        private static IUnityContainer _GetUnityContainer()
            => new UnityContainer()
                .RegisterType<IFileSystemService, FileSystemService>()
                .RegisterType<IJournalImportService, JournalImportService>()
                .RegisterType<IJournalReaderService, JournalReaderService>()
                .RegisterType<IJournalFileImportService, JournalFileImportService>()
                .RegisterType<IGoalsStorageService, GoalsStorageService>()
                .RegisterType<IGoalsFileStorageService, GoalsFileStorageService>()
                .RegisterType<IMaterialTraderService, MaterialTraderService>()
                .RegisterType<ITradeSolutionService, TradeSolutionService>()
                .RegisterType<IProfileStorageHandler, ProfileStorageHandler>()
                .RegisterSingleton<IStorageHandler, InMemoryStorageHandler>();

        internal static TService Resolve<TService>()
            => _container.Resolve<TService>();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DispatcherUnhandledException += _UnhandledException;

            MainWindow = new MainWindow();
            MainWindow.Show();
        }

        private void _UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = MainWindow.IsVisible;
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}