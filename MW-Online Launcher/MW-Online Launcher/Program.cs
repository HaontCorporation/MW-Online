using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MW_Online_Launcher
{
    static class Program
    {
        public static string[] Args;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Debugger.Launch();
            foreach (var p in Process.GetProcesses())
            {
                if (p.ProcessName == "MWOUpdater") p.Kill();
            }
            Args = args;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            foreach (string i in Args)
            {
                if (i.IndexOf("Update") >= 0)
                {
                    InstallationClass.Update();
                    return;
                }
                if (i.IndexOf("Download") >= 0)
                {
                    Application.Run(new Forms.DownForm());
                    return;
                }
                
            }
#if !DEBUG
            if (!File.Exists("speed.exe"))
            {
                MessageBox.Show("Place this file into NFS MW folder", "MW-Online Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
#endif
            if (!File.Exists("ru\\MW-Online Launcher.resources.dll") || !File.Exists("en\\MW-Online Launcher.resources.dll"))
            {
                if (!Directory.Exists("en")) Directory.CreateDirectory("en");
                if (!Directory.Exists("az")) Directory.CreateDirectory("az");
                if (!Directory.Exists("ru")) Directory.CreateDirectory("ru");


                InstallationClass.DownloadFile("https://github.com/YaNet-Production/mwo_files/raw/master/g/0.1.1.0/local/en.dll", "en\\MW-Online Launcher.resources.dll");
                InstallationClass.DownloadFile("https://github.com/YaNet-Production/mwo_files/raw/master/g/0.1.1.0/local/ru.dll", "ru\\MW-Online Launcher.resources.dll");
                InstallationClass.DownloadFile("https://github.com/YaNet-Production/mwo_files/raw/master/g/0.1.1.0/local/az.dll", "az\\MW-Online Launcher.resources.dll");

            }
            if (!File.Exists("NFSScriptLoader.exe") || !File.Exists("scripts\\MW_Online.dll") || !File.Exists("scripts\\mwonline.ini"))
            {
                InstallationClass.Start();
                Environment.Exit(0);
                return;
            }
            Application.Run(new Form1());
        }


       
    }
}
