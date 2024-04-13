using BookShelf.Domain.Settings;
using BookShelf.Infrastructure.Common;
using Newtonsoft.Json;
using System.IO;

namespace BookShelf.Infrastructure.Settings;

internal abstract class WindowMementoWrapper<TMemento> : IMainWindowMementoWrapper, IWindowMementoWrapperInitializer,
    IDisposable
    where TMemento : WindowMemento, new()
{
    private readonly IPathService _pathService;
    private TMemento _windowMemento;
    private bool _initialized;
    private string _settingFilePath;

    protected WindowMementoWrapper(IPathService pathService)
    {
        _pathService = pathService;
        _windowMemento = new TMemento();
    }

    protected abstract string MementoName { get; }

    public double Left
    {
        get
        {
            EnsureInitialized();
            return _windowMemento.Left;
        }

        set
        {
            EnsureInitialized();
            _windowMemento.Left = value;
        }
    }

    public double Top
    {
        get
        {
            EnsureInitialized();
            return _windowMemento.Top;
        }
        set
        {
            EnsureInitialized();
            _windowMemento.Top = value;
        }
    }

    public double Width
    {
        get
        {
            EnsureInitialized();
            return _windowMemento.Width;
        }
        set
        {
            EnsureInitialized();
            _windowMemento.Width = value;
        }
    }

    public double Height
    {
        get
        {
            EnsureInitialized();
            return _windowMemento.Height;
        }
        set
        {
            EnsureInitialized();
            _windowMemento.Height = value;
        }
    }

    public bool IsMaximized
    {
        get
        {
            EnsureInitialized();
            return _windowMemento.IsMaximized;
        }
        set
        {
            EnsureInitialized();
            _windowMemento.IsMaximized = value;
        }
    }

    public void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException($"Wrapper for {typeof(TMemento)} is already initialized");

        _initialized = true;


        const string settingsFolderName = "Settings";

        var settingsPath = Path.Combine(_pathService.ApplicationFolder, settingsFolderName);

        _settingFilePath = Path.Combine(settingsPath, $"{MementoName}.json");

        Directory.CreateDirectory(settingsPath);

        if (!File.Exists(_settingFilePath)) return;

        var serializedMemento = File.ReadAllText(_settingFilePath);

        _windowMemento = JsonConvert.DeserializeObject<TMemento?>(serializedMemento);
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"Wrapper for {typeof(TMemento)} is not initialized");
    }

    public void Dispose()
    {
        EnsureInitialized();

        var serializedMemento = JsonConvert.SerializeObject(_windowMemento);

        File.WriteAllText(_settingFilePath, serializedMemento);
    }
}