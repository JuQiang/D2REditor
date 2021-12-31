namespace D2REditor.Forms
{
    partial class FormEditItemWithAffix
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelItemName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbItems = new System.Windows.Forms.ComboBox();
            this.cbIsEthereal = new System.Windows.Forms.CheckBox();
            this.cbSubTypes = new System.Windows.Forms.ComboBox();
            this.cbTypes = new System.Windows.Forms.ComboBox();
            this.cbIsIdentified = new System.Windows.Forms.CheckBox();
            this.cbIsRuneWord = new System.Windows.Forms.CheckBox();
            this.cbIsCompact = new System.Windows.Forms.CheckBox();
            this.cbQuality = new System.Windows.Forms.ComboBox();
            this.lbBasicDescription = new System.Windows.Forms.ListBox();
            this.pbItemPicture = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cbSockets = new System.Windows.Forms.ComboBox();
            this.gbSocket = new System.Windows.Forms.GroupBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnSuffix2 = new System.Windows.Forms.Button();
            this.btnSuffix1 = new System.Windows.Forms.Button();
            this.btnSuffix0 = new System.Windows.Forms.Button();
            this.btnPrefix2 = new System.Windows.Forms.Button();
            this.btnPrefix1 = new System.Windows.Forms.Button();
            this.btnPrefix0 = new System.Windows.Forms.Button();
            this.btnDeleteStat = new System.Windows.Forms.Button();
            this.btnAddStat = new System.Windows.Forms.Button();
            this.dgvItemStatCost = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescFunc = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStat1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStat2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStat3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDgrpFunc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSaveAsTemplate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbItemPicture)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemStatCost)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1336, 675);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelItemName);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.lbBasicDescription);
            this.tabPage1.Controls.Add(this.pbItemPicture);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 37);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1328, 634);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelItemName
            // 
            this.labelItemName.AutoSize = true;
            this.labelItemName.Location = new System.Drawing.Point(53, 14);
            this.labelItemName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelItemName.Name = "labelItemName";
            this.labelItemName.Size = new System.Drawing.Size(34, 24);
            this.labelItemName.TabIndex = 15;
            this.labelItemName.Text = "的";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 120);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 24);
            this.label2.TabIndex = 14;
            this.label2.Text = "的";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbItems);
            this.groupBox1.Controls.Add(this.cbIsEthereal);
            this.groupBox1.Controls.Add(this.cbSubTypes);
            this.groupBox1.Controls.Add(this.cbTypes);
            this.groupBox1.Controls.Add(this.cbIsIdentified);
            this.groupBox1.Controls.Add(this.cbIsRuneWord);
            this.groupBox1.Controls.Add(this.cbIsCompact);
            this.groupBox1.Controls.Add(this.cbQuality);
            this.groupBox1.Location = new System.Drawing.Point(7, 155);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(687, 254);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本属性";
            // 
            // cbItems
            // 
            this.cbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItems.FormattingEnabled = true;
            this.cbItems.Location = new System.Drawing.Point(22, 159);
            this.cbItems.Margin = new System.Windows.Forms.Padding(2);
            this.cbItems.Name = "cbItems";
            this.cbItems.Size = new System.Drawing.Size(212, 32);
            this.cbItems.TabIndex = 23;
            // 
            // cbIsEthereal
            // 
            this.cbIsEthereal.AutoSize = true;
            this.cbIsEthereal.Location = new System.Drawing.Point(504, 26);
            this.cbIsEthereal.Margin = new System.Windows.Forms.Padding(2);
            this.cbIsEthereal.Name = "cbIsEthereal";
            this.cbIsEthereal.Size = new System.Drawing.Size(84, 28);
            this.cbIsEthereal.TabIndex = 19;
            this.cbIsEthereal.Text = "无形";
            this.cbIsEthereal.UseVisualStyleBackColor = true;
            // 
            // cbSubTypes
            // 
            this.cbSubTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubTypes.FormattingEnabled = true;
            this.cbSubTypes.Location = new System.Drawing.Point(116, 108);
            this.cbSubTypes.Margin = new System.Windows.Forms.Padding(2);
            this.cbSubTypes.Name = "cbSubTypes";
            this.cbSubTypes.Size = new System.Drawing.Size(119, 32);
            this.cbSubTypes.TabIndex = 21;
            // 
            // cbTypes
            // 
            this.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypes.FormattingEnabled = true;
            this.cbTypes.Location = new System.Drawing.Point(23, 108);
            this.cbTypes.Margin = new System.Windows.Forms.Padding(2);
            this.cbTypes.Name = "cbTypes";
            this.cbTypes.Size = new System.Drawing.Size(78, 32);
            this.cbTypes.TabIndex = 20;
            // 
            // cbIsIdentified
            // 
            this.cbIsIdentified.AutoSize = true;
            this.cbIsIdentified.Location = new System.Drawing.Point(392, 26);
            this.cbIsIdentified.Margin = new System.Windows.Forms.Padding(2);
            this.cbIsIdentified.Name = "cbIsIdentified";
            this.cbIsIdentified.Size = new System.Drawing.Size(108, 28);
            this.cbIsIdentified.TabIndex = 18;
            this.cbIsIdentified.Text = "已辨识";
            this.cbIsIdentified.UseVisualStyleBackColor = true;
            // 
            // cbIsRuneWord
            // 
            this.cbIsRuneWord.AutoSize = true;
            this.cbIsRuneWord.Location = new System.Drawing.Point(246, 26);
            this.cbIsRuneWord.Margin = new System.Windows.Forms.Padding(2);
            this.cbIsRuneWord.Name = "cbIsRuneWord";
            this.cbIsRuneWord.Size = new System.Drawing.Size(132, 28);
            this.cbIsRuneWord.TabIndex = 18;
            this.cbIsRuneWord.Text = "符文之语";
            this.cbIsRuneWord.UseVisualStyleBackColor = true;
            // 
            // cbIsCompact
            // 
            this.cbIsCompact.AutoSize = true;
            this.cbIsCompact.Location = new System.Drawing.Point(23, 26);
            this.cbIsCompact.Margin = new System.Windows.Forms.Padding(2);
            this.cbIsCompact.Name = "cbIsCompact";
            this.cbIsCompact.Size = new System.Drawing.Size(84, 28);
            this.cbIsCompact.TabIndex = 16;
            this.cbIsCompact.Text = "简单";
            this.cbIsCompact.UseVisualStyleBackColor = true;
            // 
            // cbQuality
            // 
            this.cbQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuality.FormattingEnabled = true;
            this.cbQuality.Location = new System.Drawing.Point(23, 59);
            this.cbQuality.Margin = new System.Windows.Forms.Padding(2);
            this.cbQuality.Name = "cbQuality";
            this.cbQuality.Size = new System.Drawing.Size(212, 32);
            this.cbQuality.TabIndex = 11;
            // 
            // lbBasicDescription
            // 
            this.lbBasicDescription.FormattingEnabled = true;
            this.lbBasicDescription.ItemHeight = 24;
            this.lbBasicDescription.Location = new System.Drawing.Point(126, 5);
            this.lbBasicDescription.Margin = new System.Windows.Forms.Padding(2);
            this.lbBasicDescription.Name = "lbBasicDescription";
            this.lbBasicDescription.Size = new System.Drawing.Size(574, 100);
            this.lbBasicDescription.TabIndex = 11;
            // 
            // pbItemPicture
            // 
            this.pbItemPicture.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbItemPicture.Location = new System.Drawing.Point(10, 0);
            this.pbItemPicture.Margin = new System.Windows.Forms.Padding(2);
            this.pbItemPicture.Name = "pbItemPicture";
            this.pbItemPicture.Size = new System.Drawing.Size(98, 147);
            this.pbItemPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbItemPicture.TabIndex = 10;
            this.pbItemPicture.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(126, 109);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(99, 35);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "QSH";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cbSockets);
            this.tabPage2.Controls.Add(this.gbSocket);
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1328, 640);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "打孔";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cbSockets
            // 
            this.cbSockets.FormattingEnabled = true;
            this.cbSockets.Items.AddRange(new object[] {
            "无孔",
            "1孔",
            "2孔",
            "3孔",
            "4孔",
            "5孔",
            "6孔"});
            this.cbSockets.Location = new System.Drawing.Point(44, 429);
            this.cbSockets.Name = "cbSockets";
            this.cbSockets.Size = new System.Drawing.Size(121, 32);
            this.cbSockets.TabIndex = 27;
            // 
            // gbSocket
            // 
            this.gbSocket.Location = new System.Drawing.Point(336, 310);
            this.gbSocket.Margin = new System.Windows.Forms.Padding(2);
            this.gbSocket.Name = "gbSocket";
            this.gbSocket.Padding = new System.Windows.Forms.Padding(2);
            this.gbSocket.Size = new System.Drawing.Size(326, 86);
            this.gbSocket.TabIndex = 26;
            this.gbSocket.TabStop = false;
            this.gbSocket.Text = "打孔";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnSuffix2);
            this.tabPage3.Controls.Add(this.btnSuffix1);
            this.tabPage3.Controls.Add(this.btnSuffix0);
            this.tabPage3.Controls.Add(this.btnPrefix2);
            this.tabPage3.Controls.Add(this.btnPrefix1);
            this.tabPage3.Controls.Add(this.btnPrefix0);
            this.tabPage3.Controls.Add(this.btnDeleteStat);
            this.tabPage3.Controls.Add(this.btnAddStat);
            this.tabPage3.Controls.Add(this.dgvItemStatCost);
            this.tabPage3.Location = new System.Drawing.Point(4, 37);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1328, 634);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "词缀及高级属性";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnSuffix2
            // 
            this.btnSuffix2.Location = new System.Drawing.Point(533, 13);
            this.btnSuffix2.Name = "btnSuffix2";
            this.btnSuffix2.Size = new System.Drawing.Size(98, 89);
            this.btnSuffix2.TabIndex = 53;
            this.btnSuffix2.Text = "后缀";
            this.btnSuffix2.UseVisualStyleBackColor = true;
            this.btnSuffix2.Click += new System.EventHandler(this.OnSuffixClicked);
            // 
            // btnSuffix1
            // 
            this.btnSuffix1.Location = new System.Drawing.Point(424, 13);
            this.btnSuffix1.Name = "btnSuffix1";
            this.btnSuffix1.Size = new System.Drawing.Size(98, 89);
            this.btnSuffix1.TabIndex = 52;
            this.btnSuffix1.Text = "后缀";
            this.btnSuffix1.UseVisualStyleBackColor = true;
            this.btnSuffix1.Click += new System.EventHandler(this.OnSuffixClicked);
            // 
            // btnSuffix0
            // 
            this.btnSuffix0.Location = new System.Drawing.Point(320, 13);
            this.btnSuffix0.Name = "btnSuffix0";
            this.btnSuffix0.Size = new System.Drawing.Size(98, 89);
            this.btnSuffix0.TabIndex = 51;
            this.btnSuffix0.Text = "后缀";
            this.btnSuffix0.UseVisualStyleBackColor = true;
            this.btnSuffix0.Click += new System.EventHandler(this.OnSuffixClicked);
            // 
            // btnPrefix2
            // 
            this.btnPrefix2.Location = new System.Drawing.Point(216, 13);
            this.btnPrefix2.Name = "btnPrefix2";
            this.btnPrefix2.Size = new System.Drawing.Size(98, 89);
            this.btnPrefix2.TabIndex = 50;
            this.btnPrefix2.Text = "前缀";
            this.btnPrefix2.UseVisualStyleBackColor = true;
            this.btnPrefix2.Click += new System.EventHandler(this.OnPrefixClicked);
            // 
            // btnPrefix1
            // 
            this.btnPrefix1.Location = new System.Drawing.Point(112, 13);
            this.btnPrefix1.Name = "btnPrefix1";
            this.btnPrefix1.Size = new System.Drawing.Size(98, 89);
            this.btnPrefix1.TabIndex = 49;
            this.btnPrefix1.Text = "前缀";
            this.btnPrefix1.UseVisualStyleBackColor = true;
            this.btnPrefix1.Click += new System.EventHandler(this.OnPrefixClicked);
            // 
            // btnPrefix0
            // 
            this.btnPrefix0.Location = new System.Drawing.Point(8, 13);
            this.btnPrefix0.Name = "btnPrefix0";
            this.btnPrefix0.Size = new System.Drawing.Size(98, 89);
            this.btnPrefix0.TabIndex = 48;
            this.btnPrefix0.Text = "前缀";
            this.btnPrefix0.UseVisualStyleBackColor = true;
            this.btnPrefix0.Click += new System.EventHandler(this.OnPrefixClicked);
            // 
            // btnDeleteStat
            // 
            this.btnDeleteStat.Location = new System.Drawing.Point(1262, 272);
            this.btnDeleteStat.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteStat.Name = "btnDeleteStat";
            this.btnDeleteStat.Size = new System.Drawing.Size(59, 36);
            this.btnDeleteStat.TabIndex = 8;
            this.btnDeleteStat.Text = "-";
            this.btnDeleteStat.UseVisualStyleBackColor = true;
            this.btnDeleteStat.Click += new System.EventHandler(this.btnDeleteStat_Click);
            // 
            // btnAddStat
            // 
            this.btnAddStat.Location = new System.Drawing.Point(1262, 208);
            this.btnAddStat.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddStat.Name = "btnAddStat";
            this.btnAddStat.Size = new System.Drawing.Size(59, 36);
            this.btnAddStat.TabIndex = 7;
            this.btnAddStat.Text = "+";
            this.btnAddStat.UseVisualStyleBackColor = true;
            this.btnAddStat.Click += new System.EventHandler(this.btnAddStat_Click);
            // 
            // dgvItemStatCost
            // 
            this.dgvItemStatCost.ColumnHeadersHeight = 34;
            this.dgvItemStatCost.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colDescFunc,
            this.colValue,
            this.colParam,
            this.colMaxValue,
            this.colStat1,
            this.colStat2,
            this.colStat3,
            this.colDgrpFunc});
            this.dgvItemStatCost.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvItemStatCost.Location = new System.Drawing.Point(7, 127);
            this.dgvItemStatCost.Margin = new System.Windows.Forms.Padding(2);
            this.dgvItemStatCost.Name = "dgvItemStatCost";
            this.dgvItemStatCost.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvItemStatCost.RowTemplate.Height = 30;
            this.dgvItemStatCost.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemStatCost.Size = new System.Drawing.Size(1236, 384);
            this.dgvItemStatCost.TabIndex = 6;
            this.dgvItemStatCost.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemStatCost_CellValueChanged);
            this.dgvItemStatCost.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvItemStatCost_CurrentCellDirtyStateChanged);
            // 
            // colID
            // 
            this.colID.HeaderText = "编号";
            this.colID.MinimumWidth = 8;
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Width = 150;
            // 
            // colDescFunc
            // 
            this.colDescFunc.HeaderText = "高级属性";
            this.colDescFunc.MinimumWidth = 8;
            this.colDescFunc.Name = "colDescFunc";
            this.colDescFunc.Width = 150;
            // 
            // colValue
            // 
            this.colValue.HeaderText = "值";
            this.colValue.MinimumWidth = 8;
            this.colValue.Name = "colValue";
            this.colValue.Width = 150;
            // 
            // colParam
            // 
            this.colParam.HeaderText = "参数";
            this.colParam.MinimumWidth = 8;
            this.colParam.Name = "colParam";
            this.colParam.ReadOnly = true;
            this.colParam.Width = 150;
            // 
            // colMaxValue
            // 
            this.colMaxValue.HeaderText = "最大值";
            this.colMaxValue.MinimumWidth = 8;
            this.colMaxValue.Name = "colMaxValue";
            this.colMaxValue.ReadOnly = true;
            this.colMaxValue.Width = 150;
            // 
            // colStat1
            // 
            this.colStat1.HeaderText = "影响1";
            this.colStat1.MinimumWidth = 8;
            this.colStat1.Name = "colStat1";
            this.colStat1.ReadOnly = true;
            this.colStat1.Width = 150;
            // 
            // colStat2
            // 
            this.colStat2.HeaderText = "影响2";
            this.colStat2.MinimumWidth = 8;
            this.colStat2.Name = "colStat2";
            this.colStat2.ReadOnly = true;
            this.colStat2.Width = 150;
            // 
            // colStat3
            // 
            this.colStat3.HeaderText = "影响3";
            this.colStat3.MinimumWidth = 8;
            this.colStat3.Name = "colStat3";
            this.colStat3.ReadOnly = true;
            this.colStat3.Width = 150;
            // 
            // colDgrpFunc
            // 
            this.colDgrpFunc.HeaderText = "合并属性";
            this.colDgrpFunc.MinimumWidth = 8;
            this.colDgrpFunc.Name = "colDgrpFunc";
            this.colDgrpFunc.ReadOnly = true;
            this.colDgrpFunc.Width = 150;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveAsTemplate);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 575);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1336, 100);
            this.panel1.TabIndex = 13;
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
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1151, 34);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(151, 36);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(977, 34);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(151, 36);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FormEditItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1336, 675);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FormEditItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑装备";
            this.Load += new System.EventHandler(this.FormEditItem_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbItemPicture)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemStatCost)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label labelItemName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbItems;
        private System.Windows.Forms.CheckBox cbIsEthereal;
        private System.Windows.Forms.ComboBox cbSubTypes;
        private System.Windows.Forms.ComboBox cbTypes;
        private System.Windows.Forms.CheckBox cbIsIdentified;
        private System.Windows.Forms.CheckBox cbIsRuneWord;
        private System.Windows.Forms.CheckBox cbIsCompact;
        private System.Windows.Forms.ComboBox cbQuality;
        private System.Windows.Forms.ListBox lbBasicDescription;
        private System.Windows.Forms.PictureBox pbItemPicture;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnDeleteStat;
        private System.Windows.Forms.Button btnAddStat;
        private System.Windows.Forms.DataGridView dgvItemStatCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDescFunc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStat1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStat2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStat3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDgrpFunc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSaveAsTemplate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cbSockets;
        private System.Windows.Forms.GroupBox gbSocket;
        private System.Windows.Forms.Button btnPrefix0;
        private System.Windows.Forms.Button btnSuffix2;
        private System.Windows.Forms.Button btnSuffix1;
        private System.Windows.Forms.Button btnSuffix0;
        private System.Windows.Forms.Button btnPrefix2;
        private System.Windows.Forms.Button btnPrefix1;
    }
}