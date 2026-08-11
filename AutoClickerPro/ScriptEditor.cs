using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutoClickerPro
{
    public partial class ScriptEditor : Form
    {
        public ScriptProfile CurrentScript { get; set; }

        public ScriptEditor(ScriptProfile script = null)
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);

            // 1. 合併動作，精簡為五大功能類別
            var actionItems = new[]
            {
                new { Name = "模擬滑鼠動作", Value = ActionType.MouseClickLeft }, // 底層用此做為滑鼠母板
                new { Name = "模擬鍵盤按鍵", Value = ActionType.KeyPress },       // 底層用此做為鍵盤母板
                new { Name = "輸入隨機按鍵", Value = ActionType.RandomKey },
                new { Name = "等待延遲 (毫秒)", Value = ActionType.Delay },
                new { Name = "滑鼠鎖定控制", Value = ActionType.LockMouse }
            };

            cmbActionType.DataSource = actionItems;
            cmbActionType.DisplayMember = "Name";
            cmbActionType.ValueMember = "Value";

            var randomItems = new[]
            {
                new { Name = "數字 (0-9)", Value = RandomKeyType.Number },
                new { Name = "英文 (A-Z)", Value = RandomKeyType.Letter },
                new { Name = "符號", Value = RandomKeyType.Symbol }
            };

            cmbRandomType.DataSource = randomItems;
            cmbRandomType.DisplayMember = "Name";
            cmbRandomType.ValueMember = "Value";

            rdoInfinite.CheckedChanged += (s, e) => ToggleLoopUI();
            rdoCount.CheckedChanged += (s, e) => ToggleLoopUI();

            txtKey.KeyDown += txtKey_KeyDown;
            cmbActionType.SelectedIndexChanged += (s, e) => UpdateUIState();

            // 滑鼠左、右鍵勾選狀態互斥處理
            chkMouseLeft.CheckedChanged += (s, e) => {
                if (chkMouseLeft.Checked) chkMouseRight.Checked = false;
            };
            chkMouseRight.CheckedChanged += (s, e) => {
                if (chkMouseRight.Checked) chkMouseLeft.Checked = false;
            };

            // 初始化時設定人性化預設勾選值
            chkMouseLeft.Checked = true;
            chkMouseKeyDown.Checked = true;
            chkMouseKeyUp.Checked = true;

            chkKeyDown.Checked = true;
            chkKeyUp.Checked = true;

            if (script != null)
            {
                CurrentScript = script;
                txtScriptName.Text = script.Name;
                if (script.IsInfiniteLoop)
                    rdoInfinite.Checked = true;
                else
                    rdoCount.Checked = true;

                numLoopCount.Value = script.LoopCount < 1 ? 1 : script.LoopCount;
                foreach (var action in script.Actions)
                {
                    lstActions.Items.Add(action);
                }
            }
            else
            {
                CurrentScript = new ScriptProfile();
                rdoInfinite.Checked = true;
                numLoopCount.Value = 1;
                txtScriptName.Text = "新腳本";
            }

            UpdateUIState();
        }

        private void ToggleLoopUI()
        {
            numLoopCount.Enabled = rdoCount.Checked;
        }

        private void UpdateUIState()
        {
            if (cmbActionType.SelectedValue == null) return;

            ActionType type = (ActionType)cmbActionType.SelectedValue;

            pnlMouse.Visible = false;
            pnlKeyboard.Visible = false;
            pnlRandom.Visible = false;
            pnlDelay.Visible = false;
            pnlLockMouse.Visible = false;

            switch (type)
            {
                case ActionType.MouseClickLeft: // 對應「模擬滑鼠動作」面板
                    pnlMouse.Visible = true;
                    pnlMouse.BringToFront();
                    break;

                case ActionType.KeyPress: // 對應「模擬鍵盤按鍵」面板
                    pnlKeyboard.Visible = true;
                    pnlKeyboard.BringToFront();
                    if (txtKey.Tag == null)
                    {
                        txtKey.Text = "點此並按鍵盤鍵設定";
                    }
                    break;

                case ActionType.RandomKey:
                    pnlRandom.Visible = true;
                    pnlRandom.BringToFront();
                    break;

                case ActionType.Delay:
                    pnlDelay.Visible = true;
                    pnlDelay.BringToFront();
                    break;

                case ActionType.LockMouse:
                    pnlLockMouse.Visible = true;
                    pnlLockMouse.BringToFront();
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptAction action = new ScriptAction();
            ActionType parentType = (ActionType)cmbActionType.SelectedValue;

            switch (parentType)
            {
                case ActionType.Delay:
                    action.Type = ActionType.Delay;
                    action.DelayTime = (int)numDelay.Value;
                    break;

                case ActionType.MouseClickLeft: // 實作「模擬滑鼠動作」整合儲存
                    action.UseCurrentPosition = chkCurrentPos.Checked;
                    if (int.TryParse(txtX.Text, out int x)) action.MouseX = x;
                    if (int.TryParse(txtY.Text, out int y)) action.MouseY = y;

                    bool isLeft = chkMouseLeft.Checked;
                    bool isRight = chkMouseRight.Checked;
                    if (!isLeft && !isRight)
                    {
                        MessageBox.Show("請選擇『左鍵』或『右鍵』！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    bool isMousePress = chkMouseKeyDown.Checked && chkMouseKeyUp.Checked;
                    bool isMouseDownOnly = chkMouseKeyDown.Checked && !chkMouseKeyUp.Checked;
                    bool isMouseUpOnly = !chkMouseKeyDown.Checked && chkMouseKeyUp.Checked;

                    if (!chkMouseKeyDown.Checked && !chkMouseKeyUp.Checked)
                    {
                        MessageBox.Show("請至少勾選『壓下』或『彈起』其中一個模式！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (isLeft)
                    {
                        if (isMousePress) action.Type = ActionType.MouseClickLeft;
                        else if (isMouseDownOnly) action.Type = ActionType.MouseLeftDown;
                        else action.Type = ActionType.MouseLeftUp;
                    }
                    else // 右鍵
                    {
                        if (isMousePress) action.Type = ActionType.MouseClickRight;
                        else if (isMouseDownOnly) action.Type = ActionType.MouseRightDown;
                        else action.Type = ActionType.MouseRightUp;
                    }
                    break;

                case ActionType.KeyPress: // 實作「模擬鍵盤按鍵」整合儲存
                    if (txtKey.Tag == null)
                    {
                        MessageBox.Show("請先點擊文字框，並按下鍵盤上的一個實體按鍵！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    action.KeyCode = (int)txtKey.Tag;

                    bool isKeyPress = chkKeyDown.Checked && chkKeyUp.Checked;
                    bool isKeyDownOnly = chkKeyDown.Checked && !chkKeyUp.Checked;
                    bool isKeyUpOnly = !chkKeyDown.Checked && chkKeyUp.Checked;

                    if (!chkKeyDown.Checked && !chkKeyUp.Checked)
                    {
                        MessageBox.Show("請至少勾選『壓下』或『彈起』其中一個模式！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (isKeyPress) action.Type = ActionType.KeyPress;
                    else if (isKeyDownOnly) action.Type = ActionType.KeyDown;
                    else action.Type = ActionType.KeyUp;
                    break;

                case ActionType.RandomKey:
                    action.Type = ActionType.RandomKey;
                    action.RandomType = (RandomKeyType)cmbRandomType.SelectedValue;
                    break;

                case ActionType.LockMouse:
                    action.Type = ActionType.LockMouse;
                    action.IsLockActive = chkLock.Checked;
                    break;
            }

            lstActions.Items.Add(action);
        }

        private void txtKey_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            txtKey.Text = key.ToString();
            txtKey.Tag = (int)key;

            e.SuppressKeyPress = true;
            e.Handled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstActions.SelectedIndex != -1)
                lstActions.Items.RemoveAt(lstActions.SelectedIndex);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int index = lstActions.SelectedIndex;
            if (index <= 0) return;

            object item = lstActions.SelectedItem;
            lstActions.Items.RemoveAt(index);
            lstActions.Items.Insert(index - 1, item);
            lstActions.SelectedIndex = index - 1;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int index = lstActions.SelectedIndex;
            if (index < 0 || index >= lstActions.Items.Count - 1) return;

            object item = lstActions.SelectedItem;
            lstActions.Items.RemoveAt(index);
            lstActions.Items.Insert(index + 1, item);
            lstActions.SelectedIndex = index + 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtScriptName.Text) || txtScriptName.Text == "設定腳本名稱")
            {
                MessageBox.Show("請輸入腳本名稱！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CurrentScript.Name = txtScriptName.Text;
            CurrentScript.IsInfiniteLoop = rdoInfinite.Checked;
            CurrentScript.LoopCount = (int)numLoopCount.Value;
            CurrentScript.Actions.Clear();

            foreach (ScriptAction action in lstActions.Items)
            {
                CurrentScript.Actions.Add(action);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LstActions_MouseDown(object sender, MouseEventArgs e)
        {
            if (lstActions.Items.Count == 0) return;

            int index = lstActions.IndexFromPoint(e.X, e.Y);
            if (index != -1)
            {
                lstActions.SelectedIndex = index;
                lstActions.DoDragDrop(index, DragDropEffects.Move);
            }
        }

        private void LstActions_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void LstActions_DragDrop(object sender, DragEventArgs e)
        {
            int sourceIndex = (int)e.Data.GetData(typeof(int));
            Point point = lstActions.PointToClient(new Point(e.X, e.Y));
            int targetIndex = lstActions.IndexFromPoint(point);

            if (targetIndex < 0) targetIndex = lstActions.Items.Count - 1;
            if (sourceIndex == targetIndex) return;

            object item = lstActions.Items[sourceIndex];
            lstActions.Items.RemoveAt(sourceIndex);
            lstActions.Items.Insert(targetIndex, item);
            lstActions.SelectedIndex = targetIndex;
        }
    }
}