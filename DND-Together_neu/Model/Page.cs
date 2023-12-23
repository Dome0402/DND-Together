using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DND_Together_neu.Model
{
    [Serializable]
    public class Page
    {
        [XmlElement("Title")]
        public string Title;

        [XmlElement("Url")]
        public string Url;
        public Page(string title, string url)
        {
            Title = title;
            Url = url;
        }
        public Page()
        {
            
        }
    }
}
