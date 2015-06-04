using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BattleCity.NET
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new SetupForm());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            try
            {
                Directory.Delete("tmp", true);
            }
            catch (Exception)
            {}
        }
    }
}
