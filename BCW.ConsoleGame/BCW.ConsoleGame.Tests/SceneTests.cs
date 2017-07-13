using NUnit.Framework;
using BCW.ConsoleGame.Models.Scenes;
using BCW.ConsoleGame.Models;
using System.Collections.Generic;
using BCW.ConsoleGame.Models.Commands;
using BCW.ConsoleGame.Events;
using System;
using BCW.ConsoleGame.User;
using Moq;

namespace BCW.ConsoleGame.Tests
{
    [TestFixture]
    public class SceneTests
    {
        private Mock<IUserInterface> mockUserInterface;

        private Scene constructedScene;
        private Scene initializedScene;

        [SetUp]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            initializedScene = new Scene()
            {
                Title = "Test Scene",
                Description = "Test of scenes",
                Visited = true,
                MapPosition = new MapPosition(1,1),
                Commands = new List<ICommand> {
                    new Command { Keys = "x", Description="Exit", Action = () => {return; }}
                }
            };
            constructedScene = new Scene(
                "Test Scene",
                "Test of scenes",
                new MapPosition(1, 1),
                new List<ICommand>
                {
                    new NavigationCommand { Keys = "n", Description="North", Direction = Direction.North },
                    new GameCommand { Keys = "x", Description = "Qiut"}
                });
            constructedScene.UserInterface = mockUserInterface.Object;
            constructedScene.Navigated += sceneNavigated;
            constructedScene.GameMenuSelected += sceneGameMenuSelected;
        }

        private void sceneGameMenuSelected(object sender, GameEventArgs e)
        {
            if (e.Keys != "x") throw new ArgumentException();
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
            Assert.AreEqual(initializedScene.Description, "Test of scenes");
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
            Assert.DoesNotThrow(() => { constructedScene.Commands[1].Action(); });
        }
        [Test]
        public void EnterDisplayDetails()
        {
            mockUserInterface.Setup(ui => ui.GetInput("Choose an action: ")).Returns("x");

            constructedScene.Enter();
            mockUserInterface.Verify(ui => ui.Clear());
            mockUserInterface.Verify(ui => ui.Display("Test Scene"));
            mockUserInterface.Verify(ui => ui.Display("Test of scenes"));
        }

        [Test]
        public void EnterDisplayCommandChoises()
        {
            mockUserInterface.Setup(ui => ui.GetInput("Choose an action: ")).Returns("x");
            constructedScene.Enter();
            mockUserInterface.Setup(ui => ui.Display("n = Go North"));
            mockUserInterface.Setup(ui => ui.Display("x = Quit"));
        }
    }
}
