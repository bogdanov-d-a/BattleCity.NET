using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BattleCity.NET
{
    class CTournamentControl
    {
        private List<string> m_left;

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

            if (players.Count > 3)
            {
                List<string> winners = new List<string>();
                new FBattleScreen(players, CConstants.disableInGamePb, CConstants.disableSidePb, winners).ShowDialog();

                foreach (string winner in winners)
                {
                    m_left.Add(winner);
                }
            }
            else
            {
                new FBattleScreen(players, CConstants.disableInGamePb, CConstants.disableSidePb).ShowDialog();
            }
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
    }
}
