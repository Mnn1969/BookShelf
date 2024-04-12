using BookShelf.ViewModels.Windows;
using BookShelf.Views.Factories;

namespace BookShelf.Views.Windows
{
    internal class WindowManager : IWindowManager
    {
        private readonly Dictionary<IWindowViewModel, IWindow> _viewModelWindowsMap = new();

        private readonly IWindowFactory _windowFactory;

        public WindowManager(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        public IWindow Show<TWindowViewModel>(TWindowViewModel viewModel) where TWindowViewModel : IWindowViewModel
        {
            var window = _windowFactory.Create(viewModel);

            _viewModelWindowsMap.Add(viewModel, window);

            window.Show();

            return window;
        }

        public void Close<TWindowViewModel>(TWindowViewModel viewModel) where TWindowViewModel : IWindowViewModel
        {
            if (_viewModelWindowsMap.TryGetValue(viewModel, out var window))
            {
                window.Close();

                _viewModelWindowsMap.Remove(viewModel);
            }
        }
        
    }
}
