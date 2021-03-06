﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BattleCity.NET
{
    public class CMedicineChest : CLimitedLifeObject
    {
        private static Image m_ImageMedicineChest = Image.FromFile(@"Images\MedicineChest.png");
        private static Image m_ImageAntibonus = Image.FromFile(@"Images\Antibonus.png");

        private double m_x;
        private double m_y;
        public bool m_antibonus;

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

        private void SetRandomCoord()
        {
            m_x = CRandom.Next(20, 590);
            m_y = CRandom.Next(20, 440);
        }

        private void SetRandomType()
        {
            m_antibonus = (CRandom.Next(10) == 0);
        }

        public CMedicineChest(List<CTank> Tanks, List<CMedicineChest> medChests) : base(CConstants.medChestLifetime)
        {
            do
            {
                SetRandomCoord();
            }
            while (CoordinatesIsMatchTanks(Tanks, m_x, m_y));
            SetRandomType();
        }

        public CMedicineChest() : base(CConstants.medChestLifetime)
        {
            SetRandomCoord();
            SetRandomType();
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
            graph.DrawImage(m_antibonus ? m_ImageAntibonus : m_ImageMedicineChest, (float)m_x, (float)m_y);
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
