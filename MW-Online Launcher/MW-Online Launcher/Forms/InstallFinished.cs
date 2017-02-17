using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MW_Online_Launcher.Forms
{
    public partial class InstallFinished : Form
    {
        public InstallFinished()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            InstallationClass.CloseAndOpen(this, new Form1());
        }
    }
}
