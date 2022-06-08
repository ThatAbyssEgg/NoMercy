using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoMercy.Web.Responses;
using System.Threading.Tasks;

namespace NoMercyClient
{
    public class BattleMode
    {
        public BattleMode(AuthorizationResponse char1, AuthorizationResponse char2)
        {

            Console.WriteLine($"The first character enters the battlefield. Meet {char1.Name}!");
            Console.WriteLine($"The second character enters the battlefield! Greet {char2.Name}!");
            Console.WriteLine($"Let the strife begin!");

            BattleStats statsChar1 = SetStandartStats(char1);
            BattleStats statsChar2 = SetStandartStats(char2);
            while (statsChar1.Health > 0 && statsChar2.Health > 0)
            {
                Strife(statsChar1, statsChar2, char1);
                Strife(statsChar2, statsChar1, char2);
            }
            if (statsChar1.Health <= 0 && statsChar2.Health <= 0)
            {
                Console.WriteLine("What? Both of them have fallen? It's a shame... Really, a shame.");
            }
            else if (statsChar1.Health <= 0)
            {
                Console.WriteLine($"And today's winner is {char2.Name}! Let his name be known for everyone, and let the legends of this strife be told for our ancestors!");
            }
            else if (statsChar2.Health <= 0)
            {
                Console.WriteLine($"And today's winner is {char1.Name}! Let his name be known for everyone, and let the legends of this strife be told for our ancestors!");
            }
        }

        private static void Strife(BattleStats statsChar1, BattleStats statsChar2, AuthorizationResponse character)
        {
            Random random = new Random();
            Console.WriteLine($"Your turn, {character.Name}!");
            Console.WriteLine("1: Attack");
            Console.WriteLine("2: Ability use");
            Console.WriteLine("3: Kneel... but there shall be no way back.");
            int strifeCase = Console.ReadKey(true).KeyChar - 48;

            switch (strifeCase)
            {
                case 1:
                    int damage = random.Next(0, statsChar1.Damage);
                    int finalDamage = statsChar1.DealDamage(damage);
                    Console.WriteLine($"{character.Name} deals {finalDamage} damage!");
                    break;
                case 2:
                    switch (character.Class)
                    {
                        case "bloodletter":
                            damage = random.Next(0, statsChar1.Damage);
                            finalDamage = statsChar1.DealDamage(damage) + statsChar1.DealDamage(damage);
                            statsChar1.Health -= 2;
                            Console.WriteLine($"{character.Name} the bloodletter seeks for gore. They deal {finalDamage} damage, but receive 2 damage themself! That was rough!");
                            break;
                        case "priest":
                            int healing = random.Next(1, 5);
                            statsChar2.Health += healing - 1;
                            statsChar1.Health += healing;
                            Console.WriteLine($"{character.Name} whispers an old, but pleasant prayer. They heal themselves for {healing} HP... and it seems that the foe's HP have changed by {healing - 1} as well.");
                            break;
                        case "protector":
                            statsChar1.Defence++;
                            statsChar1.Health -= 5;
                            Console.WriteLine($"{character.Name} sacrifices his health in exchange of protection. Your defence have rose by 1, but the pain have made you remember about your own mortality (-5 HP).");
                            break;
                    }
                    break;
                case 3:
                    statsChar1.Health = 0;
                    Console.WriteLine($"There is no more horrible way to end the battle, but to surrender. Your name will be ashamed for ages, {character.Name}.");
                    break;
            }
        }

        private static BattleStats SetStandartStats(AuthorizationResponse character)
        {
            BattleStats stats = new BattleStats();
            switch (character.Class)
            {
                case "bloodletter":
                    stats.Damage = 8;
                    stats.Defence = 2;
                    stats.Health = 15;
                    break;
                case "priest":
                    stats.Damage = 6;
                    stats.Defence = 2;
                    stats.Health = 15;
                    break;
                case "protector":
                    stats.Damage = 6;
                    stats.Defence = 2;
                    stats.Health = 25;
                    break;
            }
            return stats;
        }
    }
}
