using DND_Together.MVVM.ViewModels;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DND_Together.MVVM.Model
{
    public static class Consts
    {
        public static string XmlVersion = "1.0";
        public static string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DND-Together\\WebView2Cache");
        public static bool SceneHasChanged = false;

        public static async void Initialize_WebView(WebView2 webView, Uri url)
        {
            var webView2Environment = await CoreWebView2Environment.CreateAsync(null, Consts.AppDataPath);
            await webView.EnsureCoreWebView2Async(webView2Environment);

            webView.Source = url;
            webView.NavigationCompleted += WebViewNavigated;
        }

        public static void WebViewNavigated(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            Consts.SceneHasChanged = true;
        }
    }
}
