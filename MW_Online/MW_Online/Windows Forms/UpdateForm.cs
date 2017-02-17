using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MW_Online
{
    public partial class UpdateForm : Form
    {
        private string Lang = "EN";
        public UpdateForm()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            UpdateLang();
        }

        

        public void UpdateLang()
        {
            textBox1.Clear();
            if (Lang == "EN")
            {
                this.Text = Properties.Resources.TEXT_UPDATEFORM_CAPTION_EN;
                textBox1.AppendText(Properties.Resources.TEXT_OUTDATED_EN);
                uButton.Text = Properties.Resources.TEXT_UPDATE_BUTTON_EN;
                dButton.Text = Properties.Resources.TEXT_DOWNLOAD_BUTTON_EN;
                cButton.Text = Properties.Resources.TEXT_CANCEL_BUTTON_EN;
            }
            else if (Lang == "RU")
            {
                this.Text = Properties.Resources.TEXT_UPDATEFORM_CAPTION_RU;
                textBox1.AppendText(Properties.Resources.TEXT_OUTDATED_RU);
                uButton.Text = Properties.Resources.TEXT_UPDATE_BUTTON_RU;
                dButton.Text = Properties.Resources.TEXT_DOWNLOAD_BUTTON_RU;
                cButton.Text = Properties.Resources.TEXT_CANCEL_BUTTON_RU;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Lang = linkLabel2.Text;
            UpdateLang();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Lang = linkLabel1.Text;
            UpdateLang();
        }
    }
}
