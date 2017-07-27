using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Characters
{
    public class Odds
    {
        public int Level { get; set; }
        public int Exist { get; set; }
        public int CountMin { get; set; }
        public int CountMax { get; set; }
        public Odds()
        {

        }
        public Odds(int level,int exist, int countmin, int countmax)
        {
            Level = level;
            Exist = exist;
            CountMin = countmin;
            CountMax = countmax;
        }
    }
}
