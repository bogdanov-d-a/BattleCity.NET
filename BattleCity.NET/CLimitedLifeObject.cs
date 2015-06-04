using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.NET
{
    public class CLimitedLifeObject
    {
        private uint m_remaining;

        public CLimitedLifeObject(uint lifetime)
        {
            m_remaining = lifetime;
        }

        public void Update()
        {
            if (!ObjectIsDead())
            {
                --m_remaining;
            }
        }

        public bool ObjectIsDead()
        {
            return m_remaining == 0;
        }
    }
}
