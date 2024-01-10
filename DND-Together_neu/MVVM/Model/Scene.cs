using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DND_Together.MVVM.Model
{
    [Serializable]
    [XmlRoot("Scene")]
    public class Scene
    {
        [XmlAttribute("Version")]
        public string Version { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlArray("Categories")]
        [XmlArrayItem("Category")]
        public List<Category> Categories { get; set; } = new();
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is Scene)) return false;
            Scene other = (Scene)obj;

            if(this.Name != other.Name) return false;
            if(this.Version != other.Version) return false;
            if(this.Categories.Count != other.Categories.Count) return false;


            for(int c = 0; c < this.Categories.Count; c++)
            {
                if (this.Categories[c].Name != other.Categories[c].Name) return false;
                if (this.Categories[c].Pages.Count != other.Categories[c].Pages.Count) return false;
                for(int p = 0; p < this.Categories[c].Pages.Count; p++)
                {
                    if (this.Categories[c].Pages[p].HomeUrl != other.Categories[c].Pages[p].HomeUrl) return false;
                    if (this.Categories[c].Pages[p].Title != other.Categories[c].Pages[p].Title) return false;
                    if (this.Categories[c].Pages[p].Url != other.Categories[c].Pages[p].Url) return false;
                }
            }
            return true;
        }
        public Scene() 
        {

        }

    }
}
