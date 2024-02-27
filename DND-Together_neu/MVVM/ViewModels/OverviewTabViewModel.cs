using DND_Together.Commands;
using DND_Together.MVVM.Model;
using DND_Together.MVVM.View;
using Microsoft.Web.WebView2.Wpf;

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace DND_Together.MVVM.ViewModels
{
    public class OverviewTabViewModel : ViewModelBase
    {
        //
        // Properties
        //
        
        // Binded from Category TabControl -> ItemsSource
        private List<TabItem> _categoryTabs;
        public List<TabItem> CategoryTabs
        {
            get { return _categoryTabs; }
            set
            {
                _categoryTabs = value;
                OnPropertyChanged(nameof(CategoryTabs));
            }
        }

        // Binded from Category TabControl -> SelectedItem
        private TabItem _selectedItem;
        public TabItem SelectedCategory
        {
            get { return _selectedItem; }
            set {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        // Binded from TextBox tf_CategoryName -> Text
        private string? _newCategory;
        public string? CategoryName
        {
            get { return _newCategory; }
            set { 
                _newCategory = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        // Binded from TextBox tf_PageName -> Text
        private string _pageName;
        public string PageName
        {
            get { return _pageName; }
            set { 
                _pageName = value; 
                OnPropertyChanged(nameof(PageName));
            }
        }

        // Binded from tf_PageUrl -> Text
        private string _pageUrl;
        public string PageUrl
        {
            get { return _pageUrl; }
            set
            {
                _pageUrl = value;
                OnPropertyChanged(nameof(PageUrl));
            }
        }

        // Binded from btn_AddCategory, btn_DeleteCategory, btn_AddPage, btn_DeletePage -> IsEnabled
        private bool _isEnabledOtherElements;
        public bool IsEnabledOtherElements
        {
            get { return _isEnabledOtherElements; }
            set {
                _isEnabledOtherElements = value;
                OnPropertyChanged(nameof(IsEnabledOtherElements));
            }
        }

        // Binded from tf_CategoryName, btn_EditCategory -> IsEnabled
        private bool _isEnabledEditCategory;
        public bool IsEnabledEditCategory
        {
            get { return _isEnabledEditCategory; }
            set {
                _isEnabledEditCategory = value;
                OnPropertyChanged(nameof(IsEnabledEditCategory));
            }
        }

        // Binded from tf_PageName, tf_PageUrl, btn_EditPage -> IsEnabled
        private bool _isEnabledEditPage;
        public bool IsEnabledEditPage
        {
            get { return _isEnabledEditPage; }
            set
            {
                _isEnabledEditPage = value;
                OnPropertyChanged(nameof(IsEnabledEditPage));
            }
        }

        
        private string _contentButtonEditCategory;
        public string ContentButtonEditCategory
        {
            get { return _contentButtonEditCategory; }
            set { 
                _contentButtonEditCategory = value; 
                OnPropertyChanged(nameof(ContentButtonEditCategory));
            }
        }


        private string _contentButtonEditPage;
        public string ContentButtonEditPage
        {
            get { return _contentButtonEditPage; }
            set { 
                _contentButtonEditPage = value;
                OnPropertyChanged(nameof(ContentButtonEditPage));
            }
        }



        //
        // Commands
        //

        public ICommand AddCategoryCommand { get; }
        public ICommand EditCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }

        public ICommand AddPageCommand { get; }
        public ICommand EditPageCommand { get; }
        public ICommand DeletePageCommand { get; }

        public ICommand OpenSceneCommand { get; }
        public ICommand SaveSceneCommand { get; }
        public ICommand CloseApplicationCommand { get; }

        public ICommand CategoryNameTextBoxOnEnterCommand { get; }
        public ICommand GoHomeCommand { get; }

        public ICommand MoveCategoryUpCommand { get; }
        public ICommand MoveCategoryDownCommand { get; }
        public ICommand MovePageLeftCommand { get; }
        public ICommand MovePageRightCommand { get; }

        //
        // Local Variables
        //
        public bool IsCategoryEditing { get; set; }

        public bool IsPageEditing { get; set; }

        public Scene Scene { get; set; } = new();

        public Dictionary<string, Model.Page> Pages { get; set; } = new();
        public string Path { get; set; }

        public OverviewTabViewModel()
        {
            AddCategoryCommand = new AddCategoryCommand(this);
            EditCategoryCommand = new EditCategoryCommand(this);
            DeleteCategoryCommand = new DeleteCategoryCommand(this);

            AddPageCommand = new AddPageCommand(this);
            EditPageCommand = new EditPageCommand(this);
            DeletePageCommand = new DeletePageCommand(this);

            CategoryTabs = new();
            SaveSceneCommand = new SaveSceneCommand(this);
            CloseApplicationCommand = new CloseApplicationCommand(this);

            CategoryNameTextBoxOnEnterCommand = new CategoryNameTextBoxOnEnter(this);
            GoHomeCommand = new GoHomeCommand(this);

            MoveCategoryDownCommand = new MoveCategoryDownCommand(this);
            MoveCategoryUpCommand = new MoveCategoryUpCommand(this);
            MovePageLeftCommand = new MovePageLeftCommand(this);
            MovePageRightCommand = new MovePageRightCommand(this);


            IsEnabledEditCategory = true;
            IsEnabledEditPage = true;
            IsEnabledOtherElements = true;

            ContentButtonEditCategory = "⚙";
            ContentButtonEditPage = "⚙";


            OpenSceneCommand = new OpenSceneCommand(this);


            // Create AppData folder
            Debug.Print(Consts.AppDataPath);
            Directory.CreateDirectory(Consts.AppDataPath);
        }
    }
}
