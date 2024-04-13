﻿using System.Windows.Input;
using BookShelf.Domain.Settings;
using BookShelf.Domain.Version;
using BookShelf.ViewModels.Commands;
using BookShelf.ViewModels.Windows;

namespace BookShelf.ViewModels.MainWindow;

public class MainWindowViewModel : WindowViewModel<IMainWindowMementoWrapper>, IMainWindowViewModel
{
    private readonly IAboutWindowViewModel _aboutWindowViewModel;
    private readonly Command _closeMainWindowCommand;
    private readonly Command _openAboutWindowCommand;
    private readonly IWindowManager _windowManager;

    public MainWindowViewModel(IMainWindowMementoWrapper mainWindowMementoWrapper, IWindowManager windowManager,
        IAboutWindowViewModel aboutWindowViewModel, IApplicationVersionProvider applicationVersionProvider)
        : base(mainWindowMementoWrapper)
    {
        _windowManager = windowManager;
        _aboutWindowViewModel = aboutWindowViewModel;

        _closeMainWindowCommand = new Command(CloseMainWindow);
        _openAboutWindowCommand = new Command(OpenAboutWindow);

        Version = $"Version {applicationVersionProvider.Version.ToString(3)}";
    }

    public string Title => "Book Shelf";

    public string Version { get; }

    public ICommand CloseMainWindowCommand => _closeMainWindowCommand;
    public ICommand OpenAboutWindowCommand => _openAboutWindowCommand;

    private void OpenAboutWindow()
    {
        _windowManager.Show(_aboutWindowViewModel);
    }

    private void CloseMainWindow()
    {
        _windowManager.Close(this);
    }

    public override void WindowClosing()
    {
        _windowManager.Close(_aboutWindowViewModel);
    }
}