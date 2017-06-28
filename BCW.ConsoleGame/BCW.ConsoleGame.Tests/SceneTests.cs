using NUnit.Framework;
using BCW.ConsoleGame.Models.Scenes;
using BCW.ConsoleGame.Models;
using System.Collections.Generic;
using BCW.ConsoleGame.Models.Commands;
using BCW.ConsoleGame.Events;
using System;

namespace BCW.ConsoleGame.Tests
{
    [TestFixture]
    public class SceneTests
    {
        private Scene testScene;
        private Scene constructedScene;

        [SetUp]
        public void Setup()
        {
            testScene = new Scene()
            {
                Title = "Test Scene",
                Description = "Test of Scenes",
                Visited = true,
                MapPosition = new MapPosition(1,1),
                Commands = new List<ICommand> {
                    new Command { Keys = "x", Description="Exit", Action = () => {return; }}
                }
            };
            constructedScene = new Scene(
                "Test Scene",
                "Test of Scenes",
                new MapPosition(1, 1),
                new List<ICommand>
                {
                    new NavigationCommand { Keys = "n", Description="North", Direction = Direction.North },
                    new GameCommand { Keys = "q", Description = "Qiut"}
                });
            constructedScene.Navigated += sceneNavigated;
            constructedScene.GameMenuSelected += sceneGameMenuSelected;
        }

        private void sceneGameMenuSelected(object sender, GameEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void sceneNavigated(object sender, NavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DefaultConstructorDoesNotSetProperties()
        {
            var scene = new Scene();

            Assert.IsNull(scene.Title);
            Assert.IsNull(scene.Description);
            Assert.IsFalse(scene.Visited);
            Assert.IsNull(scene.MapPosition);
            Assert.IsNull(scene.Commands);
        }

        [Test]
        public void ConstructorSupportObjectInit()
        {
            Assert.IsNotNull(testScene.Title);
            Assert.IsNotNull(testScene.Description);
            Assert.IsTrue(testScene.Visited);
            Assert.IsNotNull(testScene.MapPosition);
            Assert.IsNotNull(testScene.Commands);
            Assert.AreEqual(testScene.MapPosition.X, 1);
            Assert.AreEqual(testScene.MapPosition.Y, 1);
            Assert.AreEqual(testScene.Commands[0].Description, "Exit");
            Assert.DoesNotThrow(() => { testScene.Commands[0].Action(); });
        }

        [Test]
        public void ConstructorBindCommandEvent()
        {
            Assert.Throws<NotImplementedException>(() => { constructedScene.Commands[0].Action(); });
        }
        [Test]
        public void ConstructorGameCommandEvent()
        {
            Assert.Throws<NotImplementedException>(() => { constructedScene.Commands[1].Action(); });
        }
    }
}
