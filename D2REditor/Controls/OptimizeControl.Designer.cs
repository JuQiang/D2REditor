namespace D2REditor.Controls
{
    partial class OptimizeControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbAllMoney = new D2REditor.Controls.CheckBoxEx();
            this.btnModify = new D2REditor.Controls.ButtonEx();
            this.cbSkill20 = new D2REditor.Controls.CheckBoxEx();
            this.cbAllWays = new D2REditor.Controls.CheckBoxEx();
            this.CbAllQuests = new D2REditor.Controls.CheckBoxEx();
            this.cbLevel99 = new D2REditor.Controls.CheckBoxEx();
            this.SuspendLayout();
            // 
            // cbAllMoney
            // 
            this.cbAllMoney.AutoSize = true;
            this.cbAllMoney.Checked = true;
            this.cbAllMoney.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbAllMoney.ForeColor = System.Drawing.Color.White;
            this.cbAllMoney.Location = new System.Drawing.Point(122, 272);
            this.cbAllMoney.Name = "cbAllMoney";
            this.cbAllMoney.Size = new System.Drawing.Size(279, 56);
            this.cbAllMoney.TabIndex = 11;
            this.cbAllMoney.Text = "最多钱";
            // 
            // btnModify
            // 
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModify.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnModify.FlatAppearance.BorderSize = 0;
            this.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnModify.ForeColor = System.Drawing.Color.White;
            this.btnModify.ImageFile = "";
            this.btnModify.ImageFrames = 4;
            this.btnModify.Location = new System.Drawing.Point(287, 444);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(246, 25);
            this.btnModify.TabIndex = 10;
            this.btnModify.Text = "一键修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // cbSkill20
            // 
            this.cbSkill20.AutoSize = true;
            this.cbSkill20.Checked = true;
            this.cbSkill20.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSkill20.ForeColor = System.Drawing.Color.White;
            this.cbSkill20.Location = new System.Drawing.Point(416, 99);
            this.cbSkill20.Name = "cbSkill20";
            this.cbSkill20.Size = new System.Drawing.Size(279, 56);
            this.cbSkill20.TabIndex = 9;
            this.cbSkill20.Text = "所有技能都到20级";
            // 
            // cbAllWays
            // 
            this.cbAllWays.AutoSize = true;
            this.cbAllWays.Checked = false;
            this.cbAllWays.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbAllWays.ForeColor = System.Drawing.Color.White;
            this.cbAllWays.Location = new System.Drawing.Point(416, 181);
            this.cbAllWays.Name = "cbAllWays";
            this.cbAllWays.Size = new System.Drawing.Size(279, 56);
            this.cbAllWays.TabIndex = 8;
            this.cbAllWays.Text = "打通所有传送点";
            // 
            // CbAllQuests
            // 
            this.CbAllQuests.AutoSize = true;
            this.CbAllQuests.Checked = false;
            this.CbAllQuests.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CbAllQuests.ForeColor = System.Drawing.Color.White;
            this.CbAllQuests.Location = new System.Drawing.Point(122, 181);
            this.CbAllQuests.Name = "CbAllQuests";
            this.CbAllQuests.Size = new System.Drawing.Size(279, 56);
            this.CbAllQuests.TabIndex = 7;
            this.CbAllQuests.Text = "完成所有任务";
            // 
            // cbLevel99
            // 
            this.cbLevel99.AutoSize = true;
            this.cbLevel99.Checked = true;
            this.cbLevel99.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLevel99.ForeColor = System.Drawing.Color.White;
            this.cbLevel99.Location = new System.Drawing.Point(122, 99);
            this.cbLevel99.Name = "cbLevel99";
            this.cbLevel99.Size = new System.Drawing.Size(279, 56);
            this.cbLevel99.TabIndex = 4;
            this.cbLevel99.Text = "直升到99级";
            // 
            // OptimizeControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.cbAllMoney);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.cbSkill20);
            this.Controls.Add(this.cbAllWays);
            this.Controls.Add(this.CbAllQuests);
            this.Controls.Add(this.cbLevel99);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "OptimizeControl";
            this.Size = new System.Drawing.Size(1162, 753);
            this.Load += new System.EventHandler(this.OptimizeControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OptimizeControl_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CheckBoxEx cbLevel99;
        private CheckBoxEx CbAllQuests;
        private CheckBoxEx cbAllWays;
        private CheckBoxEx cbSkill20;
        private ButtonEx btnModify;
        private CheckBoxEx cbAllMoney;
    }
}
