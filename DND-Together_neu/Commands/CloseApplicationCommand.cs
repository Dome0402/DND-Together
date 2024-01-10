using DND_Together.MVVM.Model;
using DND_Together.MVVM.ViewModels;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DND_Together.Commands
{
    public class CloseApplicationCommand : CommandBase
    {
        private OverviewTabViewModel _overviewTabViewModel;


        public override void Execute(object parameter)
        {
            if (Consts.SceneHasChanged(_overviewTabViewModel) && MessageBox.Show("Sie haben die Sitzung nicht gespeichert! Ohne Speichern beenden?", "Achtung!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                if(parameter.GetType() == typeof(CancelEventArgs))
                {
                    ((CancelEventArgs)parameter).Cancel = true;
                }
                return;
            }
            Application.Current.Shutdown();
        }
        public CloseApplicationCommand(OverviewTabViewModel overviewTabViewModel)
        {
            this._overviewTabViewModel = overviewTabViewModel;
        }
        
    }
}
