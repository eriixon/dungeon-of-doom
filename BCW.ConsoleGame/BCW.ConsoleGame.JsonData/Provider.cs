using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BCW.ConsoleGame.Models.Scenes;
using BCW.ConsoleGame.Data;
using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Commands;
using BCW.ConsoleGame.Events;
using System.Text;

namespace BCW.ConsoleGame.JsonData
{
    public class Provider : IDataProvider
    {
        public List<IScene> Scenes { get; set; }
        public MapPosition StartPosition { get; set; }
        JObject gameData;

        public Provider()
        {
            LoadJsonData();
            LoadData();
        }

        public void LoadJsonData()
        {
            var dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Scenes.json");
            using (StreamReader reader = File.OpenText(dataFilePath))
            {
                gameData = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            }
        }
        public void LoadData()
        {
            var scenesJson = (JArray)gameData.GetValue("Scenes");
            Scenes = scenesJson.Select(s => new Scene
               (
                  (string)s["Title"],
                  (string)s["Description"],
                   new MapPosition((int)s["MapPosition"]["X"], (int)s["MapPosition"]["Y"]),
                  (s["NavigationCommands"] as JArray).Select(c => new NavigationCommand
                    {
                      Keys = (string)c["Keys"],
                      Description = (string)c["Description"],
                      Direction = (Direction)Enum.Parse(typeof(Direction), (string)c["Direction"])
                  }).ToList<ICommand>(),
                  new List<ICommand> { new GameCommand { Keys = "X", Description = "Exit The Game" } }
               )).ToList<IScene>();

            var startPositionJSON = gameData.GetValue("StartPosition");
            StartPosition = startPositionJSON.ToObject<MapPosition>();

        }
        public void saveGameData()
        {
            gameData = JObject.FromObject(new
            {
                StartPosition = new
                {
                    X = StartPosition.X,
                    Y = StartPosition.Y,
                },
                Sence = from s in Scenes
                        select new
                        {
                            Title = s.Title,
                            Description = s.Description,
                            MapPosition = new
                            {
                                X = s.MapPosition.X,
                                Y = s.MapPosition.Y
                            },
                            NavigationCommands = from c in s.Commands.Where(c => c is INavigationCommand)
                                                 select new
                                                 {
                                                    Keys = c.Keys,
                                                    Description = c.Description,
                                                    Direction = Enum.GetName(typeof(Direction), (c as INavigationCommand).Direction)
                                                 }                            
                        }
            });
            var fileData = Encoding.ASCII.GetBytes(gameData.ToString());
            var dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Scenes.json");
            using (FileStream writer = File.Open(dataFilePath, FileMode.Truncate))
            {
                writer.Write(fileData, 0, fileData.Length);
            }
        }
    }
}
