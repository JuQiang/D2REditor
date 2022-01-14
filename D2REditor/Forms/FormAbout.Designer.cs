namespace D2REditor.Forms
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.link6 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.link5 = new System.Windows.Forms.LinkLabel();
            this.link4 = new System.Windows.Forms.LinkLabel();
            this.link3 = new System.Windows.Forms.LinkLabel();
            this.link2 = new System.Windows.Forms.LinkLabel();
            this.link1 = new System.Windows.Forms.LinkLabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbUpdateLog = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(411, 510);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.link6);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(403, 484);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "联系我";
            // 
            // link6
            // 
            this.link6.ForeColor = System.Drawing.Color.Black;
            this.link6.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.link6.Location = new System.Drawing.Point(6, 442);
            this.link6.Name = "link6";
            this.link6.Size = new System.Drawing.Size(391, 24);
            this.link6.TabIndex = 3;
            this.link6.Text = "访问我的Github";
            this.link6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(-4, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(407, 39);
            this.label1.TabIndex = 2;
            this.label1.Text = "扫我微信";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.link5);
            this.tabPage2.Controls.Add(this.link4);
            this.tabPage2.Controls.Add(this.link3);
            this.tabPage2.Controls.Add(this.link2);
            this.tabPage2.Controls.Add(this.link1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(403, 484);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "感谢大家";
            // 
            // link5
            // 
            this.link5.ForeColor = System.Drawing.Color.Black;
            this.link5.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.link5.Location = new System.Drawing.Point(16, 149);
            this.link5.Name = "link5";
            this.link5.Size = new System.Drawing.Size(379, 24);
            this.link5.TabIndex = 11;
            this.link5.Text = "Sprit Format";
            // 
            // link4
            // 
            this.link4.ForeColor = System.Drawing.Color.Black;
            this.link4.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.link4.Location = new System.Drawing.Point(16, 110);
            this.link4.Name = "link4";
            this.link4.Size = new System.Drawing.Size(379, 24);
            this.link4.TabIndex = 9;
            this.link4.Text = "D6C Format";
            // 
            // link3
            // 
            this.link3.ForeColor = System.Drawing.Color.Black;
            this.link3.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.link3.Location = new System.Drawing.Point(16, 71);
            this.link3.Name = "link3";
            this.link3.Size = new System.Drawing.Size(379, 24);
            this.link3.TabIndex = 7;
            this.link3.Text = "Item Format";
            // 
            // link2
            // 
            this.link2.ForeColor = System.Drawing.Color.Black;
            this.link2.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.link2.Location = new System.Drawing.Point(16, 36);
            this.link2.Name = "link2";
            this.link2.Size = new System.Drawing.Size(379, 24);
            this.link2.TabIndex = 5;
            this.link2.Text = "File Format";
            // 
            // link1
            // 
            this.link1.ForeColor = System.Drawing.Color.Black;
            this.link1.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.link1.Location = new System.Drawing.Point(16, 3);
            this.link1.Name = "link1";
            this.link1.Size = new System.Drawing.Size(379, 24);
            this.link1.TabIndex = 1;
            this.link1.Text = "D2SLib";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbUpdateLog);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(403, 484);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "更新日志";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbUpdateLog
            // 
            this.tbUpdateLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbUpdateLog.Location = new System.Drawing.Point(0, 0);
            this.tbUpdateLog.Multiline = true;
            this.tbUpdateLog.Name = "tbUpdateLog";
            this.tbUpdateLog.Size = new System.Drawing.Size(403, 484);
            this.tbUpdateLog.TabIndex = 0;
            // 
            // FormAbout
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(411, 510);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel link5;
        private System.Windows.Forms.LinkLabel link4;
        private System.Windows.Forms.LinkLabel link3;
        private System.Windows.Forms.LinkLabel link2;
        private System.Windows.Forms.LinkLabel link1;
        private System.Windows.Forms.LinkLabel link6;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbUpdateLog;
    }
}