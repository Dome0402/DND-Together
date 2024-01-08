using DND_Together.MVVM.ViewModels;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DND_Together.Commands
{
    public class CloseApplicationCommand : CommandBase
    {
        private OverviewTabViewModel _overviewTabViewModel;


        public override void Execute(object parameter)
        {
            if (_overviewTabViewModel.AreChanges && MessageBox.Show("Sie haben die Sitzung nicht gespeichert! Ohne Speichern beenden?", "Achtung!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            _overviewTabViewModel.AreChanges = false;
            Application.Current.Shutdown();
        }

        public CloseApplicationCommand(OverviewTabViewModel overviewTabViewModel)
        {
            this._overviewTabViewModel = overviewTabViewModel;
        }
        
    }
}
