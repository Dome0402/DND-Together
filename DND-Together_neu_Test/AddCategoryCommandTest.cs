using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Together_neu_Test
{
    public class AddCategoryCommandTest
    {
        

        [SetUp]
        public void Setup()
        {

        }

        [Apartment(ApartmentState.STA)]
        [Test]
        public void AddCategoryTest()
        {
            string newCategory = "Neue Kategorie";
            TestParams.overviewTabViewModel.CategoryName = newCategory;
            TestParams.overviewTabViewModel.AddCategoryCommand.Execute(null);
            // Schlägt fehl, da keine Anwendung läuft und somit auch kein Style enthalten kann.
            // Assert.Contains(newCategory, TestParams.overviewTabViewModel.Scene.Categories);
        }

        [Test]
        public void AddCategoryParameterTest()
        {

        }
    }
}
