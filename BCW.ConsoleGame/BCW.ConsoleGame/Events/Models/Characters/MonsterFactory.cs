using BCW.ConsoleGame.Data;
using BCW.ConsoleGame.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Characters
{
    public class MonsterFactory : IMonsterFactory
    {
        private static IMonsterFactory instance;
        private IList<IMonsterType> monsterType;

        private MonsterFactory(IList<IMonsterType> monsterType)
        {
            this.monsterType = monsterType;
        }

        public static IMonsterFactory Instance(IList<IMonsterType> monsterType = null)
        {
            if(instance == null)
            {
                instance = new MonsterFactory(monsterType);
            }
            return instance;
        }

        public IList<IMonster> CreateMonsters(IScene scene)
        {
            var monsterTypes = new List<IMonsterType>();
            var monsters = new List<IMonster>();
            var random = new Random();

            if (scene.Difficulty > 0)
            {
                foreach (var mt in monsterTypes)
                {
                    var odds = mt.LevelOdds[scene.Difficulty];
                    if (random.Next(1, 100) <= odds.Exist)
                    {
                        var health = random.Next(mt.HealthMin, mt.HealthMax);
                        var damage = random.Next(mt.DamageMin, mt.DamageMax);
                    }
                }
            }
           return monsters;
        }
    }
}
