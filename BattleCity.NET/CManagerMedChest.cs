﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BattleCity.NET
{
    class CManagerMedChest
    {
        List<CMedicineChest> m_MedicineChests;
        List<CTank> m_Tanks;

        public CManagerMedChest(List<CTank> Tanks)
        {
            m_MedicineChests = new List<CMedicineChest>();
            m_MedicineChests.Add(new CMedicineChest());
            m_Tanks = Tanks;
        }

        public void Update()
        {
            foreach (CMedicineChest medChest in m_MedicineChests)
            {
                medChest.Update();
            }
        }

        public List<CMedicineChest> GetMedicineChests()
        {
            return m_MedicineChests;
        }

        public void AddMedChest()
        {
            m_MedicineChests.Add(new CMedicineChest(m_Tanks, m_MedicineChests));
        }

        public void DrawAllMedchests(Graphics e, List<CTank> Tanks)
        {
            List<int> tmp = new List<int>();
            for (int i = 0; i < m_MedicineChests.Count(); ++i)
            {
                for (int k = 0; k < Tanks.Count(); ++k)
                {
                    if (!m_MedicineChests[i].ObjectIsDead() && !m_MedicineChests[i].CheckCollision(Tanks[k].GetX(), Tanks[k].GetY()))
                    {
                        m_MedicineChests[i].Draw(e);
                    }
                    else if (!m_MedicineChests[i].ObjectIsDead() && m_MedicineChests[i].CheckCollision(Tanks[k].GetX(), Tanks[k].GetY()))
                    {
                        Tanks[k].SetHealth(10);
                        if (!tmp.Contains(i))
                            tmp.Add(i);
                    }
                    else
                    {
                        if (!tmp.Contains(i))
                            tmp.Add(i);
                       // break;

                    }
                }
            }
            //foreach (int i in tmp)
            for (int i = 0; i < tmp.Count; ++i)
            {
                m_MedicineChests.RemoveAt(tmp[i]);
            }
        }
    }
}
