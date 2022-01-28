namespace Tools
{
    partial class MainForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tv = new System.Windows.Forms.TreeView();
            this.preview = new System.Windows.Forms.PictureBox();
            this.button5 = new System.Windows.Forms.Button();
            this.lb = new System.Windows.Forms.ListBox();
            this.button6 = new System.Windows.Forms.Button();
            this.cbFontList = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(784, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(282, 177);
            this.button1.TabIndex = 0;
            this.button1.Text = "转换所有sprite为png(abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1108, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(336, 180);
            this.button2.TabIndex = 1;
            this.button2.Text = "测试图片清晰度";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(51, 213);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(205, 28);
            this.textBox1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(51, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 45);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1830, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(286, 108);
            this.button3.TabIndex = 4;
            this.button3.Text = "dc6 2 png";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(177, 18);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(567, 168);
            this.button4.TabIndex = 5;
            this.button4.Text = "检索网络上的item pack文件";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tv
            // 
            this.tv.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tv.Location = new System.Drawing.Point(51, 273);
            this.tv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(1267, 886);
            this.tv.TabIndex = 6;
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            // 
            // preview
            // 
            this.preview.BackColor = System.Drawing.SystemColors.ControlDark;
            this.preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.preview.Location = new System.Drawing.Point(1346, 680);
            this.preview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(1526, 640);
            this.preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.preview.TabIndex = 7;
            this.preview.TabStop = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1467, 18);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(336, 180);
            this.button5.TabIndex = 8;
            this.button5.Text = "sprite";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lb
            // 
            this.lb.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb.FormattingEnabled = true;
            this.lb.ItemHeight = 33;
            this.lb.Location = new System.Drawing.Point(1346, 273);
            this.lb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(1524, 367);
            this.lb.TabIndex = 9;
            this.lb.SelectedIndexChanged += new System.EventHandler(this.lb_SelectedIndexChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1842, 148);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(274, 108);
            this.button6.TabIndex = 10;
            this.button6.Text = "论坛图片压缩";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // cbFontList
            // 
            this.cbFontList.FormattingEnabled = true;
            this.cbFontList.Location = new System.Drawing.Point(2143, 160);
            this.cbFontList.Name = "cbFontList";
            this.cbFontList.Size = new System.Drawing.Size(237, 26);
            this.cbFontList.TabIndex = 12;
            this.cbFontList.SelectedIndexChanged += new System.EventHandler(this.cbFontList_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(3002, 1370);
            this.Controls.Add(this.cbFontList);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.tv);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.PictureBox preview;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListBox lb;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ComboBox cbFontList;
    }
}

