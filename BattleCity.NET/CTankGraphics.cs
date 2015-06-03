using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace BattleCity.NET
{
    class CTankGraphics
    {
        public readonly Image base_;
        public readonly Image turret;
        public readonly Image tank;

        public CTankGraphics(string image, int baseSize, int turretSize, int tankSize)
        {
            base_ = Image.FromFile(@"Images\Bases\" + image);
            turret = Image.FromFile(@"Images\Turrets\" + image);
            tank = Image.FromFile(@"Images\Tanks\" + image);

            Debug.Assert(baseSize > 0);
            base_ = base_.GetThumbnailImage(baseSize, baseSize, null, IntPtr.Zero);

            Debug.Assert(turretSize > 0);
            turret = turret.GetThumbnailImage(turretSize, turretSize, null, IntPtr.Zero);

            Debug.Assert(tankSize > 0);
            tank = tank.GetThumbnailImage(tankSize, tankSize, null, IntPtr.Zero);
        }
    }
}
