namespace MW_Online_Launcher.Forms
{
    partial class FirstRunFormReq
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstRunFormReq));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.CopyLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.depNFSSCript = new System.Windows.Forms.CheckBox();
            this.depMWO = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.depNFSMW = new System.Windows.Forms.CheckBox();
            this.pLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Name = "panel1";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackgroundImage = global::MW_Online_Launcher.Properties.Resources.installing_updates;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // CopyLabel
            // 
            resources.ApplyResources(this.CopyLabel, "CopyLabel");
            this.CopyLabel.ForeColor = System.Drawing.Color.Gray;
            this.CopyLabel.Name = "CopyLabel";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // depNFSSCript
            // 
            resources.ApplyResources(this.depNFSSCript, "depNFSSCript");
            this.depNFSSCript.Name = "depNFSSCript";
            this.depNFSSCript.UseVisualStyleBackColor = true;
            this.depNFSSCript.CheckedChanged += new System.EventHandler(this.depNFSSCript_CheckedChanged);
            this.depNFSSCript.Click += new System.EventHandler(this.depNFSSCript_Click);
            // 
            // depMWO
            // 
            resources.ApplyResources(this.depMWO, "depMWO");
            this.depMWO.Name = "depMWO";
            this.depMWO.UseVisualStyleBackColor = true;
            this.depMWO.CheckedChanged += new System.EventHandler(this.depMWO_CheckedChanged);
            this.depMWO.Click += new System.EventHandler(this.depMWO_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // depNFSMW
            // 
            resources.ApplyResources(this.depNFSMW, "depNFSMW");
            this.depNFSMW.Name = "depNFSMW";
            this.depNFSMW.UseVisualStyleBackColor = true;
            this.depNFSMW.CheckedChanged += new System.EventHandler(this.depNFSMW_CheckedChanged);
            this.depNFSMW.Click += new System.EventHandler(this.depNFSMW_Click);
            // 
            // pLbl
            // 
            resources.ApplyResources(this.pLbl, "pLbl");
            this.pLbl.Name = "pLbl";
            // 
            // FirstRunFormReq
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pLbl);
            this.Controls.Add(this.depNFSMW);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.depMWO);
            this.Controls.Add(this.depNFSSCript);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.CopyLabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FirstRunFormReq";
            this.Load += new System.EventHandler(this.FirstRunFormReq_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label CopyLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox depNFSSCript;
        private System.Windows.Forms.CheckBox depMWO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox depNFSMW;
        private System.Windows.Forms.Label pLbl;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}