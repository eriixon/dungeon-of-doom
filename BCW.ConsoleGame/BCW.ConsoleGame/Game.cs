using BCW.ConsoleGame.Data;
using BCW.ConsoleGame.Events;
using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Scenes;
using BCW.ConsoleGame.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCW.ConsoleGame
{
    public class Game
    {
        private IDataProvider DataProvider { get; set; }
        public List<IScene> Scenes { get; set; }
        public MapPosition StartPoint {get;set;}
        public IUserInterface UserInterface { get; set; }

        public Game(IDataProvider dataProvider, IUserInterface userInterface)
        {
            DataProvider = dataProvider;
            Scenes = DataProvider.Scenes;
            UserInterface = userInterface;
            LoadNavigation();
            GotoPosition(DataProvider.StartPosition);
        }

        void GotoPosition(MapPosition position)
        {
            var scene = Scenes.FirstOrDefault(s => s.MapPosition.X == position.X && s.MapPosition.Y == position.Y);
            if (scene != null) scene.Enter();
        }

        private void gameMenuSelected(object sender, GameEventArgs args)
        {
            switch (args.Keys.ToLower())
            {
                case "x":
                    DataProvider.saveGameData();
                    Environment.Exit(0);
                    break;
            }
        }

        private void SceneNavigated(object sender, NavigationEventArgs args)
        {
            var toPosition = new MapPosition(args.Scene.MapPosition.X, args.Scene.MapPosition.Y);

            switch (args.Direction)
            {
                case Direction.North:
                    toPosition.Y -= 1;
                    break;

                case Direction.South:
                    toPosition.Y += 1;
                    break;

                case Direction.East:
                    toPosition.X += 1;
                    break;

                case Direction.West:
                    toPosition.X -= 1;
                    break;
            }

            var nextScene = Scenes.FirstOrDefault(s => s.MapPosition.X == toPosition.X && s.MapPosition.Y == toPosition.Y);
            if (nextScene != null) nextScene.Enter();
        }

        private void LoadNavigation()
        {
            foreach (var scene in Scenes)
            {
                scene.UserInterface = UserInterface;
                scene.GameMenuSelected += gameMenuSelected;
                scene.Navigated += SceneNavigated;
            }
        }
    }
}
