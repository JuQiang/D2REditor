namespace D2REditor.Forms
{
    partial class FormCreateNewCharactor
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
            this.lbCharacterList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbCharacterList
            // 
            this.lbCharacterList.BackColor = System.Drawing.Color.Black;
            this.lbCharacterList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbCharacterList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbCharacterList.FormattingEnabled = true;
            this.lbCharacterList.Location = new System.Drawing.Point(79, 78);
            this.lbCharacterList.Name = "lbCharacterList";
            this.lbCharacterList.Size = new System.Drawing.Size(430, 480);
            this.lbCharacterList.TabIndex = 0;
            this.lbCharacterList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbCharacterList_DrawItem);
            this.lbCharacterList.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbCharacterList_MeasureItem);
            this.lbCharacterList.SelectedIndexChanged += new System.EventHandler(this.lbCharacterList_SelectedIndexChanged);
            // 
            // FormCreateNewCharactor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(581, 753);
            this.Controls.Add(this.lbCharacterList);
            this.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCreateNewCharactor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "创建新的角色";
            this.Load += new System.EventHandler(this.CreateNewCharactorForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbCharacterList;
    }
}