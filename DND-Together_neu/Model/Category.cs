using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace DND_Together_neu.Model
{
    [Serializable]
    public class Category
    {
        [XmlElement("Name")]
        public string Name;

        [XmlArray("Pages")]
        [XmlArrayItem("Page")]
        public List<Page> Pages = new List<Page>();

        public Category(string name)
        {
            Name = name;
        }

        public Category()
        {
            
        }

        public List<Page> GetPages()
        {
            return Pages;
        }

        public void SetPages(List<Page> pages)
        {
            Pages = pages;
        }

        /// <summary>
        /// Adds/appends a page into the List
        /// </summary>
        /// <param name="page">Page to be added</param>
        public void AddPage(Page page)
        {
            if(Pages.Contains(page))
            {
                MessageBox.Show("Eine Seite mit dem Titel \"" + page.Title + "\" ist bereits vorhanden.", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Pages.Add(page);
        }
        /// <summary>
        /// Removes a given Page from the List
        /// </summary>
        /// <param name="page">Page to be removed</param>
        public void RemovePage(Page page)
        {
            if (Pages.Contains(page))
            {
                Pages.Remove(page);
                return;
            }
            MessageBox.Show("Eine Seite mit dem Titel \"" + page.Title + "\" existiert nicht.", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Inserts a Page into the list at a specific index
        /// </summary>
        /// <param name="page">The page to be inserted</param>
        /// <param name="index">Index where the page should be inserted</param>
        public void InsertPage(Page page, int index)
        {
            if (Pages.Contains(page))
            {
                MessageBox.Show("Eine Seite mit dem Titel \"" + page.Title + "\" ist bereits vorhanden.", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Pages.Insert(index, page);
        }
    }
}
