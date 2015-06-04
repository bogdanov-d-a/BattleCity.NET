using System;
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
        }

        private DataGridViewTextBoxCell StringToText(string str)
        {
            DataGridViewTextBoxCell result = new DataGridViewTextBoxCell();
            result.Value = str;
            return result;
        }
    }
}
