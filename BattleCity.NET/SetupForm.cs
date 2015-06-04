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

        private void cfgButton_Click(object sender, EventArgs e)
        {
            new ConfigForm().ShowDialog();
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            if (dllListBox.Items.Count > 4)
            {
                MessageBox.Show("Can't start game with more than 4 players");
                return;
            }

            if (dllListBox.Items.Count < 2)
            {
                MessageBox.Show("At least 2 players required for game");
                return;
            }

            new FBattleScreen(ObjectCollectionToList(dllListBox.Items), CConstants.disableInGamePb, CConstants.disableSidePb).ShowDialog();
        }

        private List<string> ObjectCollectionToList(ListBox.ObjectCollection collection)
        {
            List<string> result = new List<string>();

            foreach (string str in collection)
            {
                result.Add(str);
            }

            return result;
        }
    }
}
