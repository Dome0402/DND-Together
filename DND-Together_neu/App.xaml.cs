﻿using DND_Together.MVVM.ViewModels;
using DND_Together.MVVM.View;
using System.Configuration;
using System.Data;
using System.Windows;

namespace DND_Together
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();

            

            base.OnStartup(e);
        }
    }

}
