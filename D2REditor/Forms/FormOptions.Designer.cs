using D2REditor.Controls;

namespace D2REditor.Forms
{
    partial class FormOptions
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
            this.tcOptions = new System.Windows.Forms.TabControl();
            this.tpLanguage = new System.Windows.Forms.TabPage();
            this.btnOpenLanguageFile = new System.Windows.Forms.Button();
            this.lbLanguages = new System.Windows.Forms.ListBox();
            this.tpFile = new System.Windows.Forms.TabPage();
            this.labelCache = new System.Windows.Forms.Label();
            this.tbCacheLocation = new System.Windows.Forms.TextBox();
            this.labelGameFile = new System.Windows.Forms.Label();
            this.tbD2RFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseD2RFolder = new System.Windows.Forms.Button();
            this.btnCancel = new D2REditor.Controls.ButtonEx();
            this.btnOk = new D2REditor.Controls.ButtonEx();
            this.tcOptions.SuspendLayout();
            this.tpLanguage.SuspendLayout();
            this.tpFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcOptions
            // 
            this.tcOptions.Controls.Add(this.tpLanguage);
            this.tcOptions.Controls.Add(this.tpFile);
            this.tcOptions.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tcOptions.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tcOptions.ItemSize = new System.Drawing.Size(130, 30);
            this.tcOptions.Location = new System.Drawing.Point(54, 98);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(301, 333);
            this.tcOptions.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcOptions.TabIndex = 9;
            this.tcOptions.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tcOptions_DrawItem);
            // 
            // tpLanguage
            // 
            this.tpLanguage.BackColor = System.Drawing.Color.Black;
            this.tpLanguage.Controls.Add(this.btnOpenLanguageFile);
            this.tpLanguage.Controls.Add(this.lbLanguages);
            this.tpLanguage.Location = new System.Drawing.Point(4, 34);
            this.tpLanguage.Name = "tpLanguage";
            this.tpLanguage.Padding = new System.Windows.Forms.Padding(3);
            this.tpLanguage.Size = new System.Drawing.Size(293, 295);
            this.tpLanguage.TabIndex = 0;
            this.tpLanguage.Text = "tabPage1";
            // 
            // btnOpenLanguageFile
            // 
            this.btnOpenLanguageFile.Location = new System.Drawing.Point(56, 283);
            this.btnOpenLanguageFile.Name = "btnOpenLanguageFile";
            this.btnOpenLanguageFile.Size = new System.Drawing.Size(187, 32);
            this.btnOpenLanguageFile.TabIndex = 4;
            this.btnOpenLanguageFile.Text = "button1";
            this.btnOpenLanguageFile.UseVisualStyleBackColor = true;
            this.btnOpenLanguageFile.Click += new System.EventHandler(this.btnOpenLanguageFile_Click);
            // 
            // lbLanguages
            // 
            this.lbLanguages.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLanguages.FormattingEnabled = true;
            this.lbLanguages.ItemHeight = 12;
            this.lbLanguages.Items.AddRange(new object[] {
            "English",
            "简体中文",
            "繁体中文"});
            this.lbLanguages.Location = new System.Drawing.Point(5, 5);
            this.lbLanguages.Name = "lbLanguages";
            this.lbLanguages.Size = new System.Drawing.Size(286, 268);
            this.lbLanguages.TabIndex = 3;
            this.lbLanguages.SelectedIndexChanged += new System.EventHandler(this.lbLanguages_SelectedIndexChanged);
            // 
            // tpFile
            // 
            this.tpFile.BackColor = System.Drawing.Color.Black;
            this.tpFile.Controls.Add(this.labelCache);
            this.tpFile.Controls.Add(this.tbCacheLocation);
            this.tpFile.Controls.Add(this.labelGameFile);
            this.tpFile.Controls.Add(this.tbD2RFolder);
            this.tpFile.Controls.Add(this.btnBrowseD2RFolder);
            this.tpFile.Location = new System.Drawing.Point(4, 34);
            this.tpFile.Name = "tpFile";
            this.tpFile.Padding = new System.Windows.Forms.Padding(3);
            this.tpFile.Size = new System.Drawing.Size(293, 295);
            this.tpFile.TabIndex = 1;
            this.tpFile.Text = "tabPage2";
            // 
            // labelCache
            // 
            this.labelCache.AutoSize = true;
            this.labelCache.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCache.ForeColor = System.Drawing.Color.White;
            this.labelCache.Location = new System.Drawing.Point(8, 80);
            this.labelCache.Name = "labelCache";
            this.labelCache.Size = new System.Drawing.Size(41, 12);
            this.labelCache.TabIndex = 9;
            this.labelCache.Text = "label1";
            // 
            // tbCacheLocation
            // 
            this.tbCacheLocation.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCacheLocation.Location = new System.Drawing.Point(14, 116);
            this.tbCacheLocation.Name = "tbCacheLocation";
            this.tbCacheLocation.ReadOnly = true;
            this.tbCacheLocation.Size = new System.Drawing.Size(273, 21);
            this.tbCacheLocation.TabIndex = 8;
            // 
            // labelGameFile
            // 
            this.labelGameFile.AutoSize = true;
            this.labelGameFile.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelGameFile.ForeColor = System.Drawing.Color.White;
            this.labelGameFile.Location = new System.Drawing.Point(6, 3);
            this.labelGameFile.Name = "labelGameFile";
            this.labelGameFile.Size = new System.Drawing.Size(41, 12);
            this.labelGameFile.TabIndex = 7;
            this.labelGameFile.Text = "label1";
            // 
            // tbD2RFolder
            // 
            this.tbD2RFolder.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbD2RFolder.Location = new System.Drawing.Point(12, 39);
            this.tbD2RFolder.Name = "tbD2RFolder";
            this.tbD2RFolder.Size = new System.Drawing.Size(225, 21);
            this.tbD2RFolder.TabIndex = 6;
            // 
            // btnBrowseD2RFolder
            // 
            this.btnBrowseD2RFolder.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBrowseD2RFolder.Location = new System.Drawing.Point(245, 38);
            this.btnBrowseD2RFolder.Name = "btnBrowseD2RFolder";
            this.btnBrowseD2RFolder.Size = new System.Drawing.Size(42, 27);
            this.btnBrowseD2RFolder.TabIndex = 5;
            this.btnBrowseD2RFolder.Text = "...";
            this.btnBrowseD2RFolder.UseVisualStyleBackColor = true;
            this.btnBrowseD2RFolder.Click += new System.EventHandler(this.btnBrowseD2RFolder_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.ImageFile = "";
            this.btnCancel.ImageFrames = 4;
            this.btnCancel.Location = new System.Drawing.Point(233, 441);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 29);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.ImageFile = "";
            this.btnOk.ImageFrames = 4;
            this.btnOk.Location = new System.Drawing.Point(81, 441);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(93, 29);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FormOptions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(406, 527);
            this.ControlBox = false;
            this.Controls.Add(this.tcOptions);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormOptions";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            this.tcOptions.ResumeLayout(false);
            this.tpLanguage.ResumeLayout(false);
            this.tpFile.ResumeLayout(false);
            this.tpFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.ButtonEx btnOk;
        private Controls.ButtonEx btnCancel;
        private System.Windows.Forms.TabControl tcOptions;
        private System.Windows.Forms.TabPage tpLanguage;
        private System.Windows.Forms.ListBox lbLanguages;
        private System.Windows.Forms.TabPage tpFile;
        private System.Windows.Forms.TextBox tbD2RFolder;
        private System.Windows.Forms.Button btnOpenLanguageFile;
        private System.Windows.Forms.Label labelGameFile;
        private System.Windows.Forms.Label labelCache;
        private System.Windows.Forms.TextBox tbCacheLocation;
        private System.Windows.Forms.Button btnBrowseD2RFolder;
    }
}