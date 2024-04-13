using BookShelf.Domain.Settings;
using Newtonsoft.Json;
using System.IO;
using BookShelf.Infrastructure.Common;
using BookShelf.Infrastructure.Settings;

namespace BookShelf.Infrastructure.Settings
{
    internal class MainWindowMementoWrapper : WindowMementoWrapper<MainWindowMemento>, IMainWindowMementoWrapper
    {
        public MainWindowMementoWrapper(IPathService pathService) : base(pathService)
        {

        }

        protected override string MementoName => "MainWindowMemento";
    }
}

