﻿using System;
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
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlArray("Categories")]
        [XmlArrayItem("Category")]
        public List<Category> Categories { get; set; } = new();
        public Scene() { }
    }
}