﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleCity.NET
{
    public partial class StatsForm : Form
    {
        private int m_heals = 0;
        private int m_shots = 0;
        private short m_hits = 0;
        private int m_dmgTaken = 0;
        private int m_hitsTaken = 0;

        public StatsForm()
        {
            InitializeComponent();
        }

        public void AddRecord(string name, int heals, int shots, short hits, int dmgTaken, int hitsTaken)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.Cells.Add(StringToText(name));
            row.Cells.Add(StringToText(Convert.ToString(heals)));
            row.Cells.Add(StringToText(Convert.ToString(shots)));
            row.Cells.Add(StringToText(Convert.ToString(hits)));
            row.Cells.Add(StringToText(Convert.ToString(dmgTaken)));
            row.Cells.Add(StringToText(Convert.ToString(hitsTaken)));
            dataGridView.Rows.Add(row);

            m_heals += heals;
            m_shots += shots;
            m_hits += hits;
            m_dmgTaken += dmgTaken;
            m_hitsTaken += hitsTaken;
        }

        public void AddRecord(CTank tank)
        {
            AddRecord(tank.m_name, tank.m_heals, tank.m_shots, tank.m_hits, tank.m_dmgTaken, tank.m_hitsTaken);
        }

        public void AddTotalRecord()
        {
            AddRecord("Total", m_heals, m_shots, m_hits, m_dmgTaken, m_hitsTaken);
        }

        private DataGridViewTextBoxCell StringToText(string str)
        {
            DataGridViewTextBoxCell result = new DataGridViewTextBoxCell();
            result.Value = str;
            return result;
        }
    }
}
