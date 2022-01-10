namespace D2REditor.Forms
{
    partial class FormKnowledges
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.oKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tscbTxtList = new System.Windows.Forms.ToolStripComboBox();
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmeMaxSockets = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmeLevels = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCubemain = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvTxt = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 1097);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1644, 75);
            this.panel1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oKToolStripMenuItem,
            this.tscbTxtList,
            this.查询ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1644, 37);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // oKToolStripMenuItem
            // 
            this.oKToolStripMenuItem.Name = "oKToolStripMenuItem";
            this.oKToolStripMenuItem.Size = new System.Drawing.Size(104, 33);
            this.oKToolStripMenuItem.Text = "知者自知";
            // 
            // tscbTxtList
            // 
            this.tscbTxtList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbTxtList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tscbTxtList.Name = "tscbTxtList";
            this.tscbTxtList.Size = new System.Drawing.Size(200, 33);
            this.tscbTxtList.SelectedIndexChanged += new System.EventHandler(this.tscbTxtList_SelectedIndexChanged);
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmeMaxSockets,
            this.tsmeLevels,
            this.tsmiCubemain});
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(66, 33);
            this.查询ToolStripMenuItem.Text = "查询";
            // 
            // tsmeMaxSockets
            // 
            this.tsmeMaxSockets.Name = "tsmeMaxSockets";
            this.tsmeMaxSockets.Size = new System.Drawing.Size(190, 34);
            this.tsmeMaxSockets.Text = "最大孔数";
            this.tsmeMaxSockets.Click += new System.EventHandler(this.tsmeMaxSockets_Click);
            // 
            // tsmeLevels
            // 
            this.tsmeLevels.Name = "tsmeLevels";
            this.tsmeLevels.Size = new System.Drawing.Size(190, 34);
            this.tsmeLevels.Text = "场景等级";
            this.tsmeLevels.Click += new System.EventHandler(this.tsmeLevels_Click);
            // 
            // tsmiCubemain
            // 
            this.tsmiCubemain.Name = "tsmiCubemain";
            this.tsmiCubemain.Size = new System.Drawing.Size(190, 34);
            this.tsmiCubemain.Text = "合成公式";
            this.tsmiCubemain.Click += new System.EventHandler(this.tsmiCubemain_Click);
            // 
            // dgvTxt
            // 
            this.dgvTxt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTxt.Location = new System.Drawing.Point(0, 37);
            this.dgvTxt.Name = "dgvTxt";
            this.dgvTxt.ReadOnly = true;
            this.dgvTxt.RowHeadersWidth = 62;
            this.dgvTxt.RowTemplate.Height = 30;
            this.dgvTxt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTxt.Size = new System.Drawing.Size(1644, 1060);
            this.dgvTxt.TabIndex = 4;
            this.dgvTxt.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTxt_CellFormatting);
            this.dgvTxt.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvTxt_RowStateChanged);
            // 
            // FormKnowledges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1644, 1172);
            this.Controls.Add(this.dgvTxt);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormKnowledges";
            this.Text = "FormKnowledges";
            this.Load += new System.EventHandler(this.FormKnowledges_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTxt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem oKToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox tscbTxtList;
        private System.Windows.Forms.DataGridView dgvTxt;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmeMaxSockets;
        private System.Windows.Forms.ToolStripMenuItem tsmeLevels;
        private System.Windows.Forms.ToolStripMenuItem tsmiCubemain;
    }
}