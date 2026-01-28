using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClickerPro
{
    public partial class ScriptEditor : Form
    {
        public ScriptProfile CurrentScript { get; set; }

        public ScriptEditor(ScriptProfile script = null)
        {
            InitializeComponent();

            // --- 1. 設定動作類型 (ActionType) 的中文選項 ---
            var actionItems = new[]
            {
        new { Name = "滑鼠左鍵點擊", Value = ActionType.MouseClickLeft },
        new { Name = "滑鼠右鍵點擊", Value = ActionType.MouseClickRight },
        new { Name = "模擬鍵盤按鍵", Value = ActionType.KeyPress },
        new { Name = "輸入隨機按鍵", Value = ActionType.RandomKey },
        new { Name = "等待延遲 (毫秒)", Value = ActionType.Delay },
        new { Name = "滑鼠鎖定控制", Value = ActionType.LockMouse }
    };

            cmbActionType.DataSource = actionItems;
            cmbActionType.DisplayMember = "Name";  // 顯示給用戶看的屬性名稱
            cmbActionType.ValueMember = "Value";   // 程式背後實際用的值屬性名稱

            // --- 2. 設定隨機類型 (RandomKeyType) 的中文選項 ---
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
            // --- 3. 初始化載入資料 ---
            if (script != null)
            {
                CurrentScript = script;
                txtScriptName.Text = script.Name;
                if (script.IsInfiniteLoop)
                    rdoInfinite.Checked = true;
                else
                    rdoCount.Checked = true;

                // 載入次數 (防呆：如果是 0 改成 1)
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
            }


            // 綁定事件 (如果你沒在設計畫面綁定，就要寫這行)
            txtKey.KeyDown += txtKey_KeyDown;

            // *** 新增：當切換到「模擬按鍵」模式時，檢查 txtKey 是否有值 ***
            cmbActionType.SelectedIndexChanged += (s, e) =>
            {
                UpdateUIState();
                // 如果切換到按鍵模式，且還沒設定過按鍵，提示使用者
                if ((ActionType)cmbActionType.SelectedValue == ActionType.KeyPress)
                {
                    if (txtKey.Tag == null)
                    {
                        txtKey.Text = "請點擊並按下按鍵";
                    }
                }
            };
            // 初始化 UI 狀態
            UpdateUIState();
        }
        private void ToggleLoopUI()
        {
            // 只有選了「指定次數」時，才能調整數字框
            numLoopCount.Enabled = rdoCount.Checked;
        }
        private void UpdateUIState()
        {
            if (cmbActionType.SelectedValue == null) return;

            // 注意：這裡要改成 SelectedValue
            ActionType type = (ActionType)cmbActionType.SelectedValue;

            // (原本的 UI 隱藏/顯示邏輯保持不變，例如...)
            // panelMouse.Visible = (type == ActionType.MouseClickLeft || type == ActionType.MouseClickRight);
            // panelKey.Visible = (type == ActionType.KeyPress);
            // ... 依此類推
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptAction action = new ScriptAction();

            // *** 修改點：使用 SelectedValue ***
            action.Type = (ActionType)cmbActionType.SelectedValue;

            // 讀取 UI 數值
            if (action.Type == ActionType.Delay)
                action.DelayTime = (int)numDelay.Value;

            if (action.Type == ActionType.MouseClickLeft || action.Type == ActionType.MouseClickRight)
            {
                action.UseCurrentPosition = chkCurrentPos.Checked;
                int.TryParse(txtX.Text, out int x);
                int.TryParse(txtY.Text, out int y);
                action.MouseX = x;
                action.MouseY = y;
            }

            if (action.Type == ActionType.KeyPress)
            {
                // 防呆：如果 Tag 是空的，代表使用者沒按過鍵
                if (txtKey.Tag == null)
                {
                    MessageBox.Show("請先點擊文字框，並按下鍵盤上的一個按鍵！");
                    return; // 中斷，不給加入
                }

                // 從 Tag 取出 int
                action.KeyCode = (int)txtKey.Tag;
            }

            if (action.Type == ActionType.RandomKey)
            {
                // *** 修改點：使用 SelectedValue ***
                action.RandomType = (RandomKeyType)cmbRandomType.SelectedValue;
            }

            if (action.Type == ActionType.LockMouse)
            {
                action.IsLockActive = chkLock.Checked;
            }

            lstActions.Items.Add(action);
        }

        private void txtKey_KeyDown(object sender, KeyEventArgs e)
        {
            // 1. 取得按下的按鍵代碼 (例如 Keys.A, Keys.F1)
            Keys key = e.KeyCode;

            // 2. 顯示按鍵名稱給使用者看
            txtKey.Text = key.ToString();

            // 3. 重要：將按鍵的數值 (int) 存入 Tag 屬性
            // 我們之後存檔時，不是讀 Text，而是讀這個 Tag
            txtKey.Tag = (int)key;

            // 4. 重要：告訴系統「這個按鍵我處理掉了」，防止發出系統提示音或輸入文字
            e.SuppressKeyPress = true;
            e.Handled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstActions.SelectedIndex != -1)
                lstActions.Items.RemoveAt(lstActions.SelectedIndex);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtScriptName.Text))
            {
                MessageBox.Show("請輸入腳本名稱");
                return;
            }

            CurrentScript.Name = txtScriptName.Text;
            // --- 儲存循環設定 ---
            CurrentScript.IsInfiniteLoop = rdoInfinite.Checked;
            CurrentScript.LoopCount = (int)numLoopCount.Value;
            // ------------------
            CurrentScript.Actions.Clear();

            // 依照 ListBox 目前的順序存入 Actions
            foreach (ScriptAction action in lstActions.Items)
            {
                CurrentScript.Actions.Add(action);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void LstActions_MouseDown(object sender, MouseEventArgs e)
        {
            if (lstActions.Items.Count == 0) return;

            int index = lstActions.IndexFromPoint(e.X, e.Y);
            if (index != -1)
            {
                // 選中該項目
                lstActions.SelectedIndex = index;
                // 開始拖曳，傳遞索引
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

            // 如果拖到下面空白處，視為移動到最後一個
            if (targetIndex < 0) targetIndex = lstActions.Items.Count - 1;

            if (sourceIndex == targetIndex) return;

            // --- 修改資料 (UI 上的 Items) ---
            // 注意：這裡我們直接操作 ListBox.Items，也要操作 CurrentScript.Actions
            // 但因為 ScriptEditor 最終儲存是看 lstActions 的內容重寫 CurrentScript，
            // 為了即時顯示，我們直接對調 ListBox 的項目即可。

            object item = lstActions.Items[sourceIndex];
            lstActions.Items.RemoveAt(sourceIndex);
            lstActions.Items.Insert(targetIndex, item);

            // 保持選取
            lstActions.SelectedIndex = targetIndex;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}