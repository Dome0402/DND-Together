using DND_Together_neu.Model;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace DND_Together_neu.View
{
    /// <summary>
    /// Interaktionslogik für TabControlView.xaml
    /// </summary>
    public partial class TabControlView : UserControl
    {
        TabControl TabControl;
        public TabControlView()
        {
            InitializeComponent();
        }
        Scene scene = new Scene();

        public void generateStartTabControl()
        {
            TabControl = new TabControl()
            {
                ItemsSource = new List<string>()
                {
                    "Test 1",
                    "Test 2"
                }
            };
            tabCategories.ItemsSource = TabControl.ItemsSource;
        }

        private void tabCategories_Loaded(object sender, RoutedEventArgs e)
        {
            tabCategories = (sender as TabControl);
        }

        private void btn_AddCategory_Click(object sender, RoutedEventArgs e)
        {
            foreach (TabItem category in tabCategories.Items)
            {
                if(category.Header.ToString() == tf_CategoryName.Text)
                {
                    MessageBox.Show("Kategorie ist bereits vorhanden!", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            string trimCatName = tf_CategoryName.Text.Replace(" ", "");
            TabItem newTabItem = new TabItem
            {
                Header = tf_CategoryName.Text,
                Padding = new Thickness(20,10,20,10),
                Content = new TabControl()
            };
            tabCategories.Items.Add(newTabItem);
            scene.AddCategory(new Category()
            {
                Name = newTabItem.Header.ToString(),
                Pages = []
            });

            // Textfeld leeren
            tf_CategoryName.Text = "";
            if (tabCategories.SelectedItem == null)
                tabCategories.SelectedIndex = 0;
            
        }

        TabItem tabCategoryEdit;
        private void btn_EditCategory_Click(object sender, RoutedEventArgs e)
        {
            // Wenn überhaupt eine Kategorie existiert
            if (tabCategories.SelectedItem != null)
            {
                // Wenn noch kein Tab editiert wird
                if (tabCategoryEdit == null)
                {
                    tabCategoryEdit = (TabItem)tabCategories.SelectedItem;
                    // Alle anderen Tabs deaktivieren
                    foreach (TabItem category in tabCategories.Items)
                    {
                        // Alle Tabs, außer dem zzt ausgewählten Tab, deaktivieren
                        if (category.Header.ToString() != tabCategoryEdit.Header.ToString())
                        {
                            // Tab deaktivieren
                            category.IsEnabled = false;
                        }
                    }
                    // Alle anderen Buttons deaktivieren
                    btn_AddCategory.IsEnabled = false;
                    btn_DeleteCategory.IsEnabled = false;
                    btn_AddPage.IsEnabled = false;
                    btn_DeletePage.IsEnabled = false;
                    btn_EditPage.IsEnabled = false;

                    // Das Textfeld fokusieren
                    tf_CategoryName.Focus();

                    // Den Text des Edit-Buttons zu einem Haken ändern
                    btn_EditCategory.Content = "✓";

                    // In das Textfeld den aktuellen Text des Tabs einfügen
                    tf_CategoryName.Text = tabCategoryEdit.Header.ToString();
                }
                // Wenn gerade ein Tab editiert wird, alles wieder rückgängig machen
                else
                {
                    // tabCategoryEdit = (TabItem)tabCategories.SelectedItem;
                    // Alle Tabs aktivieren
                    foreach (TabItem category in tabCategories.Items)
                    {
                        category.IsEnabled = true;
                    }
                    // Alle anderen Buttons aktivieren
                    btn_AddCategory.IsEnabled = true;
                    btn_DeleteCategory.IsEnabled = true;
                    btn_AddPage.IsEnabled = true;
                    btn_DeletePage.IsEnabled = true;
                    btn_EditPage.IsEnabled = true;

                    // Den Text des Edit-Buttons zu einem Zahnrad ändern
                    btn_EditCategory.Content = "⚙";

                    
                    scene.EditCategory(tabCategoryEdit.Header.ToString(), tf_CategoryName.Text);


                    // Den Text vom Textfeld in den Tab übertragen
                    tabCategoryEdit.Header = tf_CategoryName.Text;

                    // Das Textfeld leeren
                    tf_CategoryName.Text = "";

                    // Editierter Tab leeren
                    tabCategoryEdit = null;
                }
                // (optional, TODO) Geschriebener Text live mit dem Text auf dem Tab ändern
            }
        }

        private void btn_DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if(tabCategories.SelectedItem != null)
            {
                TabItem deleteTab = (TabItem)tabCategories.Items[tabCategories.SelectedIndex];
                if (MessageBox.Show("Sicher, dass die Kategorie \"" + deleteTab.Header.ToString() + "\" gelöscht werden soll?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Debug.Print("Lösche Kategorie \"" + deleteTab.Header.ToString() + "\"...");
                    foreach (TabItem category in tabCategories.Items)
                    {
                        if (category.Header.ToString() == ((TabItem)tabCategories.SelectedItem).Header.ToString())
                        {
                            Debug.Print(category.Header.ToString());
                            tabCategories.Items.Remove(category);

                            scene.RemoveCategory(category.Header.ToString());

                            return;
                        }
                    }
                }
            }
            
        }


        // Liste binden?? Oder eher doch nicht??
        private void btn_AddPage_Click(object sender, RoutedEventArgs e)
        {
            // Wenn beide Felder, Titel und Url, nicht leer sind
            if (tf_PageName.Text != "" && tf_PageUrl.Text != "" && tabCategories.SelectedItem != null)
            {
                TabControl currentTabContent = (TabControl)(((TabItem)tabCategories.SelectedItem).Content);
                if (currentTabContent != null)
                {
                    foreach (TabItem page in currentTabContent.Items)
                    {
                        if (page.Header.ToString() == tf_PageName.Text)
                        {
                            MessageBox.Show("Seite ist bereits vorhanden!", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    TabItem newTabItem = new()
                    {
                        Header = tf_PageName.Text,
                    };

                    var webView = new WebView2();

                    Initialize_WebView(webView, new Uri(tf_PageUrl.Text));

                    newTabItem.Content = webView;


                    currentTabContent.Items.Add(newTabItem);

                    scene.Categories.Find(c => c.Name == ((TabItem)tabCategories.SelectedItem).Header).AddPage(new Model.Page()
                    {
                        Title = tf_PageName.Text,
                        Url = tf_PageUrl.Text
                    });

                    // Textfeld leeren

                    currentTabContent.SelectedItem = newTabItem;

                    tf_PageName.Text = "";
                    tf_PageUrl.Text = "";

                }
            }
        }
        private void Initialize_WebView(WebView2 webView, Uri url)
        {
            webView.EnsureCoreWebView2Async(null);
            
            webView.Source = url;
        }

        TabItem tabPageEdit;
        private void btn_EditPage_Click(object sender, RoutedEventArgs e)
        {
            if (tabCategories.SelectedItem != null && ((TabControl)(((TabItem)tabCategories.SelectedItem).Content)) != null)
            {
                TabControl currentTabContent = (TabControl)(((TabItem)tabCategories.SelectedItem).Content);
                if (currentTabContent.SelectedItem != null)
                    if (tabPageEdit == null)
                    {
                        tabPageEdit = (TabItem)currentTabContent.SelectedItem;

                        foreach (TabItem tabItem in currentTabContent.Items)
                        {
                            if (tabItem.Header.ToString() != tabPageEdit.Header.ToString())
                            {
                                tabItem.IsEnabled = false;
                            }
                        }

                        // Alle anderen Buttons deaktivieren
                        btn_AddCategory.IsEnabled = false;
                        btn_EditCategory.IsEnabled = false;
                        btn_DeleteCategory.IsEnabled = false;
                        btn_AddPage.IsEnabled = false;
                        btn_DeletePage.IsEnabled = false;


                        // Das Textfeld fokusieren
                        tf_PageUrl.Focus();

                        // Den Text des Edit-Buttons zu einem Haken ändern
                        btn_EditPage.Content = "✓";

                        // In das Textfeld den aktuellen Text des Tabs einfügen
                        tf_PageName.Text = tabPageEdit.Header.ToString();
                        tf_PageUrl.Text = ((WebView2)tabPageEdit.Content).Source.ToString();
                    }
                    else
                    {
                        try
                        {
                            // URL versuchen als Quelle der WebView zu setzen
                            ((WebView2)tabPageEdit.Content).Source = new Uri(tf_PageUrl.Text);
                        }
                        catch (UriFormatException ex)
                        {
                            MessageBox.Show("Es muss eine gültige URL eingegeben werden.");
                            return;
                        }
                        // tabPageEdit = (TabItem)currentTabContent.SelectedItem;
                        // Alle Tabs aktivieren
                        foreach (TabItem p in currentTabContent.Items)
                        {
                            p.IsEnabled = true;
                        }
                        // Alle anderen Buttons aktivieren
                        btn_AddCategory.IsEnabled = true;
                        btn_EditCategory.IsEnabled = true;
                        btn_DeleteCategory.IsEnabled = true;
                        btn_AddPage.IsEnabled = true;
                        btn_DeletePage.IsEnabled = true;

                        // Den Text des Edit-Buttons zu einem Zahnrad ändern
                        btn_EditPage.Content = "⚙";

                        string oldPageName = tabPageEdit.Header.ToString();
                        string newPageName = tf_PageName.Text;
                        string newPageUrl = tf_PageUrl.Text;

                        // Den Text vom Textfeld in den Tab übertragen
                        tabPageEdit.Header = tf_PageName.Text;

                        //((TabItem)(((TabControl)(((TabItem)(tabCategories.SelectedItem)).Content)).SelectedItem)).Header = tf_PageName.Text;

                        //Application.Current.Dispatcher.Invoke(() =>{tabPageEdit.Header = tf_PageName.Text;});
                        //scene.Categories.Find(c => c.Name == ((TabItem)tabCategories.SelectedItem).Header).Pages));


                        tf_PageName.Text = "";
                        tf_PageUrl.Text = "";

                        // Editierter Tab leeren
                        tabPageEdit = null;
                        Model.Page page;
                        // Ändere den Name der Seite in der globalen Scene "scene"
                        foreach(Category cat in scene.Categories)
                        {
                            if(cat.Name == ((TabItem)tabCategories.SelectedItem).Header.ToString())
                            {
                                foreach(Model.Page p in cat.Pages)
                                {
                                    if(p.Title == oldPageName)
                                    {
                                        p.Title = newPageName;
                                        p.Url = newPageUrl;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
            }
        }

        private void btn_DeletePage_Click(object sender, RoutedEventArgs e)
        {
            if(tabCategories.SelectedItem != null && ((TabControl)(((TabItem)tabCategories.SelectedItem).Content)) != null)
            {
                TabControl currentTabControl = ((TabControl)(((TabItem)tabCategories.SelectedItem).Content));
                TabItem deletePageTab = (TabItem)currentTabControl.SelectedItem;

                if (MessageBox.Show("Sicher, dass die Seite \"" + deletePageTab.Header.ToString() + "\" gelöscht werden soll?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Debug.Print("Lösche Seite \"" + deletePageTab.Header.ToString() + "\"...");
                    foreach (TabItem item in currentTabControl.Items)
                    {
                        if (item.Header.ToString() == deletePageTab.Header.ToString())
                        {
                            currentTabControl.Items.Remove(item);

                            // Ändere den Name der Seite in der globalen Scene "scene"
                            string oldPageName = item.Header.ToString();
                            foreach (Category cat in scene.Categories)
                            {
                                if (cat.Name == ((TabItem)tabCategories.SelectedItem).Header.ToString())
                                {
                                    foreach (Model.Page p in cat.Pages)
                                    {
                                        if (p.Title == oldPageName)
                                        {
                                            cat.RemovePage(p);
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }

                            Debug.Print("Seite gelöscht.");
                            return;
                        }
                    }
                }
            }
        }

        private void menuClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void menuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "DnD-Together Szenen(*.dndts)|*.dndts";
            saveFileDialog.Title = "Speichern unter";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.DefaultDirectory = System.IO.Directory.GetCurrentDirectory();
            saveFileDialog.FileName = scene.Name;
            if (saveFileDialog.ShowDialog() == true)
            {
                Scene scene = new Scene();

                scene.Name = Path.GetFileNameWithoutExtension(saveFileDialog.SafeFileName);
                foreach (TabItem category in tabCategories.Items)
                {
                    // Create new Category
                    Category cat = new Category()
                    {
                        Name = category.Header.ToString(),
                    };
                    // Add pages
                    foreach (TabItem page in ((TabControl)(category.Content)).Items)
                    {
                        cat.AddPage(new Model.Page()
                        {
                            Title = page.Header.ToString(),
                            Url = ((WebView2)page.Content).Source.ToString()
                        });
                    }
                    scene.AddCategory(cat);
                }
                XML.SaveScene(scene);
                Application.Current.Windows[0].Title = "D&D Together - " + scene.Name;
            }
        }


        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DnD-Together Szenen(*.dndts)|*.dndts";
            openFileDialog.DefaultDirectory = System.IO.Directory.GetCurrentDirectory();
            if (openFileDialog.ShowDialog() == true)
            {
                LoadScene(openFileDialog.FileName);
            }
        }

        public void LoadScene(string path)
        {
            try
            {
                if ((tabCategories.Items) != null)
                {
                    foreach (TabItem item in tabCategories.Items)
                    {
                        TabControl tabControlPages = (TabControl)(((TabItem)tabCategories.SelectedItem).Content);
                        if (tabControlPages != null)
                        {
                            foreach (TabItem page in tabControlPages.Items)
                            {
                                // Gibt Speicher von WebView2 frei (hoffentlich)
                                (page.Content as WebView2).Dispose();
                            }
                            tabControlPages.Items.Clear();
                        }
                    }
                    tabCategories.Items.Clear();
                }


                scene = XML.LoadScene(path);
                foreach (Category category in scene.Categories)
                {
                    TabItem tab = new TabItem();
                    tab.Header = category.Name;
                    tab.Padding = new Thickness(20, 10, 20, 10);
                    tabCategories.Items.Add(tab);
                    tabCategories.SelectedItem = tab;

                    tab.Content = new TabControl();


                    foreach (Model.Page page in category.Pages)
                    {
                        TabItem tabPage = new TabItem();
                        tabPage.Header = page.Title;
                        //

                        var webView = new WebView2();
                        Initialize_WebView(webView, new Uri(page.Url));

                        tabPage.Content = webView;

                        ((TabControl)tab.Content).Items.Add(tabPage);
                    }

                }
                Application.Current.Windows[0].Title = "D&D Together - " + scene.Name;
            }
            catch (Exception e)
            {
                MessageBox.Show("Fehler beim Laden der Szene aufgetreten.\n" + e.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
