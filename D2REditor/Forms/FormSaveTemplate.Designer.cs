namespace D2REditor.Forms
{
    partial class FormSaveTemplate
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvLocation = new System.Windows.Forms.TreeView();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.btnSaveTemplate = new System.Windows.Forms.Button();
            this.tbItemDescription = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvLocation);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbItemDescription);
            this.splitContainer1.Panel2.Controls.Add(this.pbPreview);
            this.splitContainer1.Panel2.Controls.Add(this.btnSaveTemplate);
            this.splitContainer1.Size = new System.Drawing.Size(2393, 1447);
            this.splitContainer1.SplitterDistance = 796;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvLocation
            // 
            this.tvLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvLocation.HideSelection = false;
            this.tvLocation.Location = new System.Drawing.Point(0, 0);
            this.tvLocation.Name = "tvLocation";
            this.tvLocation.Size = new System.Drawing.Size(796, 1447);
            this.tvLocation.TabIndex = 0;
            this.tvLocation.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLocation_AfterSelect);
            // 
            // pbPreview
            // 
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbPreview.Location = new System.Drawing.Point(1261, 0);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(333, 1245);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPreview.TabIndex = 2;
            this.pbPreview.TabStop = false;
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSaveTemplate.Location = new System.Drawing.Point(0, 1245);
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size(1594, 202);
            this.btnSaveTemplate.TabIndex = 0;
            this.btnSaveTemplate.Text = "保存为模板……";
            this.btnSaveTemplate.UseVisualStyleBackColor = true;
            this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
            // 
            // tbItemDescription
            // 
            this.tbItemDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbItemDescription.Location = new System.Drawing.Point(0, 0);
            this.tbItemDescription.Multiline = true;
            this.tbItemDescription.Name = "tbItemDescription";
            this.tbItemDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbItemDescription.Size = new System.Drawing.Size(1261, 1245);
            this.tbItemDescription.TabIndex = 3;
            // 
            // FormSaveTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2393, 1447);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormSaveTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSaveTemplate";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvLocation;
        private System.Windows.Forms.Button btnSaveTemplate;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.TextBox tbItemDescription;
    }
}