namespace D2REditor.Forms
{
    partial class FormGenerateCoolThings
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
            this.btnCreateRuns = new System.Windows.Forms.Button();
            this.btnCreateKeys = new System.Windows.Forms.Button();
            this.btnCreateGems = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateRuns
            // 
            this.btnCreateRuns.Location = new System.Drawing.Point(50, 214);
            this.btnCreateRuns.Name = "btnCreateRuns";
            this.btnCreateRuns.Size = new System.Drawing.Size(577, 115);
            this.btnCreateRuns.TabIndex = 0;
            this.btnCreateRuns.Text = "生成33种符文的模板";
            this.btnCreateRuns.UseVisualStyleBackColor = true;
            this.btnCreateRuns.Click += new System.EventHandler(this.btnCreateRuns_Click);
            // 
            // btnCreateKeys
            // 
            this.btnCreateKeys.Location = new System.Drawing.Point(50, 382);
            this.btnCreateKeys.Name = "btnCreateKeys";
            this.btnCreateKeys.Size = new System.Drawing.Size(577, 115);
            this.btnCreateKeys.TabIndex = 1;
            this.btnCreateKeys.Text = "生成三种钥匙的模板";
            this.btnCreateKeys.UseVisualStyleBackColor = true;
            this.btnCreateKeys.Click += new System.EventHandler(this.btnCreateKeys_Click);
            // 
            // btnCreateGems
            // 
            this.btnCreateGems.Location = new System.Drawing.Point(50, 40);
            this.btnCreateGems.Name = "btnCreateGems";
            this.btnCreateGems.Size = new System.Drawing.Size(577, 115);
            this.btnCreateGems.TabIndex = 2;
            this.btnCreateGems.Text = "生成7种宝石的模板";
            this.btnCreateGems.UseVisualStyleBackColor = true;
            this.btnCreateGems.Click += new System.EventHandler(this.btnCreateGems_Click);
            // 
            // FormGenerateCoolThings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(672, 772);
            this.Controls.Add(this.btnCreateGems);
            this.Controls.Add(this.btnCreateKeys);
            this.Controls.Add(this.btnCreateRuns);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormGenerateCoolThings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGenerateAndSaveCoolThings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateRuns;
        private System.Windows.Forms.Button btnCreateKeys;
        private System.Windows.Forms.Button btnCreateGems;
    }
}