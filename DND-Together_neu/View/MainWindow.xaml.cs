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

namespace DND_Together_neu.View
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

        protected override void OnClosing(CancelEventArgs e)
        {
            
            if (tabControlView.AreChanges && MessageBox.Show("Sie haben die Sitzung nicht gespeichert! Ohne Speichern beenden?", "Achtung!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                e.Cancel = true;
                return;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            
        }
    }
}