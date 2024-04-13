using BookShelf.ViewModels.MainWindow;
using System.ComponentModel;

namespace BookShelf.Views.AboutWindow;

public partial class AboutWindow : IAboutWindow
{
    public AboutWindow(IAboutWindowViewModel aboutWindowViewModel)
    {
        InitializeComponent();

        DataContext = aboutWindowViewModel;
    }

    
}