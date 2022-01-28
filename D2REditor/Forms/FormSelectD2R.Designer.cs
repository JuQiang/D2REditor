using D2REditor.Controls;

namespace D2REditor.Forms
{
    partial class FormSelectD2R
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
            this.panelCenter = new System.Windows.Forms.Panel();
            this.btnEdit = new D2REditor.Controls.ButtonEx();
            this.pbRightPanel = new D2REditor.Controls.PanelEx();
            this.lbCharactors = new D2REditor.Controls.ListBoxEx();
            this.btnRefresh = new D2REditor.Controls.ButtonEx();
            this.btnCreateNew = new D2REditor.Controls.ButtonEx();
            this.btnDelete = new D2REditor.Controls.ButtonEx();
            this.pbLeftPanel = new D2REditor.Controls.PanelEx();
            this.btnAbout = new D2REditor.Controls.ButtonEx();
            this.btnOptions = new D2REditor.Controls.ButtonEx();
            this.panelCenter.SuspendLayout();
            this.pbRightPanel.SuspendLayout();
            this.pbLeftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCenter
            // 
            this.panelCenter.BackColor = System.Drawing.Color.RosyBrown;
            this.panelCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelCenter.Controls.Add(this.btnEdit);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(296, 0);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(466, 632);
            this.panelCenter.TabIndex = 22;
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.ImageFile = "";
            this.btnEdit.ImageFrames = 4;
            this.btnEdit.Location = new System.Drawing.Point(149, 554);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(184, 44);
            this.btnEdit.TabIndex = 15;
            this.btnEdit.Text = "开始修改";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // pbRightPanel
            // 
            this.pbRightPanel.BackColor = System.Drawing.Color.DarkGray;
            this.pbRightPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbRightPanel.Controls.Add(this.lbCharactors);
            this.pbRightPanel.Controls.Add(this.btnRefresh);
            this.pbRightPanel.Controls.Add(this.btnCreateNew);
            this.pbRightPanel.Controls.Add(this.btnDelete);
            this.pbRightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbRightPanel.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 10.5F);
            this.pbRightPanel.Location = new System.Drawing.Point(762, 0);
            this.pbRightPanel.Name = "pbRightPanel";
            this.pbRightPanel.Size = new System.Drawing.Size(292, 632);
            this.pbRightPanel.TabIndex = 10;
            // 
            // lbCharactors
            // 
            this.lbCharactors.FormattingEnabled = true;
            this.lbCharactors.ItemHeight = 21;
            this.lbCharactors.Location = new System.Drawing.Point(17, 60);
            this.lbCharactors.Name = "lbCharactors";
            this.lbCharactors.Size = new System.Drawing.Size(252, 508);
            this.lbCharactors.TabIndex = 15;
            this.lbCharactors.SelectedIndexChanged += new System.EventHandler(this.lbCharactors_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 16F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.ImageFile = "";
            this.btnRefresh.ImageFrames = 4;
            this.btnRefresh.Location = new System.Drawing.Point(217, 574);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 32);
            this.btnRefresh.TabIndex = 18;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCreateNew
            // 
            this.btnCreateNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCreateNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateNew.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F);
            this.btnCreateNew.ForeColor = System.Drawing.Color.White;
            this.btnCreateNew.ImageFile = "";
            this.btnCreateNew.ImageFrames = 4;
            this.btnCreateNew.Location = new System.Drawing.Point(73, 573);
            this.btnCreateNew.Name = "btnCreateNew";
            this.btnCreateNew.Size = new System.Drawing.Size(138, 33);
            this.btnCreateNew.TabIndex = 19;
            this.btnCreateNew.Text = "创建新角色";
            this.btnCreateNew.UseVisualStyleBackColor = true;
            this.btnCreateNew.Click += new System.EventHandler(this.BtnCreateNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 16F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.ImageFile = "";
            this.btnDelete.ImageFrames = 4;
            this.btnDelete.Location = new System.Drawing.Point(35, 573);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 32);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pbLeftPanel
            // 
            this.pbLeftPanel.BackColor = System.Drawing.Color.White;
            this.pbLeftPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLeftPanel.Controls.Add(this.btnAbout);
            this.pbLeftPanel.Controls.Add(this.btnOptions);
            this.pbLeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.pbLeftPanel.Name = "pbLeftPanel";
            this.pbLeftPanel.Size = new System.Drawing.Size(296, 632);
            this.pbLeftPanel.TabIndex = 5;
            this.pbLeftPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.pbLeftPanel_Paint);
            // 
            // btnAbout
            // 
            this.btnAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnAbout.ForeColor = System.Drawing.Color.White;
            this.btnAbout.ImageFile = "";
            this.btnAbout.ImageFrames = 4;
            this.btnAbout.Location = new System.Drawing.Point(58, 416);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(184, 43);
            this.btnAbout.TabIndex = 12;
            this.btnAbout.Text = "关于";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptions.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F, System.Drawing.FontStyle.Bold);
            this.btnOptions.ForeColor = System.Drawing.Color.White;
            this.btnOptions.ImageFile = "";
            this.btnOptions.ImageFrames = 4;
            this.btnOptions.Location = new System.Drawing.Point(58, 355);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(184, 44);
            this.btnOptions.TabIndex = 11;
            this.btnOptions.Text = "设定";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // FormSelectD2R
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1054, 632);
            this.ControlBox = false;
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.pbRightPanel);
            this.Controls.Add(this.pbLeftPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormSelectD2R";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormSelectD2R_Load);
            this.SizeChanged += new System.EventHandler(this.FormSelectD2R_SizeChanged);
            this.panelCenter.ResumeLayout(false);
            this.pbRightPanel.ResumeLayout(false);
            this.pbLeftPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private PanelEx pbLeftPanel;
        private PanelEx pbRightPanel;
        private Controls.ButtonEx btnOptions;
        private Controls.ButtonEx btnAbout;
        private Controls.ListBoxEx lbCharactors;
        private Controls.ButtonEx btnDelete;
        private Controls.ButtonEx btnRefresh;
        private Controls.ButtonEx btnCreateNew;
        private System.Windows.Forms.Panel panelCenter;
        private ButtonEx btnEdit;
    }
}