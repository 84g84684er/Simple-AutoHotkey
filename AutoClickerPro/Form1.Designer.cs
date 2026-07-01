namespace AutoClickerPro
{
    partial class form1
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

        #region Windows Form 設計工具產生的程式碼

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form1));
            this.chkListScripts = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnToggleTheme = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkListScripts
            // 
            this.chkListScripts.AllowDrop = true;
            this.chkListScripts.CheckOnClick = false; // 強制關閉點文字自動勾選
            this.chkListScripts.FormattingEnabled = true;
            this.chkListScripts.Location = new System.Drawing.Point(20, 65);
            this.chkListScripts.Name = "chkListScripts";
            this.chkListScripts.Size = new System.Drawing.Size(370, 284);
            this.chkListScripts.TabIndex = 2;
            this.chkListScripts.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChkListScripts_ItemCheck);
            this.chkListScripts.DragDrop += new System.Windows.Forms.DragEventHandler(this.ChkListScripts_DragDrop);
            this.chkListScripts.DragEnter += new System.Windows.Forms.DragEventHandler(this.ChkListScripts_DragEnter); // 新增：安全拖曳核可事件
            this.chkListScripts.DragOver += new System.Windows.Forms.DragEventHandler(this.ChkListScripts_DragOver);
            this.chkListScripts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChkListScripts_MouseDown);
            this.chkListScripts.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChkListScripts_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(20, 365);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "💡 F2啟動 ｜ F1停止";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(405, 65);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 45);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "新增腳本 ＋";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(405, 120);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 45);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "編輯腳本 📝";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(405, 175);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(120, 45);
            this.btnDel.TabIndex = 5;
            this.btnDel.Text = "刪除腳本 🗑";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(191, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "按鍵精靈專業版 Pro";
            // 
            // btnToggleTheme
            // 
            this.btnToggleTheme.Location = new System.Drawing.Point(405, 15);
            this.btnToggleTheme.Name = "btnToggleTheme";
            this.btnToggleTheme.Size = new System.Drawing.Size(120, 32);
            this.btnToggleTheme.TabIndex = 1;
            this.btnToggleTheme.Text = "🌙 深色模式";
            this.btnToggleTheme.UseVisualStyleBackColor = true;
            this.btnToggleTheme.Click += new System.EventHandler(this.btnToggleTheme_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(402, 365);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(103, 15);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "狀態：已停止";
            // 
            // form1
            // 
            this.ClientSize = new System.Drawing.Size(545, 400);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnToggleTheme);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkListScripts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "按鍵精靈";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkListScripts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnToggleTheme;
        private System.Windows.Forms.Label lblStatus;
    }
}