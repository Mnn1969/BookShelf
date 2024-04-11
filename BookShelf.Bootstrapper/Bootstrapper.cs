﻿using System.Windows;
using BookShelf.View;

namespace BookShelf.Bootstrapper
{
    public class Bootstrapper : IDisposable
    {
        public Window Run()
        {
            var mainWindow = new MainWindow();

            mainWindow.Show();

            return mainWindow;
        }

        public void Dispose()
        {
           
        }
    }
}
