using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Characters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Tests
{
    [TestFixture]
    class CopositeTests
    {
        private CompositeTest actual;
        [SetUp]
        public void CompositeTest()
        {
            actual = new CompositeTest() { Name = "Inventory" };
            actual.AddItem("treasure/gems/rubies", new CompositeTest() { Name = "Ruby" });
            actual.AddItem("treasure/coins", new CompositeTest() { Name = "Doubloon" });
        }

        [Test]
        public void CountOnlyIncludedLeafNodes()
        {
            //var expected = new List<IComposite>();
            Assert.That(actual.Count, Is.EqualTo(2));
        }

        [Test]
        public void PruneTest()
        {
            var ruby = new CompositeTest() { Name = "Ruby" };
            actual.RemoveItem("treasure / gems / rubies", ruby);
            Assert.That(actual.Count, Is.EqualTo(1));
        }
    }
    class CompositeTest: Composite
    {
        public CompositeTest()
        {
            items = new List<IComposite>();
        }
    }
}
