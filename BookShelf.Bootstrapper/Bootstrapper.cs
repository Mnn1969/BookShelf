using Autofac;
using BookShelf.ViewModels;
using BookShelf.Views.MainWindow;
using System.Windows;
using BookShelf.Infrastructure.Settings;

namespace BookShelf.Bootstrapper
{
    public class Bootstrapper : IDisposable
    {
        private readonly IContainer _container;

        public Bootstrapper()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterModule<Infrastructure.RegistrationModule>()
                .RegisterModule<Views.RegistrationModule>()
                .RegisterModule<RegistrationModule>();

            _container = containerBuilder.Build();
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public Window Run()
        {
            InitializeDependencies();

            var mainWindow = _container.Resolve<IMainWindow>();

            if (mainWindow is not Window window)
                throw new NotImplementedException();

            window.Show();

            return window;
        }

        private void InitializeDependencies()
        {
            _container.Resolve<IMainWindowMementoWrapperInitializer>().Initialize();
        }
    }
}
