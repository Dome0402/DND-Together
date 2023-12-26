using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace DND_Together_neu.Model
{
    public static class XML
    {
        public static IEnumerable<TabControl> GetTabControls()
        {

            return new TabControl[] { };
        }
        /// <summary>
        /// Saves the parent TabControl into an XML File in the "Data"-Folder.
        /// If no "Data"-Folder exists it creates a new one.
        /// </summary>
        /// <param name="tabControls">Parent TabControl where each Tab contains another TabControl with Tabs (pages)</param>
        public static void SaveScene(Scene scene)
        {
            string path = "./Data/";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Scene));
            TextWriter textWriter = new StreamWriter(path + scene.Name + ".dndts");

            xmlSerializer.Serialize(textWriter, scene);
            textWriter.Close();
        }

        public static Scene LoadScene(string path)
        {
            Scene scene;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Scene));
            TextReader textReader = new StreamReader(path);

            scene = (Scene)xmlSerializer.Deserialize(textReader);
            textReader.Close();

            return scene;
        }
    }
}
