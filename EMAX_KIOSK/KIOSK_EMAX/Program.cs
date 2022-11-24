using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace KIOSK_EMAX
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

            string sDBConnstring = Configurations.GetConfig("DBConnstring");

            if (string.IsNullOrWhiteSpace(sDBConnstring))
            {
                Change_Config config = new Change_Config();
                Application.Run(config);
                if (config.DialogResult == DialogResult.OK)
                    Application.Run(new MainMenu());
                else
                    Application.Exit();
            }
            else
            {
                Application.Run(new MainMenu());
            }
        }
    }
}
