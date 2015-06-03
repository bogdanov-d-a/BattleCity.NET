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
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            reloadTimeValue.Value = Convert.ToDecimal(CConstants.reloadTime);
            medRateValue.Value = Convert.ToDecimal(CConstants.medChestAppearRate);
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CConstants.reloadTime = Convert.ToInt32(reloadTimeValue.Value);
            CConstants.medChestAppearRate = Convert.ToInt32(medRateValue.Value);
        }
    }
}
