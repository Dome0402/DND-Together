using DND_Together.MVVM.Model;
using DND_Together.MVVM.ViewModels;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DND_Together.Commands
{
    public class SaveSceneCommand : CommandBase
    {
        private OverviewTabViewModel _overviewTabViewModel;


        public override void Execute(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "DnD-Together Szenen(*.dndts)|*.dndts";
            saveFileDialog.Title = "Speichern unter";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.DefaultDirectory = System.IO.Directory.GetCurrentDirectory();
            saveFileDialog.FileName = _overviewTabViewModel.Scene.Name;
            if (saveFileDialog.ShowDialog() == true)
            {
                Scene scene = new Scene();

                scene = ((Scene)parameter) == null ? _overviewTabViewModel.Scene : (Scene)parameter;
                scene.Name = Path.GetFileNameWithoutExtension(saveFileDialog.SafeFileName);
                
                XML.SaveScene(scene, saveFileDialog.FileName);
                _overviewTabViewModel.Path = saveFileDialog.FileName;
                Application.Current.Windows[0].Title = "D&D Together - " + scene.Name;

                Consts.SceneHasChanged = false;
            }
        }

        public SaveSceneCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
