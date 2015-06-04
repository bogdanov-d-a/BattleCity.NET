using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace BattleCity.NET
{
    class CTournamentControl
    {
        private List<string> m_left;
        private List<string> m_dead = new List<string>();

        public CTournamentControl(List<string> dlls)
        {
            m_left = dlls;
        }

        public bool Active()
        {
            return (m_left.Count > 1);
        }

        public void Play()
        {
            List<string> players = GetPlayers();
            new FBattleScreen(players, CConstants.disableInGamePb, CConstants.disableSidePb, (players.Count > 3) ? 2 : 1, m_left, m_dead).ShowDialog();
        }

        private List<string> GetPlayers()
        {
            Debug.Assert(Active());
            List<string> result = new List<string>();

            while (result.Count < 4 && m_left.Count != 0)
            {
                result.Add(m_left[m_left.Count - 1]);
                m_left.RemoveAt(m_left.Count - 1);
            }

            return result;
        }

        public void ShowResult()
        {
            StringBuilder str = new StringBuilder();

            foreach (string name in m_left)
            {
                str.Append(name + "\n");
            }

            for (int i = m_dead.Count - 1; i >= 0; --i)
            {
                str.Append(m_dead[i] + "\n");
            }

            MessageBox.Show(str.ToString());
        }
    }
}
