using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MWOUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            new Thread(work).Start();
        }

        void work()
        {
            try
            {
                foreach (var p in Process.GetProcesses())
                {
                    if (p.ProcessName.ToLower() == "mw-online launcher") p.Kill();
                    if (p.ProcessName.ToLower() == "speed") p.Kill();
                    if (p.ProcessName.ToLower() == "nfsscriptloader") p.Kill();
                }

                WebClient wc = new WebClient();
                string latest = wc.DownloadString("https://github.com/YaNet-Production/mwo_files/raw/master/g/latest.txt");
                string[] remove = wc.DownloadString("https://github.com/YaNet-Production/mwo_files/raw/master/g/remove.txt").Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string path in remove)
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch { }
                }

                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += wc_DownloadFileCompleted;
                wc.DownloadFileAsync(new Uri("https://github.com/YaNet-Production/mwo_files/raw/master/g/" + latest + "/MW-Online%20Launcher.exe"), "MW-Online Launcher.exe");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "MW-Online: Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Environment.Exit(0); }
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            new Process() { StartInfo = new ProcessStartInfo() { FileName = "MW-Online Launcher.exe", Arguments= "ForceInstall" } }.Start();
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BeginInvoke(new Action(delegate { progressBar1.Value = e.ProgressPercentage; }));
        }
    }
}
