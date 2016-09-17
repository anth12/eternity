using System;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using Eternity.Core;
using Eternity.Core.Repositories;
using Eternity.ViewModels.Home;
using Eternity.Views;

namespace Eternity
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static IContainer Container { get; private set; }
        protected void App_OnStartup(object sender, StartupEventArgs e)
        {
            RegisterDependencies();

            Bootstrapper.Setup();

            var window = new HomeWindow();

            window.DataContext = Container.Resolve<HomeViewModel>();
            window.Show();

            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);
            
        }

        #region Event handling

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // ErrorLog.Log("Unhandled Exception", e.Exception);


#if DEBUG   // In debug mode do not custom-handle the exception, let Visual Studio handle it

            e.Handled = false;

#else

            ShowUnhandeledException(e);    

#endif     
        }

        private void ShowUnhandeledException(DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            string errorMessage =
                $@"An application error occurred.
Please check whether your data is correct and repeat the action. If this error occurs again there seems to be a more serious malfunction in the application, and you better close it and try again...
Error:
{e.Exception.Message + (e.Exception.InnerException != null ? "\n" + e.Exception.InnerException.Message : null)}
Do you want to continue?
(if you click Yes the application will remain open, if you click No the application will close)";

            if (
                MessageBox.Show(errorMessage, "Application Error", MessageBoxButton.YesNoCancel, MessageBoxImage.Error) == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }
        }

        #endregion

        #region IoC component registration

        private static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
               .Where(t => t.Name.EndsWith("Service"))
               .AsSelf()
               .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
               .Where(t => t.Name.EndsWith("Repository"))
               .AsSelf()
               .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("ViewModel"))
                .AsSelf();

            Container = builder.Build();
        }

        #endregion
    }
}
