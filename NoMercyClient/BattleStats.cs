using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMercyClient
{
    public class BattleStats
    {
        public int Damage { get; set; }
        public int Defence { get; set; }
        public int Health { get; set; }
        public int DealDamage(int damage)
        {
            int finalDamage = damage - Defence;
            if (finalDamage > 0)
            {
                Health -= finalDamage;
                return finalDamage;
            }
            return 0;
        }
    }
}
