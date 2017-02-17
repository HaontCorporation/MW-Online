
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MW_Online_Launcher
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            try
            {
                InstallationClass.CheckUpdate();
            }
            catch { }
            InitializeComponent();
            if (!File.Exists("scripts\\mwonline.ini"))
            {
                StreamWriter writer = new StreamWriter("scripts\\mwonline.ini");
                writer.Write("ip=37.113.97.234;port=5555;nickname=Player");
                writer.Flush();
                writer.Close();
                writer.Dispose();
            }

            try
            {
                StreamReader reader = new StreamReader("scripts\\mwonline.ini");
                string r = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                serverBox.Text = r.Split(';')[0].Split('=')[1] + ":" + r.Split(';')[1].Split('=')[1];
                nickBox.Text = r.Split(';')[2].Split('=')[1];
            }
            catch {
                StreamWriter writer = new StreamWriter("scripts\\mwonline.ini");
                writer.Write("ip=37.113.97.234;port=5555;nickname=Player");
                writer.Flush();
                writer.Close();
                writer.Dispose();

                StreamReader reader = new StreamReader("scripts\\mwonline.ini");
                string r = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                serverBox.Text = r.Split(';')[0].Split('=')[1] + ":" + r.Split(';')[1].Split('=')[1];
                nickBox.Text = r.Split(';')[1].Split('=')[1];
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter writer = new StreamWriter("scripts\\mwonline.ini");
                writer.Write("ip=" + serverBox.Text.Split(':')[0] + ";port=" + serverBox.Text.Split(':')[1] + ";nickname=" + nickBox.Text);
                writer.Flush();
                writer.Close();
                writer.Dispose();

                new Process() { StartInfo = new ProcessStartInfo() { FileName = "NFSScriptLoader.exe" } }.Start();
                while (true)
                {
                    if (Process.GetProcessesByName("speed").Length >= 1)
                    {
                        // todolol
                        break;
                    }
                }

                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "MW-Online: Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
