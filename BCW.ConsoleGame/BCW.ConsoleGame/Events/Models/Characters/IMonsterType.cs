using BCW.ConsoleGame.Models.Characters;
using System.Collections;
using System.Collections.Generic;

namespace BCW.ConsoleGame.Data
{
    public interface IMonsterType
    {
        string Name { get; set; }
        int HealthMin { get; set; }
        int HealthMax { get; set; }
        int DamageMin { get; set; }
        int DamageMax { get; set; }
        IDictionary <int, Odds> LevelOdds { get; set; }
    }
}