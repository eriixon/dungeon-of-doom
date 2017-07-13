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
        [Test]
        public void CompositeTest()
        {
            var actual = new CompositeTest();
            
            var expected = new List<IComposite>();

            Assert.That(actual.GetItems(), Is.Not.Null);
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
