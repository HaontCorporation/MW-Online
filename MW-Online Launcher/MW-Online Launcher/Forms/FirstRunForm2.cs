using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MW_Online_Launcher.Forms
{
    public partial class FirstRunForm2 : Form
    {
        public FirstRunForm2()
        {
            InitializeComponent();
            CopyLabel.Text = "(C) MW-Online Dev Team";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Process() { StartInfo = new ProcessStartInfo() { FileName = "explorer.exe", Arguments = "http://coolweb.comlu.com/mwonline/screenshots/index.html" } }.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InstallationClass.CloseAndOpen(this, new FirstRunForm());
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (!File.Exists("scripts\\mwonline.ini") || !File.Exists("NFSScriptLoader.exe") || !File.Exists("scripts\\MW_Online.dll")) InstallationClass.CloseAndOpen(this, new FirstRunFormReq());
            else InstallationClass.CloseAndOpen(this, new Form1());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
