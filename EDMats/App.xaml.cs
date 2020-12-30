using System.Linq;
using System.Windows;
using System.Windows.Threading;
using EDMats.Services;
using EDMats.Services.Implementations;
using EDMats.Stores;
using EDMats.ViewModels;
using FluxBase;
using Unity;

namespace EDMats
{
    public partial class App : Application
    {
        private static readonly IUnityContainer _container = _GetUnityContainer();

        private static IUnityContainer _GetUnityContainer()
            => new UnityContainer()
                .RegisterSingleton<FluxBase.Dispatcher>()
                .RegisterSingleton<NotificationsViewModel>()
                .RegisterType<IFileSystemService, FileSystemService>()
                .RegisterType<IJournalImportService, JournalImportService>()
                .RegisterType<IJournalReaderService, JournalReaderService>()
                .RegisterType<IJournalFileImportService, JournalFileImportService>()
                .RegisterType<IGoalsStorageService, GoalsStorageService>()
                .RegisterType<IGoalsFileStorageService, GoalsFileStorageService>()
                .RegisterType<IMaterialTraderService, MaterialTraderService>()
                .RegisterType<ITradeSolutionService, TradeSolutionService>();

        internal static TService Resolve<TService>()
            => _container.Resolve<TService>();

        internal static T EnsureDependencies<T>(T instance)
            => _container.BuildUp(instance);

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherUnhandledException += _UnhandledException;

            var dispatcher = _container.Resolve<FluxBase.Dispatcher>();
            foreach (var store in Resources.Values.OfType<Store>())
                dispatcher.Register(store);

            MainWindow = EnsureDependencies(new MainWindow());
            MainWindow.Show();
        }

        private void _UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = MainWindow.IsVisible;
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        internal static GoalsStore GoalsStore
            => (GoalsStore)Current.FindResource(nameof(GoalsStore));
    }
}