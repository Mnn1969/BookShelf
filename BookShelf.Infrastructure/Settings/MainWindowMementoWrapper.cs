using BookShelf.Domain.Settings;
using Newtonsoft.Json;
using System.IO;

namespace BookShelf.Infrastructure.Settings
{
    internal class MainWindowMementoWrapper : IMainWindowMementoWrapper, IMainWindowMementoWrapperInitializer, IDisposable
    {
        private bool _initialized;
        private MainWindowMemento _mainWindowMemento;
        private string _settingFilePath;

        public MainWindowMementoWrapper()
        {
            _mainWindowMemento = new MainWindowMemento();
        }

        public void Dispose()
        {
            EnsureInitialized();

            var serializedMemento = JsonConvert.SerializeObject(_mainWindowMemento);

            File.WriteAllText(_settingFilePath, serializedMemento);
        }


        public double Left
        {
            get
            {
                EnsureInitialized();
                return _mainWindowMemento.Left;
            }

            set
            {
                EnsureInitialized();
                _mainWindowMemento.Left = value;
            }
        }

        public double Top
        {
            get
            {
                EnsureInitialized();
                return _mainWindowMemento.Top;
            }
            set
            {
                EnsureInitialized();
                _mainWindowMemento.Top = value;
            }
        }

        public double Width
        {
            get
            { 
                EnsureInitialized();
                return _mainWindowMemento.Width;
            }
            set
            {
                EnsureInitialized();
                _mainWindowMemento.Width = value;
            }
        }

        public double Height
        {
            get
            {
                EnsureInitialized();
                return _mainWindowMemento.Height; 
            }
            set
            {
                EnsureInitialized();
                _mainWindowMemento.Height = value;
            }
        }

        public bool IsMaximized
        {
            get
            {
                EnsureInitialized();
                return _mainWindowMemento.IsMaximized;
            }
            set
            {
                EnsureInitialized();
                _mainWindowMemento.IsMaximized = value;
            }
        }

        

        public void Initialize()
        {
            if (_initialized)
                throw new InvalidOperationException($"{nameof(IMainWindowMementoWrapper)} is already initialized");

            _initialized = true;

            var localApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            const string company = "MNN";
            const string applicationName = "BookShelf";
            const string settingsFolderName = "Settings";

            var settingsPath = Path.Combine(localApplicationDataPath, company, applicationName, settingsFolderName);

            _settingFilePath = Path.Combine(settingsPath, "MainWindowMemento.json");

            Directory.CreateDirectory(settingsPath);

            if (!File.Exists(_settingFilePath)) return;

            var serializedMemento = File.ReadAllText(_settingFilePath);

            _mainWindowMemento = JsonConvert.DeserializeObject<MainWindowMemento>(serializedMemento);
        }

        private void EnsureInitialized()
        {
            if (!_initialized)
                throw new InvalidOperationException($"{nameof(IMainWindowMementoWrapper)} is not initialized");
        }
    }
}
