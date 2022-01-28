using D2REditor.Controls;

namespace D2REditor
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
            this.components = new System.ComponentModel.Container();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.pContainer = new System.Windows.Forms.Panel();
            this.pTop = new D2REditor.Controls.PanelEx();
            this.pBottom = new D2REditor.Controls.PanelEx();
            this.button2 = new System.Windows.Forms.Button();
            this.btnBooks = new D2REditor.Controls.ButtonEx();
            this.btnSave = new D2REditor.Controls.ButtonEx();
            this.btnExtra4 = new D2REditor.Controls.ButtonEx();
            this.btnExtra2 = new D2REditor.Controls.ButtonEx();
            this.btnExtra3 = new D2REditor.Controls.ButtonEx();
            this.btnExtra1 = new D2REditor.Controls.ButtonEx();
            this.btnQuestsWaypoints = new D2REditor.Controls.ButtonEx();
            this.btnItems = new D2REditor.Controls.ButtonEx();
            this.btnOptimizeAll = new D2REditor.Controls.ButtonEx();
            this.btnCharactorSkill = new D2REditor.Controls.ButtonEx();
            this.button1 = new System.Windows.Forms.Button();
            this.pBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tooltip
            // 
            this.tooltip.IsBalloon = true;
            // 
            // pContainer
            // 
            this.pContainer.BackColor = System.Drawing.Color.Black;
            this.pContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pContainer.Location = new System.Drawing.Point(0, 24);
            this.pContainer.Name = "pContainer";
            this.pContainer.Size = new System.Drawing.Size(1161, 753);
            this.pContainer.TabIndex = 27;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Black;
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(1161, 24);
            this.pTop.TabIndex = 25;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Black;
            this.pBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pBottom.Controls.Add(this.button2);
            this.pBottom.Controls.Add(this.btnBooks);
            this.pBottom.Controls.Add(this.btnSave);
            this.pBottom.Controls.Add(this.btnExtra4);
            this.pBottom.Controls.Add(this.btnExtra2);
            this.pBottom.Controls.Add(this.btnExtra3);
            this.pBottom.Controls.Add(this.btnExtra1);
            this.pBottom.Controls.Add(this.btnQuestsWaypoints);
            this.pBottom.Controls.Add(this.btnItems);
            this.pBottom.Controls.Add(this.btnOptimizeAll);
            this.pBottom.Controls.Add(this.btnCharactorSkill);
            this.pBottom.Controls.Add(this.button1);
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(0, 777);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(1161, 182);
            this.pBottom.TabIndex = 22;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(897, 792);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(235, 120);
            this.button2.TabIndex = 20;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnBooks
            // 
            this.btnBooks.BackColor = System.Drawing.Color.Transparent;
            this.btnBooks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBooks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBooks.FlatAppearance.BorderSize = 0;
            this.btnBooks.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBooks.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBooks.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBooks.ForeColor = System.Drawing.Color.White;
            this.btnBooks.ImageFile = "";
            this.btnBooks.ImageFrames = 4;
            this.btnBooks.Location = new System.Drawing.Point(623, 118);
            this.btnBooks.Name = "btnBooks";
            this.btnBooks.Size = new System.Drawing.Size(34, 34);
            this.btnBooks.TabIndex = 19;
            this.btnBooks.UseVisualStyleBackColor = false;
            this.btnBooks.Click += new System.EventHandler(this.btnBooks_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.ImageFile = "";
            this.btnSave.ImageFrames = 4;
            this.btnSave.Location = new System.Drawing.Point(513, 118);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(34, 34);
            this.btnSave.TabIndex = 18;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExtra4
            // 
            this.btnExtra4.BackColor = System.Drawing.Color.Transparent;
            this.btnExtra4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExtra4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExtra4.FlatAppearance.BorderSize = 0;
            this.btnExtra4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExtra4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExtra4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtra4.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExtra4.ForeColor = System.Drawing.Color.White;
            this.btnExtra4.ImageFile = "";
            this.btnExtra4.ImageFrames = 4;
            this.btnExtra4.Location = new System.Drawing.Point(852, 123);
            this.btnExtra4.Name = "btnExtra4";
            this.btnExtra4.Size = new System.Drawing.Size(25, 25);
            this.btnExtra4.TabIndex = 17;
            this.btnExtra4.Text = "99";
            this.btnExtra4.UseVisualStyleBackColor = false;
            // 
            // btnExtra2
            // 
            this.btnExtra2.BackColor = System.Drawing.Color.Transparent;
            this.btnExtra2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExtra2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExtra2.FlatAppearance.BorderSize = 0;
            this.btnExtra2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExtra2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExtra2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtra2.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExtra2.ForeColor = System.Drawing.Color.White;
            this.btnExtra2.ImageFile = "";
            this.btnExtra2.ImageFrames = 4;
            this.btnExtra2.Location = new System.Drawing.Point(752, 123);
            this.btnExtra2.Name = "btnExtra2";
            this.btnExtra2.Size = new System.Drawing.Size(25, 25);
            this.btnExtra2.TabIndex = 16;
            this.btnExtra2.Text = "99";
            this.btnExtra2.UseVisualStyleBackColor = false;
            // 
            // btnExtra3
            // 
            this.btnExtra3.BackColor = System.Drawing.Color.Transparent;
            this.btnExtra3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExtra3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExtra3.FlatAppearance.BorderSize = 0;
            this.btnExtra3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExtra3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExtra3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtra3.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExtra3.ForeColor = System.Drawing.Color.White;
            this.btnExtra3.ImageFile = "";
            this.btnExtra3.ImageFrames = 4;
            this.btnExtra3.Location = new System.Drawing.Point(802, 123);
            this.btnExtra3.Name = "btnExtra3";
            this.btnExtra3.Size = new System.Drawing.Size(25, 25);
            this.btnExtra3.TabIndex = 15;
            this.btnExtra3.Text = "99";
            this.btnExtra3.UseVisualStyleBackColor = false;
            // 
            // btnExtra1
            // 
            this.btnExtra1.BackColor = System.Drawing.Color.Transparent;
            this.btnExtra1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExtra1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExtra1.FlatAppearance.BorderSize = 0;
            this.btnExtra1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExtra1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExtra1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtra1.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExtra1.ForeColor = System.Drawing.Color.White;
            this.btnExtra1.ImageFile = "";
            this.btnExtra1.ImageFrames = 4;
            this.btnExtra1.Location = new System.Drawing.Point(702, 123);
            this.btnExtra1.Name = "btnExtra1";
            this.btnExtra1.Size = new System.Drawing.Size(25, 25);
            this.btnExtra1.TabIndex = 12;
            this.btnExtra1.Text = "99";
            this.btnExtra1.UseVisualStyleBackColor = false;
            // 
            // btnQuestsWaypoints
            // 
            this.btnQuestsWaypoints.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuestsWaypoints.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnQuestsWaypoints.FlatAppearance.BorderSize = 0;
            this.btnQuestsWaypoints.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnQuestsWaypoints.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnQuestsWaypoints.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuestsWaypoints.ForeColor = System.Drawing.Color.White;
            this.btnQuestsWaypoints.ImageFile = "";
            this.btnQuestsWaypoints.ImageFrames = 4;
            this.btnQuestsWaypoints.Location = new System.Drawing.Point(302, 134);
            this.btnQuestsWaypoints.Name = "btnQuestsWaypoints";
            this.btnQuestsWaypoints.Size = new System.Drawing.Size(24, 24);
            this.btnQuestsWaypoints.TabIndex = 8;
            this.btnQuestsWaypoints.Text = "2";
            this.btnQuestsWaypoints.UseVisualStyleBackColor = true;
            this.btnQuestsWaypoints.Click += new System.EventHandler(this.btnQuestsWaypoints_Click);
            // 
            // btnItems
            // 
            this.btnItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnItems.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnItems.FlatAppearance.BorderSize = 0;
            this.btnItems.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnItems.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItems.ForeColor = System.Drawing.Color.White;
            this.btnItems.ImageFile = "";
            this.btnItems.ImageFrames = 4;
            this.btnItems.Location = new System.Drawing.Point(326, 134);
            this.btnItems.Name = "btnItems";
            this.btnItems.Size = new System.Drawing.Size(24, 24);
            this.btnItems.TabIndex = 7;
            this.btnItems.Text = "3";
            this.btnItems.UseVisualStyleBackColor = true;
            this.btnItems.Click += new System.EventHandler(this.btnItems_Click);
            // 
            // btnOptimizeAll
            // 
            this.btnOptimizeAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOptimizeAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnOptimizeAll.FlatAppearance.BorderSize = 0;
            this.btnOptimizeAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOptimizeAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOptimizeAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptimizeAll.ForeColor = System.Drawing.Color.White;
            this.btnOptimizeAll.ImageFile = "";
            this.btnOptimizeAll.ImageFrames = 4;
            this.btnOptimizeAll.Location = new System.Drawing.Point(350, 134);
            this.btnOptimizeAll.Name = "btnOptimizeAll";
            this.btnOptimizeAll.Size = new System.Drawing.Size(24, 24);
            this.btnOptimizeAll.TabIndex = 6;
            this.btnOptimizeAll.Text = "4";
            this.btnOptimizeAll.UseVisualStyleBackColor = true;
            this.btnOptimizeAll.Click += new System.EventHandler(this.btnOptimizeAll_Click);
            // 
            // btnCharactorSkill
            // 
            this.btnCharactorSkill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCharactorSkill.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCharactorSkill.FlatAppearance.BorderSize = 0;
            this.btnCharactorSkill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCharactorSkill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCharactorSkill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCharactorSkill.ForeColor = System.Drawing.Color.White;
            this.btnCharactorSkill.ImageFile = "";
            this.btnCharactorSkill.ImageFrames = 4;
            this.btnCharactorSkill.Location = new System.Drawing.Point(278, 134);
            this.btnCharactorSkill.Name = "btnCharactorSkill";
            this.btnCharactorSkill.Size = new System.Drawing.Size(24, 24);
            this.btnCharactorSkill.TabIndex = 5;
            this.btnCharactorSkill.Text = "1";
            this.btnCharactorSkill.UseVisualStyleBackColor = true;
            this.btnCharactorSkill.Click += new System.EventHandler(this.btnCharactor_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1862, 1545);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(447, 148);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1161, 959);
            this.Controls.Add(this.pContainer);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font(Helper.CurrentFontFamily, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "暗黑2重置版离线存档修改器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.pBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private Controls.ButtonEx btnOptimizeAll;
        private Controls.ButtonEx btnItems;
        private Controls.ButtonEx btnQuestsWaypoints;
        private System.Windows.Forms.ToolTip tooltip;
        private Controls.ButtonEx btnExtra1;
        private Controls.ButtonEx btnExtra3;
        private Controls.ButtonEx btnExtra2;
        private Controls.ButtonEx btnExtra4;
        private Controls.ButtonEx btnSave;
        private Controls.ButtonEx btnBooks;
        private System.Windows.Forms.Button button2;
        private PanelEx pBottom;
        private Controls.ButtonEx btnCharactorSkill;
        private PanelEx pTop;
        private System.Windows.Forms.Panel pContainer;
    }
}