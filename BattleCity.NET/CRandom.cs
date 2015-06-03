using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.NET
{
    class CRandom
    {
        private static Random instance = new Random();

        public static int Next(int maxValue)
        {
            return instance.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return instance.Next(minValue, maxValue);
        }
    }
}
