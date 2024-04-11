using Autofac;
using BookShelf.Views.MainWindow;

namespace BookShelf.Views
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<View.MainWindow.MainWindow>().As<IMainWindow>().InstancePerDependency();
        }
    }
}
