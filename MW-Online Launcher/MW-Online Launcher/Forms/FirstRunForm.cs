using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MW_Online_Launcher
{
    public partial class FirstRunForm : Form
    {
        bool b = false;
        public FirstRunForm()
        {
            InitializeComponent();
            UpdateLangBox();
        }

        private void LangBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!b)
            {
                if (LangBox.SelectedIndex == 0) Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                else if (LangBox.SelectedIndex == 1) Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
                this.Controls.Clear();
                InitializeComponent();

                b = true;

                UpdateLangBox();
            }
            b = false;
        }

        private void UpdateLangBox()
        {
            if (Thread.CurrentThread.CurrentUICulture.ToString() == "ru-RU")
            {
                LangBox.SelectedItem = LangBox.Items[1];
            }
            else
            {
                LangBox.SelectedItem = LangBox.Items[0];
            }
            CopyLabel.Text = "(C) MW-Online Dev Team";
        }

        private void FirstRunForm_ControlAdded(object sender, ControlEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = Thread.CurrentThread.CurrentUICulture.ToString();
            Properties.Settings.Default.Save();
            InstallationClass.CloseAndOpen(this, new Forms.FirstRunForm2());
        }

        private void CopyLabel_Click(object sender, EventArgs e)
        {

        }

    }
}
