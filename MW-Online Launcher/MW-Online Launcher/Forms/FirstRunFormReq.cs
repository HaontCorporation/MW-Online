using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MW_Online_Launcher.Forms
{
    public partial class FirstRunFormReq : Form
    {
        public bool Installing = false;
        public bool Installed = false;

        
        public static string[] nfsScriptFiles = {
                                                    "https://github.com/YaNet-Production/mwo_files/raw/master/g/0.1.1.0/NFSScript/NFSScript.dll",
                                                    "https://github.com/YaNet-Production/mwo_files/raw/master/g/0.1.1.0/NFSScript/NFSScript.xml",
                                                    "https://github.com/YaNet-Production/mwo_files/raw/master/g/0.1.1.0/NFSScript/NFSScriptLoader.exe"
                                                };

        public static string[] mwoModFiles = {
                                                 "https://github.com/YaNet-Production/mwo_files/raw/master/g/0.1.1.0/Client/MW_Online.dll"
                                             };
        
        public FirstRunFormReq()
        {
            InitializeComponent();
            CopyLabel.Text = "(C) MW-Online Dev Team";
            SetVisP(false);
            CheckDep();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "MW-Online installation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        public void ThrInstall()
        {
            BeginInvoke(new Action(delegate { SetVisP(true); }));
            if (Directory.Exists("temp"))
            {
                foreach(var f in Directory.EnumerateFiles("temp"))
                {
                    File.Delete(f);
                }
                Directory.Delete("temp");
            }

            Directory.CreateDirectory("temp");

            if (!depNFSSCript.Checked)
            {
                foreach (string url in nfsScriptFiles)
                {
                    try
                    {
                        string[] t = url.Split('/');
                        string name = t[t.Length - 1];
                        InstallationClass.DownloadFile(url, "temp\\" + name);
                        string tp = Path.Combine("temp", name);
                        File.Copy(tp, name);
                    }
                    catch { }
                }
                Directory.CreateDirectory("scripts");
            }
            BeginInvoke(new Action(delegate { CheckDep(); }));

            if (!depMWO.Checked)
            {
                foreach (string url in mwoModFiles)
                {
                    try
                    {
                        string[] t = url.Split('/');
                        string name = t[t.Length - 1];
                        InstallationClass.DownloadFile(url, "temp\\" + name);
                        string tp = Path.Combine("temp", name);
                        File.Copy(tp, "scripts\\" + name);
                    }
                    catch { }
                }
            }

            BeginInvoke(new Action(delegate { CheckDep(); }));
            Thread.Sleep(1000);
            BeginInvoke(new Action(delegate { this.Close(); }));
        }


        public void CheckDep()
        {
            if (File.Exists("speed.exe")) depNFSMW.Checked = true;
            else depNFSMW.Checked = false;

            if (File.Exists("NFSScriptLoader.exe")) depNFSSCript.Checked = true;
            else depNFSSCript.Checked = false;

            if (File.Exists("scripts\\MW_Online.dll")) depMWO.Checked = true;
            else depMWO.Checked = false;
        }


        public void SetVisP(bool t)
        {
            pLbl.Visible = t;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Installing)
            {
                button2.Enabled = false;
                button3.Enabled = false;
                new Thread(ThrInstall).Start();
            }
        }

        private void depMWO_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void depNFSSCript_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void FirstRunFormReq_Load(object sender, EventArgs e)
        {

        }

        private void depNFSMW_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void depNFSMW_Click(object sender, EventArgs e)
        {
            depNFSMW.Checked = !depNFSMW.Checked;
        }

        private void depNFSSCript_Click(object sender, EventArgs e)
        {
            depNFSSCript.Checked = !depNFSSCript.Checked;
            
        }

        private void depMWO_Click(object sender, EventArgs e)
        {
            depMWO.Checked = !depMWO.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InstallationClass.CloseAndOpen(this, new FirstRunForm2());
        }
    }
}
