using DND_Together.MVVM.ViewModels;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DND_Together.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if(((this.DataContext as MainViewModel).CurrentViewModel as OverviewTabViewModel).AreChanges)
            {
                MessageBoxResult result = MessageBox.Show("Es sind noch ungespeicherte Änderungen vorhanden. Möchten Sie diese vorher speichern?", "Achtung!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    ((this.DataContext as MainViewModel).CurrentViewModel as OverviewTabViewModel).SaveSceneCommand.Execute(this);
                }
            }
        }
    }
}