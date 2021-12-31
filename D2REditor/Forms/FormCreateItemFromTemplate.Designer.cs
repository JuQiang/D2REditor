namespace D2REditor.Forms
{
    partial class FormCreateItemFromTemplate
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
            this.lbItems = new System.Windows.Forms.ListBox();
            this.btnPrevious = new D2REditor.Controls.ButtonEx();
            this.btnNext = new D2REditor.Controls.ButtonEx();
            this.SuspendLayout();
            // 
            // lbItems
            // 
            this.lbItems.BackColor = System.Drawing.Color.Black;
            this.lbItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbItems.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbItems.ForeColor = System.Drawing.Color.Black;
            this.lbItems.FormattingEnabled = true;
            this.lbItems.Location = new System.Drawing.Point(36, 75);
            this.lbItems.Name = "lbItems";
            this.lbItems.Size = new System.Drawing.Size(907, 713);
            this.lbItems.TabIndex = 7;
            this.lbItems.Visible = false;
            this.lbItems.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbItems_DrawItem);
            this.lbItems.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbItems_MeasureItem);
            this.lbItems.DoubleClick += new System.EventHandler(this.lbItems_DoubleClick);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.ForeColor = System.Drawing.Color.White;
            this.btnPrevious.ImageFile = "";
            this.btnPrevious.ImageFrames = 4;
            this.btnPrevious.Location = new System.Drawing.Point(573, 725);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(171, 56);
            this.btnPrevious.TabIndex = 9;
            this.btnPrevious.Text = "buttonEx2";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.ImageFile = "";
            this.btnNext.ImageFrames = 4;
            this.btnNext.Location = new System.Drawing.Point(763, 725);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(171, 56);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "buttonEx1";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // FormCreateItemFromTemplate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(975, 817);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lbItems);
            this.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormCreateItemFromTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormCreateItemFromTemplate";
            this.Load += new System.EventHandler(this.FormCreateItemFromTemplate_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lbItems;
        private Controls.ButtonEx btnNext;
        private Controls.ButtonEx btnPrevious;
    }
}