using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Treasures
{
    public interface ITreasure
    {
        string Name { get; set; }
        int Value { get; set; }
    }
}
