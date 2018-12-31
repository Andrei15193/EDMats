﻿using System;
using System.Windows;
using System.Windows.Threading;
using EDMats.Services;
using EDMats.Services.Implementations;
using EDMats.Stores;
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
                .RegisterType<ITradeSolutionService, TradeSolutionService>();

        internal static T EnsureDependencies<T>(T instance)
            => _container.BuildUp(instance);

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherUnhandledException += _UnhandledException;

            MainWindow = EnsureDependencies(new MainWindow());
            MainWindow.Show();
        }

        private void _UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        internal static CommanderInfoStore CommanderInfoStore
            => (CommanderInfoStore)Current.FindResource(nameof(CommanderInfoStore));

        internal static GoalsStore GoalsStore
            => (GoalsStore)Current.FindResource(nameof(GoalsStore));

        internal static NotificationsStore NotificationsStore
            => (NotificationsStore)Current.FindResource(nameof(NotificationsStore));
    }
}