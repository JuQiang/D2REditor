namespace D2REditor.Forms
{
    partial class FormEditItem
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSaveAsTemplate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dgvItemStatCost = new System.Windows.Forms.DataGridView();
            this.colDescFunc = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colDescFuncValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbItems = new System.Windows.Forms.ComboBox();
            this.btnDeleteStat = new System.Windows.Forms.Button();
            this.cbSubTypes = new System.Windows.Forms.ComboBox();
            this.cbIsEthereal = new System.Windows.Forms.CheckBox();
            this.cbTypes = new System.Windows.Forms.ComboBox();
            this.btnAddStat = new System.Windows.Forms.Button();
            this.cbQuality = new System.Windows.Forms.ComboBox();
            this.cbSockets = new System.Windows.Forms.ComboBox();
            this.lbBasicDescription = new System.Windows.Forms.ListBox();
            this.pbItemPicture = new System.Windows.Forms.PictureBox();
            this.btnRestoreValues = new System.Windows.Forms.Button();
            this.btnMaxValues = new System.Windows.Forms.Button();
            this.cbWeight = new System.Windows.Forms.ComboBox();
            this.labelSockets = new System.Windows.Forms.Label();
            this.labelRunewordsCannotEdit = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemStatCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbItemPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveAsTemplate
            // 
            this.btnSaveAsTemplate.Location = new System.Drawing.Point(-235, 32);
            this.btnSaveAsTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveAsTemplate.Name = "btnSaveAsTemplate";
            this.btnSaveAsTemplate.Size = new System.Drawing.Size(151, 36);
            this.btnSaveAsTemplate.TabIndex = 14;
            this.btnSaveAsTemplate.Text = "保存为模板";
            this.btnSaveAsTemplate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveAsTemplate);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 575);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1048, 100);
            this.panel1.TabIndex = 13;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(813, 19);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(151, 60);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(639, 19);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(151, 60);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvItemStatCost
            // 
            this.dgvItemStatCost.ColumnHeadersHeight = 34;
            this.dgvItemStatCost.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDescFunc,
            this.colDescFuncValue,
            this.colValue,
            this.colMaxValue});
            this.dgvItemStatCost.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvItemStatCost.Location = new System.Drawing.Point(11, 177);
            this.dgvItemStatCost.Margin = new System.Windows.Forms.Padding(2);
            this.dgvItemStatCost.Name = "dgvItemStatCost";
            this.dgvItemStatCost.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvItemStatCost.RowTemplate.Height = 30;
            this.dgvItemStatCost.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvItemStatCost.Size = new System.Drawing.Size(953, 367);
            this.dgvItemStatCost.TabIndex = 48;
            this.dgvItemStatCost.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemStatCost_CellValueChanged);
            this.dgvItemStatCost.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvItemStatCost_CurrentCellDirtyStateChanged);
            this.dgvItemStatCost.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvItemStatCost_DataError);
            // 
            // colDescFunc
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.colDescFunc.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDescFunc.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colDescFunc.HeaderText = "高级属性(下拉更改)";
            this.colDescFunc.MinimumWidth = 8;
            this.colDescFunc.Name = "colDescFunc";
            this.colDescFunc.Width = 250;
            // 
            // colDescFuncValue
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colDescFuncValue.DefaultCellStyle = dataGridViewCellStyle5;
            this.colDescFuncValue.HeaderText = "高级属性值";
            this.colDescFuncValue.MinimumWidth = 8;
            this.colDescFuncValue.Name = "colDescFuncValue";
            this.colDescFuncValue.ReadOnly = true;
            this.colDescFuncValue.Width = 250;
            // 
            // colValue
            // 
            this.colValue.HeaderText = "值";
            this.colValue.MinimumWidth = 8;
            this.colValue.Name = "colValue";
            this.colValue.Width = 150;
            // 
            // colMaxValue
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colMaxValue.DefaultCellStyle = dataGridViewCellStyle6;
            this.colMaxValue.HeaderText = "最大值";
            this.colMaxValue.MinimumWidth = 8;
            this.colMaxValue.Name = "colMaxValue";
            this.colMaxValue.ReadOnly = true;
            this.colMaxValue.Width = 150;
            // 
            // cbItems
            // 
            this.cbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItems.FormattingEnabled = true;
            this.cbItems.Location = new System.Drawing.Point(857, 69);
            this.cbItems.Margin = new System.Windows.Forms.Padding(2);
            this.cbItems.Name = "cbItems";
            this.cbItems.Size = new System.Drawing.Size(107, 32);
            this.cbItems.TabIndex = 43;
            this.cbItems.SelectedIndexChanged += new System.EventHandler(this.cbItems_SelectedIndexChanged);
            // 
            // btnDeleteStat
            // 
            this.btnDeleteStat.Location = new System.Drawing.Point(971, 237);
            this.btnDeleteStat.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteStat.Name = "btnDeleteStat";
            this.btnDeleteStat.Size = new System.Drawing.Size(70, 36);
            this.btnDeleteStat.TabIndex = 47;
            this.btnDeleteStat.Text = "-";
            this.btnDeleteStat.UseVisualStyleBackColor = true;
            this.btnDeleteStat.Click += new System.EventHandler(this.btnDeleteStat_Click);
            // 
            // cbSubTypes
            // 
            this.cbSubTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubTypes.Enabled = false;
            this.cbSubTypes.FormattingEnabled = true;
            this.cbSubTypes.Location = new System.Drawing.Point(725, 69);
            this.cbSubTypes.Margin = new System.Windows.Forms.Padding(2);
            this.cbSubTypes.Name = "cbSubTypes";
            this.cbSubTypes.Size = new System.Drawing.Size(119, 32);
            this.cbSubTypes.TabIndex = 42;
            // 
            // cbIsEthereal
            // 
            this.cbIsEthereal.AutoSize = true;
            this.cbIsEthereal.Location = new System.Drawing.Point(530, 18);
            this.cbIsEthereal.Margin = new System.Windows.Forms.Padding(2);
            this.cbIsEthereal.Name = "cbIsEthereal";
            this.cbIsEthereal.Size = new System.Drawing.Size(84, 28);
            this.cbIsEthereal.TabIndex = 40;
            this.cbIsEthereal.Text = "无形";
            this.cbIsEthereal.UseVisualStyleBackColor = true;
            // 
            // cbTypes
            // 
            this.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypes.Enabled = false;
            this.cbTypes.FormattingEnabled = true;
            this.cbTypes.Location = new System.Drawing.Point(628, 69);
            this.cbTypes.Margin = new System.Windows.Forms.Padding(2);
            this.cbTypes.Name = "cbTypes";
            this.cbTypes.Size = new System.Drawing.Size(78, 32);
            this.cbTypes.TabIndex = 41;
            // 
            // btnAddStat
            // 
            this.btnAddStat.Location = new System.Drawing.Point(971, 177);
            this.btnAddStat.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddStat.Name = "btnAddStat";
            this.btnAddStat.Size = new System.Drawing.Size(70, 36);
            this.btnAddStat.TabIndex = 46;
            this.btnAddStat.Text = "+";
            this.btnAddStat.UseVisualStyleBackColor = true;
            this.btnAddStat.Click += new System.EventHandler(this.btnAddStat_Click);
            // 
            // cbQuality
            // 
            this.cbQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuality.Enabled = false;
            this.cbQuality.FormattingEnabled = true;
            this.cbQuality.Location = new System.Drawing.Point(530, 69);
            this.cbQuality.Margin = new System.Windows.Forms.Padding(2);
            this.cbQuality.Name = "cbQuality";
            this.cbQuality.Size = new System.Drawing.Size(84, 32);
            this.cbQuality.TabIndex = 35;
            // 
            // cbSockets
            // 
            this.cbSockets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSockets.FormattingEnabled = true;
            this.cbSockets.Items.AddRange(new object[] {
            "无孔",
            "1孔",
            "2孔",
            "3孔",
            "4孔",
            "5孔",
            "6孔"});
            this.cbSockets.Location = new System.Drawing.Point(723, 14);
            this.cbSockets.Name = "cbSockets";
            this.cbSockets.Size = new System.Drawing.Size(121, 32);
            this.cbSockets.TabIndex = 45;
            // 
            // lbBasicDescription
            // 
            this.lbBasicDescription.FormattingEnabled = true;
            this.lbBasicDescription.ItemHeight = 24;
            this.lbBasicDescription.Location = new System.Drawing.Point(122, 11);
            this.lbBasicDescription.Margin = new System.Windows.Forms.Padding(2);
            this.lbBasicDescription.Name = "lbBasicDescription";
            this.lbBasicDescription.Size = new System.Drawing.Size(373, 148);
            this.lbBasicDescription.TabIndex = 36;
            // 
            // pbItemPicture
            // 
            this.pbItemPicture.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbItemPicture.Location = new System.Drawing.Point(11, 11);
            this.pbItemPicture.Margin = new System.Windows.Forms.Padding(2);
            this.pbItemPicture.Name = "pbItemPicture";
            this.pbItemPicture.Size = new System.Drawing.Size(98, 147);
            this.pbItemPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbItemPicture.TabIndex = 34;
            this.pbItemPicture.TabStop = false;
            // 
            // btnRestoreValues
            // 
            this.btnRestoreValues.Location = new System.Drawing.Point(971, 358);
            this.btnRestoreValues.Margin = new System.Windows.Forms.Padding(2);
            this.btnRestoreValues.Name = "btnRestoreValues";
            this.btnRestoreValues.Size = new System.Drawing.Size(70, 36);
            this.btnRestoreValues.TabIndex = 50;
            this.btnRestoreValues.Text = "原始";
            this.btnRestoreValues.UseVisualStyleBackColor = true;
            this.btnRestoreValues.Visible = false;
            this.btnRestoreValues.Click += new System.EventHandler(this.btnRestoreValues_Click);
            // 
            // btnMaxValues
            // 
            this.btnMaxValues.Location = new System.Drawing.Point(971, 298);
            this.btnMaxValues.Margin = new System.Windows.Forms.Padding(2);
            this.btnMaxValues.Name = "btnMaxValues";
            this.btnMaxValues.Size = new System.Drawing.Size(70, 36);
            this.btnMaxValues.TabIndex = 49;
            this.btnMaxValues.Text = "最大";
            this.btnMaxValues.UseVisualStyleBackColor = true;
            this.btnMaxValues.Click += new System.EventHandler(this.btnMaxValues_Click);
            // 
            // cbWeight
            // 
            this.cbWeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWeight.Enabled = false;
            this.cbWeight.FormattingEnabled = true;
            this.cbWeight.Location = new System.Drawing.Point(857, 11);
            this.cbWeight.Name = "cbWeight";
            this.cbWeight.Size = new System.Drawing.Size(107, 32);
            this.cbWeight.TabIndex = 51;
            // 
            // labelSockets
            // 
            this.labelSockets.AutoSize = true;
            this.labelSockets.Location = new System.Drawing.Point(624, 20);
            this.labelSockets.Name = "labelSockets";
            this.labelSockets.Size = new System.Drawing.Size(82, 24);
            this.labelSockets.TabIndex = 52;
            this.labelSockets.Text = "label1";
            // 
            // labelRunewordsCannotEdit
            // 
            this.labelRunewordsCannotEdit.AutoSize = true;
            this.labelRunewordsCannotEdit.ForeColor = System.Drawing.Color.Red;
            this.labelRunewordsCannotEdit.Location = new System.Drawing.Point(526, 134);
            this.labelRunewordsCannotEdit.Name = "labelRunewordsCannotEdit";
            this.labelRunewordsCannotEdit.Size = new System.Drawing.Size(82, 24);
            this.labelRunewordsCannotEdit.TabIndex = 53;
            this.labelRunewordsCannotEdit.Text = "label1";
            // 
            // FormEditItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1048, 675);
            this.Controls.Add(this.labelRunewordsCannotEdit);
            this.Controls.Add(this.labelSockets);
            this.Controls.Add(this.cbWeight);
            this.Controls.Add(this.btnRestoreValues);
            this.Controls.Add(this.btnMaxValues);
            this.Controls.Add(this.dgvItemStatCost);
            this.Controls.Add(this.cbItems);
            this.Controls.Add(this.btnDeleteStat);
            this.Controls.Add(this.cbSubTypes);
            this.Controls.Add(this.cbIsEthereal);
            this.Controls.Add(this.cbTypes);
            this.Controls.Add(this.btnAddStat);
            this.Controls.Add(this.cbQuality);
            this.Controls.Add(this.cbSockets);
            this.Controls.Add(this.lbBasicDescription);
            this.Controls.Add(this.pbItemPicture);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FormEditItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑装备";
            this.Load += new System.EventHandler(this.FormEditItem_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemStatCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbItemPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSaveAsTemplate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView dgvItemStatCost;
        private System.Windows.Forms.ComboBox cbItems;
        private System.Windows.Forms.Button btnDeleteStat;
        private System.Windows.Forms.ComboBox cbSubTypes;
        private System.Windows.Forms.CheckBox cbIsEthereal;
        private System.Windows.Forms.ComboBox cbTypes;
        private System.Windows.Forms.Button btnAddStat;
        private System.Windows.Forms.ComboBox cbQuality;
        private System.Windows.Forms.ComboBox cbSockets;
        private System.Windows.Forms.ListBox lbBasicDescription;
        private System.Windows.Forms.PictureBox pbItemPicture;
        private System.Windows.Forms.Button btnRestoreValues;
        private System.Windows.Forms.Button btnMaxValues;
        private System.Windows.Forms.ComboBox cbWeight;
        private System.Windows.Forms.Label labelSockets;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDescFunc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescFuncValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxValue;
        private System.Windows.Forms.Label labelRunewordsCannotEdit;
    }
}