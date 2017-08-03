using System.Collections.Generic;

namespace BCW.ConsoleGame.Models.Characters
{
    public class Player: Composite, IPlayer
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public int Vitality { get; set; }
        public int Agility { get; set; }
        public Player()
        {
            items = new List<IComposite>();
        }
    }
}
