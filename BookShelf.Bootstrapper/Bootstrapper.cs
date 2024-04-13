using Autofac;
using BookShelf.Infrastructure.Settings;
using BookShelf.ViewModels.MainWindow;
using BookShelf.ViewModels.Windows;
using System.Windows;
using BookShelf.Infrastructure.Common;

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
                .RegisterModule<ViewModels.RegistrationModule>()
                .RegisterModule<Views.RegistrationModule>()
                .RegisterModule<RegistrationModule>();

            _container = containerBuilder.Build();
        }


        public Window Run()
        {
            InitializeDependencies();

            var mainWindowViewModel = _container.Resolve<IMainWindowViewModel>();
            var windowManager = _container.Resolve<IWindowManager>();

            var mainWindow = windowManager.Show(mainWindowViewModel);

            if (mainWindow is not Window window)
                throw new NotImplementedException();

            window.Show();

            return window;
        }

        private void InitializeDependencies()
        {
            _container.Resolve<IPathServiceInitializer>().Initialize();

            var windowMementoWrapperInitializers = _container.Resolve<IEnumerable<IWindowMementoWrapperInitializer>>();

            foreach (var windowMementoWrapperInitializer in windowMementoWrapperInitializers)
                windowMementoWrapperInitializer.Initialize();

        }

        public void Dispose()
        {
            _container.Dispose();
        }


    }
}
