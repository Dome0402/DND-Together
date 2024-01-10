using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DND_Together.MVVM.Model
{
    [Serializable]
    public class Page
    {
        [XmlElement("Title")]
        public string Title {  get; set; }
        [XmlElement("Url")]
        public string Url {  get; set; }
        [XmlElement("Home")]
        public string HomeUrl {  get; set; }
        public Page()
        {

        }
    }
}
