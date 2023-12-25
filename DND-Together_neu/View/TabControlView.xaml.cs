using DND_Together_neu.Model;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Shapes;

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

        public void LoadScene(string name)
        {
            Scene scene = XML.LoadScene(name);
            foreach(Category category in scene.Categories)
            {
                
            }
        }

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
                Name = "tab_" + trimCatName,
                Padding = new Thickness(20,10,20,10),
                Content = new TabControl()
            };
            tabCategories.Items.Add(newTabItem);

            // Textfeld leeren
            tf_CategoryName.Text = "";
            if (tabCategories.SelectedItem == null)
                tabCategories.SelectedIndex = 0;
        }

        TabItem tabCategoryEdit;
        private void btn_EditCategory_Click(object sender, RoutedEventArgs e)
        {
            // Wenn noch kein Tab editiert wird
            if(tabCategoryEdit == null)
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
                tabCategoryEdit = (TabItem)tabCategories.SelectedItem;
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

                // Den Text vom Textfeld in den Tab übertragen
                tabCategoryEdit.Header = tf_CategoryName.Text;

                // Das Textfeld leeren
                tf_CategoryName.Text = "";

                // Editierter Tab leeren
                tabCategoryEdit = null;
            }
            // (optional, TODO) Geschriebener Text live mit dem Text auf dem Tab ändern
        }

        private void btn_DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            TabItem deleteTab = (TabItem)tabCategories.Items[tabCategories.SelectedIndex];
            if(MessageBox.Show("Sicher, dass die Kategorie \"" + deleteTab.Header.ToString() + "\" gelöscht werden soll?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Debug.Print("Lösche Kategorie \"" + deleteTab.Header.ToString() + "\"...");
                foreach (TabItem category in tabCategories.Items)
                {
                    if (category.Header.ToString() == ((TabItem)tabCategories.SelectedItem).Header.ToString())
                    {
                        Debug.Print(category.Header.ToString());
                        tabCategories.Items.Remove(category);
                        return;
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
                    string trimPageName = tf_PageName.Text.Replace(" ", "");
                    TabItem newTabItem = new TabItem
                    {
                        Header = tf_PageName.Text,
                        Name = "tab_" + trimPageName,
                    };

                    var webView = new WebView2();
                    webView.Initialized += WebView_Initialized;


                    webView.Source = new Uri(tf_PageUrl.Text);
                    newTabItem.Content = webView;


                    currentTabContent.Items.Add(newTabItem);

                    // Textfeld leeren

                    currentTabContent.SelectedItem = newTabItem;

                }
            }
        }

        private async void WebView_Initialized(object? sender, EventArgs e)
        {
            WebView2 webView = (WebView2)sender;
            await webView.EnsureCoreWebView2Async(null);
            
            webView.Source = new Uri(tf_PageUrl.Text);
            tf_PageName.Text = "";
            tf_PageUrl.Text = "";
        }

        TabItem tabPageEdit;
        private void btn_EditPage_Click(object sender, RoutedEventArgs e)
        {
            TabControl currentTabContent = (TabControl)(((TabItem)tabCategories.SelectedItem).Content);
            if(currentTabContent.SelectedItem != null)
                if (tabPageEdit == null)
                {
                    tabPageEdit = (TabItem)currentTabContent.SelectedItem;

                    foreach(TabItem tabItem in currentTabContent.Items)
                    {
                        if(tabItem.Header.ToString() != tabPageEdit.Header.ToString())
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
                    tabPageEdit = (TabItem)currentTabContent.SelectedItem;
                    // Alle Tabs aktivieren
                    foreach (TabItem page in currentTabContent.Items)
                    {
                        page.IsEnabled = true;
                    }
                    // Alle anderen Buttons aktivieren
                    btn_AddCategory.IsEnabled = true;
                    btn_EditCategory.IsEnabled = true;
                    btn_DeleteCategory.IsEnabled = true;
                    btn_AddPage.IsEnabled = true;
                    btn_DeletePage.IsEnabled = true;

                    // Den Text des Edit-Buttons zu einem Zahnrad ändern
                    btn_EditPage.Content = "⚙";

                    // Den Text vom Textfeld in den Tab übertragen
                    tabPageEdit.Header = tf_PageName.Text;

                    // Editierter Tab leeren
                    tabPageEdit = null;

                    tf_PageName.Text = "";
                    tf_PageUrl.Text = "";
                }
        }

        private void btn_DeletePage_Click(object sender, RoutedEventArgs e)
        {
            TabControl currentTabControl = ((TabControl)(((TabItem)tabCategories.SelectedItem).Content));
            TabItem deletePageTab = (TabItem)currentTabControl.SelectedItem;

            if (MessageBox.Show("Sicher, dass die Seite \"" + deletePageTab.Header.ToString() + "\" gelöscht werden soll?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Debug.Print("Lösche Seite \"" + deletePageTab.Header.ToString() + "\"...");
                foreach(TabItem item in currentTabControl.Items)
                {
                    if(item.Header.ToString() == deletePageTab.Header.ToString())
                    {
                        currentTabControl.Items.Remove(item);
                        Debug.Print("Seite gelöscht.");
                        return;
                    }
                }
            }
        }

        private void menuClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            Scene scene = new Scene();
            foreach(TabItem category in tabCategories.Items)
            {
                // Create new Category
                Category cat = new Category()
                {
                    Name = category.Name,
                };
                // Add pages
                foreach(TabItem page in ((TabControl)(category.Content)).Items)
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
        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
