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
    public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            foreach (string fileName in openFileDialog.FileNames)
            {
                if (!ListHasItem(fileName))
                {
                    dllListBox.Items.Add(fileName);
                }
            }
        }

        private bool ListHasItem(string item)
        {
            foreach (string curItem in dllListBox.Items)
            {
                if (curItem == item)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
