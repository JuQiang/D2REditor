namespace D2REditor.Forms
{
    partial class FormOpenD2R
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOpenD2R));
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFile.BackgroundImage")));
            this.btnSelectFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectFile.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectFile.ForeColor = System.Drawing.Color.White;
            this.btnSelectFile.Location = new System.Drawing.Point(25, 961);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(750, 90);
            this.btnSelectFile.TabIndex = 7;
            this.btnSelectFile.Text = "打开存档";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            // 
            // lbFiles
            // 
            this.lbFiles.BackColor = System.Drawing.Color.IndianRed;
            this.lbFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbFiles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.ItemHeight = 24;
            this.lbFiles.Items.AddRange(new object[] {
            "aaa"});
            this.lbFiles.Location = new System.Drawing.Point(25, 5);
            this.lbFiles.Margin = new System.Windows.Forms.Padding(4);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(750, 938);
            this.lbFiles.TabIndex = 6;
            this.lbFiles.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbFiles_DrawItem);
            this.lbFiles.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbFiles_MeasureItem);
            // 
            // FormOpenD2R
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 1077);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.lbFiles);
            this.Name = "FormOpenD2R";
            this.Text = "FormOpenD2R";
            this.Load += new System.EventHandler(this.FormOpenD2R_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.ListBox lbFiles;
    }
}