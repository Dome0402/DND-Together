using DND_Together.Commands;
using DND_Together.MVVM.View;
using Microsoft.Web.WebView2.Wpf;

using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        private IEnumerable<TabItem> _categoryTabs;
        public IEnumerable<TabItem> CategoryTabs
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
        public TabItem SelectedItem
        {
            get { return _selectedItem; }
            set {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        // Binded from TextBox tf_CategoryName -> Text
        private string _newCategory;
        public string CategoryName
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


        //
        // Commands
        //

        public ICommand AddCategoryCommand { get; }
        public ICommand EditCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }

        public ICommand AddPageCommand { get; }
        public ICommand EditPageCommand { get; }
        public ICommand DeletePageCommand { get; }


        public OverviewTabViewModel()
        {
            AddCategoryCommand = new AddCategoryCommand(this);
            EditCategoryCommand = new EditCategoryCommand(this);
            DeleteCategoryCommand = new DeleteCategoryCommand(this);

            AddPageCommand = new AddPageCommand(this);
            EditPageCommand = new EditPageCommand(this);
            DeletePageCommand = new DeletePageCommand(this);

            IsEnabledEditCategory = true;
            IsEnabledEditPage = true;
            IsEnabledOtherElements = true;

            ContentButtonEditCategory = "⚙";

        }
    }
}
