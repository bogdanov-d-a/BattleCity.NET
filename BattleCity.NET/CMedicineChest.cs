using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BattleCity.NET
{
    class CMedicineChest : CLimitedLifeObject
    {
        private static Image m_ImageMedicineChest = Image.FromFile(@"Images\MedicineChest.png");

        private double m_x;
        private double m_y;
       
        private bool CoordinatesIsMatchTanks(List<CTank> Tanks, double tempX, double tempY)
        {
            for (int i = 0; i < Tanks.Count(); ++i)
            {
                if (Math.Abs(tempX - Tanks[i].GetX()) < CConstants.tankSize / 2 && Math.Abs(tempY - Tanks[i].GetY()) < CConstants.tankSize / 2)
                {
                    return true;
                }

            }
            return false;
        }

        private bool CoordinatesIsMatchMedChests(List<CMedicineChest> Chests, double tempX, double tempY)
        {
            for (int i = 0; i < Chests.Count(); ++i)
            {
                if (Math.Abs(tempX - Chests[i].GetX()) < CConstants.medChestsSize / 2 && Math.Abs(tempY - Chests[i].GetY()) < CConstants.medChestsSize / 2)
                {
                    return true;
                }

            }
            return false;
        }

        public CMedicineChest(List<CTank> Tanks, List<CMedicineChest> medChests) : base(CConstants.medChestLifetime)
        {
            double tempX = 0;
            double tempY = 0;
            do
            {
                tempX = CRandom.Next(20, 590);
                tempY = CRandom.Next(20, 440);
            } while (CoordinatesIsMatchTanks(Tanks, tempX, tempY));
            m_x = tempX;
            m_y = tempY;
        }

        public CMedicineChest() : base(CConstants.medChestLifetime)
        {
            m_x = CRandom.Next(1, 500);
            m_y = CRandom.Next(1, 500);
        }

        public double GetX()
        {
            return m_x;
        }

        public double GetY()
        {
            return m_y;
        }

        public void Draw(Graphics graph)
        {
            graph.DrawImage(m_ImageMedicineChest, (float)m_x, (float)m_y);
        }

        public bool CheckCollision(double x, double y)
        {
            if (!ObjectIsDead() && Math.Abs(m_x - x) < CConstants.tankSize / 2 && Math.Abs(m_y - y) < CConstants.tankSize / 2)
            {
                return true;
            }
            return false;
        }
    }
}
