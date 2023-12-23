using DND_Together_neu.Model;

namespace DND_Together_neu_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_XmlSaveScene()
        {
            XML.SaveScene(Consts.scene);
            Assert.Pass();
        }

        [Test]
        public void Test_XmlLoadScene()
        {
            Scene scene = XML.LoadScene("NUnit Test");
            Assert.Pass();
        }
    }
}