using System.Windows;
using BookShelf.ViewModels.MainWindow;
using BookShelf.Views.MainWindow;

namespace BookShelf.View.MainWindow
{
    public partial class MainWindow : IMainWindow
    {
        public MainWindow(IMainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
        }
    }
}