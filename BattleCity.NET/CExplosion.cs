using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.NET
{
    class CExplosion : CLimitedLifeObject
    {
        private static readonly Image explosion = Image.FromFile(@"Images\explosion.png")
            .GetThumbnailImage(CConstants.tankSize, CConstants.tankSize, null, IntPtr.Zero);

        public CExplosion(int x, int y) : base(Convert.ToUInt32(CConstants.explodeTime))
        {
            m_x = x;
            m_y = y;
            FBattleScreen.PlaySound("explode");
        }
        public void Draw(Graphics graph)
        {
            graph.DrawImage(explosion, Convert.ToInt32(m_x - CConstants.tankSize / 2), Convert.ToInt32(m_y - CConstants.tankSize / 2));
        }

        private int m_x;
        private int m_y;
    }
}
