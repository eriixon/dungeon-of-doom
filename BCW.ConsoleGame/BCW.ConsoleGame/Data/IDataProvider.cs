using BCW.ConsoleGame.Models;
using BCW.ConsoleGame.Models.Scenes;
using System.Collections.Generic;

namespace BCW.ConsoleGame.Data
{
    public interface IDataProvider
    {
        List<IScene> Scenes { get; set; }
        MapPosition StartPosition { get; set; }
        void saveGameData();
    }
}
