﻿using Autofac;
using BookShelf.Domain.DispatcherTimer;
using BookShelf.ViewModels.Windows;
using BookShelf.Views.AboutWindow;
using BookShelf.Views.DispatcherTimer;
using BookShelf.Views.MainWindow;
using BookShelf.Views.Windows;

namespace BookShelf.Views;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<View.MainWindow.MainWindow>().As<IMainWindow>().InstancePerDependency();
        builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
        builder.RegisterType<AboutWindow.AboutWindow>().As<IAboutWindow>().InstancePerDependency();
        builder.RegisterType<DispatcherTimerWrapperFactory>().As<IDispatcherTimerFactory>().SingleInstance();
    }
}