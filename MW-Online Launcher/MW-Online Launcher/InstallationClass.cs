using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MW_Online_Launcher
{
    class InstallationClass
    {
        public static string GetLoc()
        {
            return Thread.CurrentThread.CurrentUICulture.ToString();
        }
        public static void CloseAndOpen(Form close, Form open)
        {
            close.Hide();
            open.ShowDialog();
        }

        public static void Start()
        {
            new FirstRunForm().ShowDialog();
            new Forms.InstallFinished().ShowDialog();
            new Form1().ShowDialog();
        }

        public static void DownloadFile(string uri, string file)
        {
            WebClient client = new WebClient();
            client.DownloadFile(uri, file);
            client.Dispose();
        }

        public static void Update()
        {
            if (File.Exists("MWOUpdater.exe")) File.Delete("MWOUpdater.exe");
            InstallationClass.DownloadFile("...", "MWOUpdater.exe");
            new Process() { StartInfo = new ProcessStartInfo() { FileName = "MWOUpdater.exe" } }.Start();
        }

        public static void CheckUpdate()
        {
            try
            {
                WebClient client = new WebClient();
                string ver = client.DownloadString("https://github.com/YaNet-Production/mwo_files/raw/master/g/latest.txt");
                client.Dispose();
                if (ver != System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
                {
                    string capt = "Update required!";
                    string text = "Novaya versuha blyad! SKOCHAT!!!???";
                    if (Thread.CurrentThread.CurrentUICulture.ToString() == "RU-ru")
                    {
                        capt = "Требуется обновление!";
                        text = "Чтобы играть дальше Вам необходимо обновить версию MW-Online. Желаете сделать это прямо сейчас?\n\nВнимание: Игра и лаунчер будут автоматически закрыты.";
                    }
                    if (MessageBox.Show(text, capt, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Update();
                        Environment.Exit(0);
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
            }
            catch { }
        }

    }
}
