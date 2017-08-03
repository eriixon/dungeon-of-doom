using BCW.ConsoleGame.Models.Scenes;
using BCW.ConsoleGame.Models.Treasures;
using System;
using System.Collections.Generic;

namespace BCW.ConsoleGame.Models.Characters
{
    public class MonsterFactory : IMonsterFactory
    {
        private static IMonsterFactory instance;
        private IList<IMonsterType> monsterTypes;
        private IList<string> treasureName;

        private MonsterFactory(IList<IMonsterType> monsterTypes)
        {
            this.monsterTypes = monsterTypes;
            treasureName = new List<string>()
            {
                "Gold",
                "Silver",
                "Platinum",
                "Ruby",
                "Diamond",
                "Sapphire",
                "Emerald",
                "Garnet",
                "Amulet",
                "Ring"
            };
        }

        public static IMonsterFactory Instance(IList<IMonsterType> monsterTypes = null)
        {
            if(instance == null)
            {
                instance = new MonsterFactory(monsterTypes);
            }

            return instance;
        }

        public IList<IMonster> CreateMonsters(IScene scene)
        {
            var monsters = new List<IMonster>();
            var random = new Random();

            if (scene.Difficulty > 0)
            {
                foreach (var monsterType in monsterTypes)
                {
                    var odds = monsterType.LevelOdds[scene.Difficulty];

                    if (random.Next(1, 100) <= odds.Exist)
                    {
                        var countMin = monsterType.LevelOdds[scene.Difficulty].CountMin;
                        var countMax = monsterType.LevelOdds[scene.Difficulty].CountMax;
                        var count = random.Next(countMin, countMax);
                        while (count > 0)
                        {
                            var health = random.Next(monsterType.HealthMin, monsterType.HealthMax);
                            var damage = random.Next(monsterType.DamageMin, monsterType.DamageMax);
                            var defense = random.Next(monsterType.HealthMin, monsterType.HealthMax);
                            var vitality = random.Next(monsterType.DamageMin, monsterType.DamageMax);
                            var agility = random.Next(monsterType.AgilityMin, monsterType.AgilityMax);

                            var treasureIndex = random.Next(0, 9);
                            var treasureValue = random.Next(5, 25);

                            var treasure = new Treasure(){ Name = treasureName[treasureIndex],Value = treasureValue};
                            var monster = new Monster() { Name = monsterType.Name, Health = health, Damage = damage, Defense = defense, Vitality =vitality, Agility = agility };

                            monster.AddItem("Treasures", treasure);
                            monsters.Add(monster);
                            count--;
                        }
                    }
                }
            }
            return monsters;
        }
    }
}
