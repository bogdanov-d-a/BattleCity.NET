using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BattleCity.NET
{
    class CConstants
    {
        public static int refreshTime = 20;
        public static int tankSize = 64;
        public static int turretSize = 80;
        public static int formWidth = 640;
        public static int formHeight = 480;
        public static int tankSpeed = refreshTime / 10;
        public static double baseRotationRate = 0.01 * refreshTime;
        public static double turretRotationRate = 0.01 * refreshTime;
        public static int reloadTime = 5000 / refreshTime;
        public static int shellSpeed = refreshTime;
        public static int shellSize = 16;
        public static int explodeTime = 200 / refreshTime;
        public static int medChestsSize = 20;
        public static int error = 0;
    }
}
