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
        private Scene initializedScene;

        [SetUp]
        public void Setup()
        {
            initializedScene = new Scene()
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
            if (e.Keys != "q") throw new ArgumentException();
        }

        private void sceneNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Direction != Direction.North) throw new ArgumentException();
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
            Assert.IsNotNull(initializedScene);
            Assert.IsNotNull(initializedScene.Title);
            Assert.AreEqual(initializedScene.Title, "Test Scene");
            Assert.IsNotNull(initializedScene.Description);
            Assert.AreEqual(initializedScene.Description, "This is a test scene");
            Assert.IsNotNull(initializedScene.MapPosition);
            Assert.AreEqual(initializedScene.MapPosition.X, 1);
            Assert.AreEqual(initializedScene.MapPosition.Y, 1);
            Assert.IsNotNull(initializedScene.Commands);
            Assert.AreEqual(initializedScene.Commands.Count, 1);
            Assert.AreEqual(initializedScene.Commands[0].Keys, "x");
            Assert.AreEqual(initializedScene.Commands[0].Description, "Exit");
            Assert.DoesNotThrow(() => { initializedScene.Commands[0].Action(); });
        }

        [Test]
        public void ConstructorBindCommandEvent()
        {
            Assert.DoesNotThrow(() => { constructedScene.Commands[0].Action(); });
            Assert.DoesNotThrow(() => { constructedScene.Commands[1].Action(); });
        }
        [Test]
        public void ConstructorGameCommandEvent()
        {
            Assert.Throws<NotImplementedException>(() => { constructedScene.Commands[1].Action(); });
        }
    }
}
