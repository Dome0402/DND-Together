using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DND_Together.MVVM.Model
{
    [Serializable]
    public class Category
    {
        [XmlElement("CatName")]
        public string Name { get; set; }

        [XmlArray("Pages")]
        [XmlArrayItem("Page")]
        public List<Page> Pages { get; set; }

        public Category()
        {
            Pages = new List<Page>();
        }
    }
}
