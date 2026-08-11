namespace AutoClickerPro
{
    partial class ScriptEditor
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditor));
            this.grpScriptSettings = new System.Windows.Forms.GroupBox();
            this.lblScriptName = new System.Windows.Forms.Label();
            this.txtScriptName = new System.Windows.Forms.TextBox();
            this.rdoInfinite = new System.Windows.Forms.RadioButton();
            this.rdoCount = new System.Windows.Forms.RadioButton();
            this.numLoopCount = new System.Windows.Forms.NumericUpDown();
            this.grpActionCreator = new System.Windows.Forms.GroupBox();
            this.lblActionType = new System.Windows.Forms.Label();
            this.cmbActionType = new System.Windows.Forms.ComboBox();
            this.grpParameters = new System.Windows.Forms.GroupBox();
            this.pnlMouse = new System.Windows.Forms.Panel();
            this.chkCurrentPos = new System.Windows.Forms.CheckBox();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.lblMouseKey = new System.Windows.Forms.Label();
            this.chkMouseLeft = new System.Windows.Forms.CheckBox();
            this.chkMouseRight = new System.Windows.Forms.CheckBox();
            this.lblMouseMode = new System.Windows.Forms.Label();
            this.chkMouseKeyDown = new System.Windows.Forms.CheckBox();
            this.chkMouseKeyUp = new System.Windows.Forms.CheckBox();
            this.pnlKeyboard = new System.Windows.Forms.Panel();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblKeyMode = new System.Windows.Forms.Label();
            this.chkKeyDown = new System.Windows.Forms.CheckBox();
            this.chkKeyUp = new System.Windows.Forms.CheckBox();
            this.pnlRandom = new System.Windows.Forms.Panel();
            this.lblRandom = new System.Windows.Forms.Label();
            this.cmbRandomType = new System.Windows.Forms.ComboBox();
            this.pnlDelay = new System.Windows.Forms.Panel();
            this.lblDelay = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.pnlLockMouse = new System.Windows.Forms.Panel();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpActionSequence = new System.Windows.Forms.GroupBox();
            this.lstActions = new System.Windows.Forms.ListBox();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpScriptSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopCount)).BeginInit();
            this.grpActionCreator.SuspendLayout();
            this.grpParameters.SuspendLayout();
            this.pnlMouse.SuspendLayout();
            this.pnlKeyboard.SuspendLayout();
            this.pnlRandom.SuspendLayout();
            this.pnlDelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.pnlLockMouse.SuspendLayout();
            this.grpActionSequence.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpScriptSettings
            // 
            this.grpScriptSettings.Controls.Add(this.lblScriptName);
            this.grpScriptSettings.Controls.Add(this.txtScriptName);
            this.grpScriptSettings.Controls.Add(this.rdoInfinite);
            this.grpScriptSettings.Controls.Add(this.rdoCount);
            this.grpScriptSettings.Controls.Add(this.numLoopCount);
            this.grpScriptSettings.Location = new System.Drawing.Point(15, 15);
            this.grpScriptSettings.Name = "grpScriptSettings";
            this.grpScriptSettings.Size = new System.Drawing.Size(320, 130);
            this.grpScriptSettings.TabIndex = 0;
            this.grpScriptSettings.TabStop = false;
            this.grpScriptSettings.Text = "腳本基本設定";
            // 
            // lblScriptName
            // 
            this.lblScriptName.AutoSize = true;
            this.lblScriptName.Location = new System.Drawing.Point(15, 30);
            this.lblScriptName.Name = "lblScriptName";
            this.lblScriptName.Size = new System.Drawing.Size(71, 15);
            this.lblScriptName.TabIndex = 0;
            this.lblScriptName.Text = "腳本名稱:";
            // 
            // txtScriptName
            // 
            this.txtScriptName.Location = new System.Drawing.Point(90, 27);
            this.txtScriptName.Name = "txtScriptName";
            this.txtScriptName.Size = new System.Drawing.Size(210, 25);
            this.txtScriptName.TabIndex = 1;
            // 
            // rdoInfinite
            // 
            this.rdoInfinite.AutoSize = true;
            this.rdoInfinite.Location = new System.Drawing.Point(18, 75);
            this.rdoInfinite.Name = "rdoInfinite";
            this.rdoInfinite.Size = new System.Drawing.Size(88, 19);
            this.rdoInfinite.TabIndex = 2;
            this.rdoInfinite.TabStop = true;
            this.rdoInfinite.Text = "無限循環";
            this.rdoInfinite.UseVisualStyleBackColor = true;
            // 
            // rdoCount
            // 
            this.rdoCount.AutoSize = true;
            this.rdoCount.Location = new System.Drawing.Point(115, 75);
            this.rdoCount.Name = "rdoCount";
            this.rdoCount.Size = new System.Drawing.Size(88, 19);
            this.rdoCount.TabIndex = 3;
            this.rdoCount.TabStop = true;
            this.rdoCount.Text = "指定次數";
            this.rdoCount.UseVisualStyleBackColor = true;
            // 
            // numLoopCount
            // 
            this.numLoopCount.Location = new System.Drawing.Point(210, 73);
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
            this.numLoopCount.Size = new System.Drawing.Size(90, 25);
            this.numLoopCount.TabIndex = 4;
            this.numLoopCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // grpActionCreator
            // 
            this.grpActionCreator.Controls.Add(this.lblActionType);
            this.grpActionCreator.Controls.Add(this.cmbActionType);
            this.grpActionCreator.Controls.Add(this.grpParameters);
            this.grpActionCreator.Controls.Add(this.btnAdd);
            this.grpActionCreator.Location = new System.Drawing.Point(15, 160);
            this.grpActionCreator.Name = "grpActionCreator";
            this.grpActionCreator.Size = new System.Drawing.Size(320, 310);
            this.grpActionCreator.TabIndex = 1;
            this.grpActionCreator.TabStop = false;
            this.grpActionCreator.Text = "設定並新增動作";
            // 
            // lblActionType
            // 
            this.lblActionType.AutoSize = true;
            this.lblActionType.Location = new System.Drawing.Point(15, 30);
            this.lblActionType.Name = "lblActionType";
            this.lblActionType.Size = new System.Drawing.Size(71, 15);
            this.lblActionType.TabIndex = 0;
            this.lblActionType.Text = "選擇動作:";
            // 
            // cmbActionType
            // 
            this.cmbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionType.FormattingEnabled = true;
            this.cmbActionType.Location = new System.Drawing.Point(90, 27);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.Size = new System.Drawing.Size(210, 23);
            this.cmbActionType.TabIndex = 1;
            // 
            // grpParameters
            // 
            this.grpParameters.Controls.Add(this.pnlMouse);
            this.grpParameters.Controls.Add(this.pnlKeyboard);
            this.grpParameters.Controls.Add(this.pnlRandom);
            this.grpParameters.Controls.Add(this.pnlDelay);
            this.grpParameters.Controls.Add(this.pnlLockMouse);
            this.grpParameters.Location = new System.Drawing.Point(15, 65);
            this.grpParameters.Name = "grpParameters";
            this.grpParameters.Size = new System.Drawing.Size(285, 175);
            this.grpParameters.TabIndex = 2;
            this.grpParameters.TabStop = false;
            this.grpParameters.Text = "動態參數配置";
            // 
            // pnlMouse
            // 
            this.pnlMouse.Controls.Add(this.chkCurrentPos);
            this.pnlMouse.Controls.Add(this.lblX);
            this.pnlMouse.Controls.Add(this.lblY);
            this.pnlMouse.Controls.Add(this.txtX);
            this.pnlMouse.Controls.Add(this.txtY);
            this.pnlMouse.Controls.Add(this.lblMouseKey);
            this.pnlMouse.Controls.Add(this.chkMouseLeft);
            this.pnlMouse.Controls.Add(this.chkMouseRight);
            this.pnlMouse.Controls.Add(this.lblMouseMode);
            this.pnlMouse.Controls.Add(this.chkMouseKeyDown);
            this.pnlMouse.Controls.Add(this.chkMouseKeyUp);
            this.pnlMouse.Location = new System.Drawing.Point(10, 20);
            this.pnlMouse.Name = "pnlMouse";
            this.pnlMouse.Size = new System.Drawing.Size(265, 140);
            this.pnlMouse.TabIndex = 0;
            // 
            // chkCurrentPos
            // 
            this.chkCurrentPos.AutoSize = true;
            this.chkCurrentPos.Location = new System.Drawing.Point(12, 10);
            this.chkCurrentPos.Name = "chkCurrentPos";
            this.chkCurrentPos.Size = new System.Drawing.Size(179, 19);
            this.chkCurrentPos.TabIndex = 0;
            this.chkCurrentPos.Text = "直接使用游標當前位置";
            this.chkCurrentPos.UseVisualStyleBackColor = true;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(12, 38);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(21, 15);
            this.lblX.TabIndex = 1;
            this.lblX.Text = "X:";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(135, 38);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(21, 15);
            this.lblY.TabIndex = 3;
            this.lblY.Text = "Y:";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(39, 33);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(70, 25);
            this.txtX.TabIndex = 2;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(162, 33);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(70, 25);
            this.txtY.TabIndex = 4;
            // 
            // lblMouseKey
            // 
            this.lblMouseKey.AutoSize = true;
            this.lblMouseKey.Location = new System.Drawing.Point(12, 70);
            this.lblMouseKey.Name = "lblMouseKey";
            this.lblMouseKey.Size = new System.Drawing.Size(41, 15);
            this.lblMouseKey.TabIndex = 5;
            this.lblMouseKey.Text = "按鍵:";
            // 
            // chkMouseLeft
            // 
            this.chkMouseLeft.AutoSize = true;
            this.chkMouseLeft.Location = new System.Drawing.Point(15, 90);
            this.chkMouseLeft.Name = "chkMouseLeft";
            this.chkMouseLeft.Size = new System.Drawing.Size(59, 19);
            this.chkMouseLeft.TabIndex = 6;
            this.chkMouseLeft.Text = "左鍵";
            this.chkMouseLeft.UseVisualStyleBackColor = true;
            // 
            // chkMouseRight
            // 
            this.chkMouseRight.AutoSize = true;
            this.chkMouseRight.Location = new System.Drawing.Point(80, 90);
            this.chkMouseRight.Name = "chkMouseRight";
            this.chkMouseRight.Size = new System.Drawing.Size(59, 19);
            this.chkMouseRight.TabIndex = 7;
            this.chkMouseRight.Text = "右鍵";
            this.chkMouseRight.UseVisualStyleBackColor = true;
            // 
            // lblMouseMode
            // 
            this.lblMouseMode.AutoSize = true;
            this.lblMouseMode.Location = new System.Drawing.Point(150, 70);
            this.lblMouseMode.Name = "lblMouseMode";
            this.lblMouseMode.Size = new System.Drawing.Size(41, 15);
            this.lblMouseMode.TabIndex = 8;
            this.lblMouseMode.Text = "模式:";
            // 
            // chkMouseKeyDown
            // 
            this.chkMouseKeyDown.AutoSize = true;
            this.chkMouseKeyDown.Location = new System.Drawing.Point(150, 90);
            this.chkMouseKeyDown.Name = "chkMouseKeyDown";
            this.chkMouseKeyDown.Size = new System.Drawing.Size(59, 19);
            this.chkMouseKeyDown.TabIndex = 9;
            this.chkMouseKeyDown.Text = "壓下";
            this.chkMouseKeyDown.UseVisualStyleBackColor = true;
            // 
            // chkMouseKeyUp
            // 
            this.chkMouseKeyUp.AutoSize = true;
            this.chkMouseKeyUp.Location = new System.Drawing.Point(150, 115);
            this.chkMouseKeyUp.Name = "chkMouseKeyUp";
            this.chkMouseKeyUp.Size = new System.Drawing.Size(59, 19);
            this.chkMouseKeyUp.TabIndex = 10;
            this.chkMouseKeyUp.Text = "彈起";
            this.chkMouseKeyUp.UseVisualStyleBackColor = true;
            // 
            // pnlKeyboard
            // 
            this.pnlKeyboard.Controls.Add(this.lblKey);
            this.pnlKeyboard.Controls.Add(this.txtKey);
            this.pnlKeyboard.Controls.Add(this.lblKeyMode);
            this.pnlKeyboard.Controls.Add(this.chkKeyDown);
            this.pnlKeyboard.Controls.Add(this.chkKeyUp);
            this.pnlKeyboard.Location = new System.Drawing.Point(10, 20);
            this.pnlKeyboard.Name = "pnlKeyboard";
            this.pnlKeyboard.Size = new System.Drawing.Size(265, 140);
            this.pnlKeyboard.TabIndex = 1;
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(12, 10);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(146, 15);
            this.lblKey.TabIndex = 0;
            this.lblKey.Text = "點擊下方框並按鍵盤:";
            // 
            // txtKey
            // 
            this.txtKey.BackColor = System.Drawing.Color.White;
            this.txtKey.Location = new System.Drawing.Point(12, 33);
            this.txtKey.Name = "txtKey";
            this.txtKey.ReadOnly = true;
            this.txtKey.Size = new System.Drawing.Size(235, 25);
            this.txtKey.TabIndex = 1;
            this.txtKey.Text = "點此並按實體鍵";
            this.txtKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblKeyMode
            // 
            this.lblKeyMode.AutoSize = true;
            this.lblKeyMode.Location = new System.Drawing.Point(12, 75);
            this.lblKeyMode.Name = "lblKeyMode";
            this.lblKeyMode.Size = new System.Drawing.Size(41, 15);
            this.lblKeyMode.TabIndex = 2;
            this.lblKeyMode.Text = "模式:";
            // 
            // chkKeyDown
            // 
            this.chkKeyDown.AutoSize = true;
            this.chkKeyDown.Location = new System.Drawing.Point(15, 98);
            this.chkKeyDown.Name = "chkKeyDown";
            this.chkKeyDown.Size = new System.Drawing.Size(101, 19);
            this.chkKeyDown.TabIndex = 3;
            this.chkKeyDown.Text = "壓下 / 按住";
            this.chkKeyDown.UseVisualStyleBackColor = true;
            // 
            // chkKeyUp
            // 
            this.chkKeyUp.AutoSize = true;
            this.chkKeyUp.Location = new System.Drawing.Point(137, 98);
            this.chkKeyUp.Name = "chkKeyUp";
            this.chkKeyUp.Size = new System.Drawing.Size(101, 19);
            this.chkKeyUp.TabIndex = 4;
            this.chkKeyUp.Text = "彈起 / 放開";
            this.chkKeyUp.UseVisualStyleBackColor = true;
            // 
            // pnlRandom
            // 
            this.pnlRandom.Controls.Add(this.lblRandom);
            this.pnlRandom.Controls.Add(this.cmbRandomType);
            this.pnlRandom.Location = new System.Drawing.Point(10, 20);
            this.pnlRandom.Name = "pnlRandom";
            this.pnlRandom.Size = new System.Drawing.Size(265, 140);
            this.pnlRandom.TabIndex = 2;
            // 
            // lblRandom
            // 
            this.lblRandom.AutoSize = true;
            this.lblRandom.Location = new System.Drawing.Point(15, 15);
            this.lblRandom.Name = "lblRandom";
            this.lblRandom.Size = new System.Drawing.Size(86, 15);
            this.lblRandom.TabIndex = 0;
            this.lblRandom.Text = "隨機字元組:";
            // 
            // cmbRandomType
            // 
            this.cmbRandomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRandomType.FormattingEnabled = true;
            this.cmbRandomType.Location = new System.Drawing.Point(15, 45);
            this.cmbRandomType.Name = "cmbRandomType";
            this.cmbRandomType.Size = new System.Drawing.Size(235, 23);
            this.cmbRandomType.TabIndex = 1;
            // 
            // pnlDelay
            // 
            this.pnlDelay.Controls.Add(this.lblDelay);
            this.pnlDelay.Controls.Add(this.numDelay);
            this.pnlDelay.Location = new System.Drawing.Point(10, 20);
            this.pnlDelay.Name = "pnlDelay";
            this.pnlDelay.Size = new System.Drawing.Size(265, 140);
            this.pnlDelay.TabIndex = 3;
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(15, 15);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(111, 15);
            this.lblDelay.TabIndex = 0;
            this.lblDelay.Text = "延遲時間(毫秒):";
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(15, 45);
            this.numDelay.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(235, 25);
            this.numDelay.TabIndex = 1;
            this.numDelay.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // pnlLockMouse
            // 
            this.pnlLockMouse.Controls.Add(this.chkLock);
            this.pnlLockMouse.Location = new System.Drawing.Point(10, 20);
            this.pnlLockMouse.Name = "pnlLockMouse";
            this.pnlLockMouse.Size = new System.Drawing.Size(265, 140);
            this.pnlLockMouse.TabIndex = 4;
            // 
            // chkLock
            // 
            this.chkLock.AutoSize = true;
            this.chkLock.Location = new System.Drawing.Point(15, 30);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(223, 19);
            this.chkLock.TabIndex = 0;
            this.chkLock.Text = "鎖定滑鼠 (使其無法隨意移動)";
            this.chkLock.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(15, 255);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(285, 40);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "新增此動作至序列 ↓";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpActionSequence
            // 
            this.grpActionSequence.Controls.Add(this.lstActions);
            this.grpActionSequence.Controls.Add(this.btnMoveUp);
            this.grpActionSequence.Controls.Add(this.btnMoveDown);
            this.grpActionSequence.Controls.Add(this.btnDelete);
            this.grpActionSequence.Location = new System.Drawing.Point(350, 15);
            this.grpActionSequence.Name = "grpActionSequence";
            this.grpActionSequence.Size = new System.Drawing.Size(445, 455);
            this.grpActionSequence.TabIndex = 2;
            this.grpActionSequence.TabStop = false;
            this.grpActionSequence.Text = "目前已設定的動作序列";
            // 
            // lstActions
            // 
            this.lstActions.AllowDrop = true;
            this.lstActions.FormattingEnabled = true;
            this.lstActions.ItemHeight = 15;
            this.lstActions.Location = new System.Drawing.Point(15, 25);
            this.lstActions.Name = "lstActions";
            this.lstActions.Size = new System.Drawing.Size(325, 409);
            this.lstActions.TabIndex = 0;
            this.lstActions.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstActions_DragDrop);
            this.lstActions.DragOver += new System.Windows.Forms.DragEventHandler(this.LstActions_DragOver);
            this.lstActions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LstActions_MouseDown);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(350, 25);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(80, 45);
            this.btnMoveUp.TabIndex = 1;
            this.btnMoveUp.Text = "上移 ▲";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(350, 80);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(80, 45);
            this.btnMoveDown.TabIndex = 2;
            this.btnMoveDown.Text = "下移 ▼";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ForeColor = System.Drawing.Color.DarkRed;
            this.btnDelete.Location = new System.Drawing.Point(350, 160);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 45);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "刪除 ✕";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSave.Location = new System.Drawing.Point(559, 480);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(116, 40);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "儲存並返回";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(685, 480);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 535);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpActionSequence);
            this.Controls.Add(this.grpActionCreator);
            this.Controls.Add(this.grpScriptSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ScriptEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "腳本序列編輯器";
            this.grpScriptSettings.ResumeLayout(false);
            this.grpScriptSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopCount)).EndInit();
            this.grpActionCreator.ResumeLayout(false);
            this.grpActionCreator.PerformLayout();
            this.grpParameters.ResumeLayout(false);
            this.pnlMouse.ResumeLayout(false);
            this.pnlMouse.PerformLayout();
            this.pnlKeyboard.ResumeLayout(false);
            this.pnlKeyboard.PerformLayout();
            this.pnlRandom.ResumeLayout(false);
            this.pnlRandom.PerformLayout();
            this.pnlDelay.ResumeLayout(false);
            this.pnlDelay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.pnlLockMouse.ResumeLayout(false);
            this.pnlLockMouse.PerformLayout();
            this.grpActionSequence.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpScriptSettings;
        private System.Windows.Forms.Label lblScriptName;
        private System.Windows.Forms.TextBox txtScriptName;
        private System.Windows.Forms.RadioButton rdoInfinite;
        private System.Windows.Forms.RadioButton rdoCount;
        private System.Windows.Forms.NumericUpDown numLoopCount;
        private System.Windows.Forms.GroupBox grpActionCreator;
        private System.Windows.Forms.Label lblActionType;
        private System.Windows.Forms.ComboBox cmbActionType;
        private System.Windows.Forms.GroupBox grpParameters;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grpActionSequence;
        private System.Windows.Forms.ListBox lstActions;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        private System.Windows.Forms.Panel pnlMouse;
        private System.Windows.Forms.CheckBox chkCurrentPos;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label lblMouseKey;
        private System.Windows.Forms.CheckBox chkMouseLeft;
        private System.Windows.Forms.CheckBox chkMouseRight;
        private System.Windows.Forms.Label lblMouseMode;
        private System.Windows.Forms.CheckBox chkMouseKeyDown;
        private System.Windows.Forms.CheckBox chkMouseKeyUp;

        private System.Windows.Forms.Panel pnlKeyboard;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblKeyMode;
        private System.Windows.Forms.CheckBox chkKeyDown;
        private System.Windows.Forms.CheckBox chkKeyUp;

        private System.Windows.Forms.Panel pnlRandom;
        private System.Windows.Forms.Label lblRandom;
        private System.Windows.Forms.ComboBox cmbRandomType;

        private System.Windows.Forms.Panel pnlDelay;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.NumericUpDown numDelay;

        private System.Windows.Forms.Panel pnlLockMouse;
        private System.Windows.Forms.CheckBox chkLock;
    }
}