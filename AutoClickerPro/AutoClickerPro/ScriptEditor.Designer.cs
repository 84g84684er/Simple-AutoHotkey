namespace AutoClickerPro
{
    partial class ScriptEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditor));
            this.lstActions = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbRandomType = new System.Windows.Forms.ComboBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.chkCurrentPos = new System.Windows.Forms.CheckBox();
            this.cmbActionType = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtScriptName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numLoopCount = new System.Windows.Forms.NumericUpDown();
            this.rdoCount = new System.Windows.Forms.RadioButton();
            this.rdoInfinite = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopCount)).BeginInit();
            this.SuspendLayout();
            // 
            // lstActions
            // 
            this.lstActions.AllowDrop = true;
            this.lstActions.FormattingEnabled = true;
            this.lstActions.ItemHeight = 15;
            this.lstActions.Location = new System.Drawing.Point(380, 85);
            this.lstActions.Name = "lstActions";
            this.lstActions.Size = new System.Drawing.Size(428, 379);
            this.lstActions.TabIndex = 0;
            this.lstActions.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstActions_DragDrop);
            this.lstActions.DragOver += new System.Windows.Forms.DragEventHandler(this.LstActions_DragOver);
            this.lstActions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LstActions_MouseDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numDelay);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.cmbActionType);
            this.groupBox1.Location = new System.Drawing.Point(23, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 320);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "鍵鼠";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "延遲(毫秒)";
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(169, 280);
            this.numDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(120, 25);
            this.numDelay.TabIndex = 3;
            this.numDelay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmbRandomType);
            this.panel2.Controls.Add(this.txtKey);
            this.panel2.Location = new System.Drawing.Point(44, 158);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 116);
            this.panel2.TabIndex = 2;
            // 
            // cmbRandomType
            // 
            this.cmbRandomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRandomType.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbRandomType.FormattingEnabled = true;
            this.cmbRandomType.Location = new System.Drawing.Point(12, 66);
            this.cmbRandomType.Name = "cmbRandomType";
            this.cmbRandomType.Size = new System.Drawing.Size(121, 28);
            this.cmbRandomType.TabIndex = 1;
            // 
            // txtKey
            // 
            this.txtKey.BackColor = System.Drawing.Color.White;
            this.txtKey.Location = new System.Drawing.Point(12, 14);
            this.txtKey.Multiline = true;
            this.txtKey.Name = "txtKey";
            this.txtKey.ReadOnly = true;
            this.txtKey.Size = new System.Drawing.Size(187, 35);
            this.txtKey.TabIndex = 0;
            this.txtKey.Text = "請點擊此處並按下按鍵";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkLock);
            this.panel1.Controls.Add(this.txtY);
            this.panel1.Controls.Add(this.txtX);
            this.panel1.Controls.Add(this.chkCurrentPos);
            this.panel1.Location = new System.Drawing.Point(44, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 100);
            this.panel1.TabIndex = 1;
            // 
            // chkLock
            // 
            this.chkLock.AutoSize = true;
            this.chkLock.Location = new System.Drawing.Point(12, 39);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(59, 19);
            this.chkLock.TabIndex = 3;
            this.chkLock.Text = "鎖定";
            this.chkLock.UseVisualStyleBackColor = true;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(95, 64);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(75, 25);
            this.txtY.TabIndex = 2;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(12, 64);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(77, 25);
            this.txtX.TabIndex = 1;
            // 
            // chkCurrentPos
            // 
            this.chkCurrentPos.AutoSize = true;
            this.chkCurrentPos.Location = new System.Drawing.Point(12, 13);
            this.chkCurrentPos.Name = "chkCurrentPos";
            this.chkCurrentPos.Size = new System.Drawing.Size(59, 19);
            this.chkCurrentPos.TabIndex = 0;
            this.chkCurrentPos.Text = "位置";
            this.chkCurrentPos.UseVisualStyleBackColor = true;
            // 
            // cmbActionType
            // 
            this.cmbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionType.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbActionType.FormattingEnabled = true;
            this.cmbActionType.Location = new System.Drawing.Point(44, 17);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.Size = new System.Drawing.Size(215, 28);
            this.cmbActionType.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(147, 481);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 44);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "刪除動作";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(23, 481);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(104, 44);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "加入動作";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtScriptName
            // 
            this.txtScriptName.Location = new System.Drawing.Point(456, 481);
            this.txtScriptName.Multiline = true;
            this.txtScriptName.Name = "txtScriptName";
            this.txtScriptName.Size = new System.Drawing.Size(148, 34);
            this.txtScriptName.TabIndex = 2;
            this.txtScriptName.Text = "設定腳本名稱";
            this.txtScriptName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(636, 481);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 44);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "儲存\r\n";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(725, 481);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 44);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numLoopCount);
            this.groupBox2.Controls.Add(this.rdoCount);
            this.groupBox2.Controls.Add(this.rdoInfinite);
            this.groupBox2.Location = new System.Drawing.Point(23, 344);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 120);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "循環";
            // 
            // numLoopCount
            // 
            this.numLoopCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numLoopCount.Location = new System.Drawing.Point(138, 77);
            this.numLoopCount.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numLoopCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLoopCount.Name = "numLoopCount";
            this.numLoopCount.Size = new System.Drawing.Size(121, 25);
            this.numLoopCount.TabIndex = 2;
            this.numLoopCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rdoCount
            // 
            this.rdoCount.AutoSize = true;
            this.rdoCount.Location = new System.Drawing.Point(16, 77);
            this.rdoCount.Name = "rdoCount";
            this.rdoCount.Size = new System.Drawing.Size(88, 19);
            this.rdoCount.TabIndex = 1;
            this.rdoCount.TabStop = true;
            this.rdoCount.Text = "指定次數";
            this.rdoCount.UseVisualStyleBackColor = true;
            // 
            // rdoInfinite
            // 
            this.rdoInfinite.AutoSize = true;
            this.rdoInfinite.Location = new System.Drawing.Point(16, 41);
            this.rdoInfinite.Name = "rdoInfinite";
            this.rdoInfinite.Size = new System.Drawing.Size(88, 19);
            this.rdoInfinite.TabIndex = 0;
            this.rdoInfinite.TabStop = true;
            this.rdoInfinite.Text = "無限循環";
            this.rdoInfinite.UseVisualStyleBackColor = true;
            // 
            // ScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 537);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtScriptName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lstActions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ScriptEditor";
            this.Text = "腳本編輯";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstActions;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkCurrentPos;
        private System.Windows.Forms.ComboBox cmbActionType;
        private System.Windows.Forms.TextBox txtScriptName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbRandomType;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkLock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numLoopCount;
        private System.Windows.Forms.RadioButton rdoCount;
        private System.Windows.Forms.RadioButton rdoInfinite;
    }
}