using DND_Together.MVVM.Model;
using DND_Together.MVVM.ViewModels;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DND_Together.Commands
{
    public class EditPageCommand : CommandBase
    {
        private readonly OverviewTabViewModel _overviewTabViewModel;
        public override void Execute(object parameter)
        {
            // If a category is selected
            if (_overviewTabViewModel.SelectedCategory != null && _overviewTabViewModel.SelectedCategory.Content != null )
            {
                TabItem currentPage = (TabItem)(_overviewTabViewModel.SelectedCategory.Content as TabControl).SelectedItem;
                // If a page is selected
                if(currentPage != null)
                {
                    // If no page is being edited
                    if (!_overviewTabViewModel.IsPageEditing)
                    {
                        // Disable all pages
                        foreach(TabItem page in (_overviewTabViewModel.SelectedCategory.Content as TabControl).Items)
                        {
                            page.IsEnabled = false;
                        }
                        // Disable all Categories
                        foreach (TabItem category in _overviewTabViewModel.CategoryTabs)
                        {
                            category.IsEnabled = false;
                        }
                        _overviewTabViewModel.IsEnabledEditCategory = false;
                        _overviewTabViewModel.IsEnabledOtherElements = false;

                        _overviewTabViewModel.ContentButtonEditPage = "✓";

                        string url = "";
                        foreach(Category cat in _overviewTabViewModel.Scene.Categories)
                        {
                            if(url != "")
                                break;
                            
                            foreach(MVVM.Model.Page p in cat.Pages)
                            {
                                if(p.Title == currentPage.Header.ToString())
                                {
                                    url = p.HomeUrl;
                                    break;
                                }
                            }
                        }

                        _overviewTabViewModel.PageName = currentPage.Header.ToString();
                        _overviewTabViewModel.PageUrl = url;

                        _overviewTabViewModel.IsPageEditing = true;
                    }
                    // If page is being edited
                    else
                    {
                        try
                        {
                            (currentPage.Content as WebView2).Source = new Uri(_overviewTabViewModel.PageUrl);
                        }
                        catch(UriFormatException ex)
                        {
                            MessageBox.Show("Es muss eine gültige URL eingegeben werden.");
                            return;
                        }
                        currentPage.Header = _overviewTabViewModel.PageName;


                        _overviewTabViewModel.ContentButtonEditPage = "⚙";
                        // Enable all Pages
                        foreach (TabItem page in (_overviewTabViewModel.SelectedCategory.Content as TabControl).Items)
                        {
                            page.IsEnabled = true;
                        }

                        // Enable all Categories
                        foreach (TabItem category in _overviewTabViewModel.CategoryTabs)
                        {
                            category.IsEnabled = true;
                        }

                        foreach (Category cat in _overviewTabViewModel.Scene.Categories)
                        {
                            foreach (MVVM.Model.Page p in cat.Pages)
                            {
                                if (p.Title == currentPage.Header.ToString())
                                {
                                    p.Url = _overviewTabViewModel.PageUrl;
                                    p.HomeUrl = _overviewTabViewModel.PageUrl;
                                    break;
                                }
                            }
                        }

                        _overviewTabViewModel.IsEnabledEditCategory = true;
                        _overviewTabViewModel.IsEnabledOtherElements = true;

                        _overviewTabViewModel.IsPageEditing = false;


                        _overviewTabViewModel.PageName = "";
                        _overviewTabViewModel.PageUrl = "";
                    }
                }
            }

            Debug.Print("Seite \"" + _overviewTabViewModel.PageName + "\" mit der URL: \"" + _overviewTabViewModel.PageUrl + "\" wurde editiert!");
        }

        public EditPageCommand(OverviewTabViewModel overviewTabViewModel)
        {
            _overviewTabViewModel = overviewTabViewModel;
        }
    }
}
