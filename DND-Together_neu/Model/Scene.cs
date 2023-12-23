using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace DND_Together_neu.Model
{
    [Serializable]
    [XmlRoot("Scene")]
    public class Scene
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlArray("Categories")]
        [XmlArrayItem("Category")]
        public List<Category> Categories { get; set; }

        public Scene()
        {
            Categories = new List<Category>();
        }

        /// <summary>
        /// Adds/appends a Category into the List
        /// </summary>
        /// <param name="category">Category to be added</param>
        public void AddCategory(Category category)
        {
            if(Categories.Contains(category))
            {
                MessageBox.Show("Eine Kategorie mit dem Titel \"" + category.Name + "\" ist bereits vorhanden.", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Categories.Add(category);
        }

        /// <summary>
        /// Removes a specific Category from the List
        /// </summary>
        /// <param name="category">Category to be removed</param>
        public void RemoveCategory(Category category)
        {
            if(Categories.Contains(category))
            {
                Categories.Remove(category);
                return;
            }
            MessageBox.Show("Eine Kategorie mit dem Titel \"" + category.Name + "\" existiert nicht.", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void InsertCategory(Category category, int index)
        {
            if (Categories.Contains(category))
            {
                MessageBox.Show("Eine Kategorie mit dem Titel \"" + category.Name + "\" ist bereits vorhanden.", "Achtung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Categories.Insert(index, category);
        }

    }
}
