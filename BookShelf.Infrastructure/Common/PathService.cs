using System.IO;

namespace BookShelf.Infrastructure.Common;

internal class PathService : IPathService, IPathServiceInitializer
{
    private string _applicationFolder;
    private bool _initalized;

    public string ApplicationFolder
    {
        get
        {
            EnsureInitialized();
            return _applicationFolder;
        }
        private set => _applicationFolder = value;
    }

    public void Initialize()
    {
        if (_initalized)
            throw new InvalidOperationException($"{nameof(IPathService)} is already initialized");

        _initalized = true;

        var localApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        const string company = "MNN";
        const string applicationName = "BookShelf";

        ApplicationFolder = Path.Combine(localApplicationDataPath, company, applicationName);
    }

    private void EnsureInitialized()
    {
        if (!_initalized)
            throw new InvalidOperationException($"{nameof(IPathService)} is not initialized");
    }
}