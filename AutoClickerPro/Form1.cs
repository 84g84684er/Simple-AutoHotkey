using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AutoClickerPro
{
    public partial class form1 : Form
    {
        private List<ScriptProfile> allScripts = new List<ScriptProfile>();
        private bool isRunning = false;
        private CancellationTokenSource cts;

        private const string SAVE_FILE = "scripts.xml";
        private const string SETTINGS_FILE = "settings.xml";

        private const int HOTKEY_ID_START = 1;
        private const int HOTKEY_ID_STOP = 2;

        private static bool isGlobalLocked = false;
        private static Point lockedPosition = new Point(0, 0);
        private Thread mouseLockThread;

        // 拖曳狀態專用變數
        private Point dragStartPoint = Point.Empty;
        private int dragIndex = -1;

        // 核心安全鎖旗標，用以完全隔離「點選文字」造成的勾選狀態變更
        private bool isInternalCheckChange = false;

        // 引入 Windows 多媒體 API 來播放系統內建的 USB 音效
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PlaySound(string pszSound, IntPtr hmod, uint fdwSound);

        private const uint SND_ASYNC = 0x0001;
        private const uint SND_ALIAS = 0x00010000;
        private const uint SND_NODEFAULT = 0x0002;

        public form1()
        {
            InitializeComponent();
            LoadScripts();
            LoadSettings();

            ThemeManager.ApplyTheme(this);
            UpdateThemeButtonText();

            // 確保清單元件維持「不可點文字勾選」設定
            chkListScripts.CheckOnClick = false;

            // 註冊全域點擊事件，使點擊外部元件時能取消反藍
            this.MouseDown += (s, e) => chkListScripts.SelectedIndex = -1;
            RegisterGlobalClickToUnselect(this);
        }

        private void RegisterGlobalClickToUnselect(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl != chkListScripts && !(ctrl is Button))
                {
                    ctrl.MouseDown += (s, e) => chkListScripts.SelectedIndex = -1;
                }
                if (ctrl.HasChildren)
                {
                    RegisterGlobalClickToUnselect(ctrl);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            NativeMethods.RegisterHotKey(this.Handle, HOTKEY_ID_START, 0, 0x71); // F2
            NativeMethods.RegisterHotKey(this.Handle, HOTKEY_ID_STOP, 0, 0x70);  // F1
            StartLockThread();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            NativeMethods.UnregisterHotKey(this.Handle, HOTKEY_ID_START);
            NativeMethods.UnregisterHotKey(this.Handle, HOTKEY_ID_STOP);

            UnlockMouse();
            if (mouseLockThread != null && mouseLockThread.IsAlive) mouseLockThread.Abort();

            SaveScripts();
            SaveSettings();
            base.OnFormClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                if (id == HOTKEY_ID_START) StartAllScripts();
                if (id == HOTKEY_ID_STOP) StopAllScripts();
            }
            base.WndProc(ref m);
        }

        private void LoadSettings()
        {
            if (!File.Exists(SETTINGS_FILE)) return;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ThemeManager.Theme));
                using (TextReader reader = new StreamReader(SETTINGS_FILE))
                {
                    ThemeManager.CurrentTheme = (ThemeManager.Theme)serializer.Deserialize(reader);
                }
            }
            catch { }
        }

        private void SaveSettings()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ThemeManager.Theme));
                using (TextWriter writer = new StreamWriter(SETTINGS_FILE))
                {
                    serializer.Serialize(writer, ThemeManager.CurrentTheme);
                }
            }
            catch { }
        }

        private void StartLockThread()
        {
            mouseLockThread = new Thread(() =>
            {
                while (true)
                {
                    if (isGlobalLocked)
                    {
                        // 核心修正：使用底層 API 讀取物理座標
                        NativeMethods.POINT curPos;
                        if (NativeMethods.GetCursorPos(out curPos))
                        {
                            // 只要滑鼠與按下 F2 時的基準點有任何一點偏離，瞬間極速拉回起點
                            if (curPos.X != lockedPosition.X || curPos.Y != lockedPosition.Y)
                            {
                                NativeMethods.SetCursorPos(lockedPosition.X, lockedPosition.Y);
                            }
                        }

                        // 輔助系統級 ClipCursor 邊界限制
                        DoClipCursor(lockedPosition);
                    }
                    Thread.Sleep(1);
                }
            });
            mouseLockThread.IsBackground = true;
            mouseLockThread.Start();
        }

        private void DoClipCursor(Point p)
        {
            NativeMethods.RECT rect = new NativeMethods.RECT
            {
                left = p.X,
                top = p.Y,
                right = p.X + 1,
                bottom = p.Y + 1
            };
            NativeMethods.ClipCursor(ref rect);
        }

        // ==========================================
        // 補回：實作解除滑鼠限制的 UnlockMouse Method
        // ==========================================
        private void UnlockMouse()
        {
            isGlobalLocked = false;
            // 傳入零指標 (IntPtr.Zero) 即可解開系統的 ClipCursor 限制
            NativeMethods.ClipCursor(IntPtr.Zero);
        }

        private void StartAllScripts()
        {
            if (isRunning) return;

            // 取得勾選腳本
            List<ScriptProfile> activeScripts = new List<ScriptProfile>();
            for (int i = 0; i < chkListScripts.Items.Count; i++)
            {
                if (chkListScripts.GetItemChecked(i))
                    activeScripts.Add((ScriptProfile)chkListScripts.Items[i]);
            }

            if (activeScripts.Count == 0)
            {
                MessageBox.Show("未勾選任何腳本");
                return;
            }

            isRunning = true;
            cts = new CancellationTokenSource();
            PlaySound(true); // 播 USB 插入聲

            // 核心修正：在按下 F2 的絕對瞬間，立刻記錄物理滑鼠座標
            lockedPosition = Cursor.Position;

            // 核心修正：如果勾選的腳本中含有「滑鼠鎖定」動作，在 F2 按下的瞬間立即鎖定
            bool hasLockAction = activeScripts.Any(s => s.Actions.Any(a => a.Type == ActionType.LockMouse && a.IsLockActive));
            if (hasLockAction)
            {
                isGlobalLocked = true;
                DoClipCursor(lockedPosition);
            }
            else
            {
                isGlobalLocked = false;
            }

            // 啟動腳本
            foreach (var script in activeScripts)
            {
                Task.Run(() => RunScript(script, cts.Token), cts.Token);
            }

            this.Text = "運行中 (F1 停止)";
            lblStatus.Text = "狀態：運行中";
            lblStatus.ForeColor = Color.ForestGreen;
            SetUIEnabled(false);
        }

        private void StopAllScripts()
        {
            if (!isRunning) return;
            cts?.Cancel();
            isRunning = false;

            UnlockMouse();
            PlaySound(false); // 播 USB 拔出聲

            this.Text = "已停止";
            lblStatus.Text = "狀態：已停止";
            lblStatus.ForeColor = Color.Gray;
            SetUIEnabled(true);
        }

        // 執行時也同步禁用刪除按鈕 (btnDel)，達到全方位安全保護
        private void SetUIEnabled(bool enabled)
        {
            chkListScripts.Enabled = enabled;
            btnAdd.Enabled = enabled;
            btnEdit.Enabled = enabled;
            btnDel.Enabled = enabled;
            btnToggleTheme.Enabled = enabled;
        }

        private void btnToggleTheme_Click(object sender, EventArgs e)
        {
            ThemeManager.CurrentTheme = ThemeManager.CurrentTheme == ThemeManager.Theme.Light
                ? ThemeManager.Theme.Dark
                : ThemeManager.Theme.Light;

            ThemeManager.ApplyTheme(this);
            UpdateThemeButtonText();
            SaveSettings();
        }

        private void UpdateThemeButtonText()
        {
            btnToggleTheme.Text = ThemeManager.CurrentTheme == ThemeManager.Theme.Dark ? "☀️ 淺色模式" : "🌙 深色模式";
        }

        private void RunScript(ScriptProfile script, CancellationToken token)
        {
            int currentIteration = 0;
            while (!token.IsCancellationRequested)
            {
                if (!script.IsInfiniteLoop && currentIteration >= script.LoopCount)
                {
                    break;
                }

                foreach (var action in script.Actions)
                {
                    if (token.IsCancellationRequested) break;

                    switch (action.Type)
                    {
                        case ActionType.MouseClickLeft:
                        case ActionType.MouseClickRight:
                        case ActionType.MouseLeftDown:
                        case ActionType.MouseLeftUp:
                        case ActionType.MouseRightDown:
                        case ActionType.MouseRightUp:
                            PerformMouseClick(action);
                            break;
                        case ActionType.KeyPress:
                            PerformKeyPress(action.KeyCode);
                            break;
                        case ActionType.KeyDown:
                            PerformKeyDown(action.KeyCode);
                            break;
                        case ActionType.KeyUp:
                            PerformKeyUp(action.KeyCode);
                            break;
                        case ActionType.RandomKey:
                            PerformRandomKey(action.RandomType);
                            break;
                        case ActionType.Delay:
                            Thread.Sleep(action.DelayTime);
                            break;
                        case ActionType.LockMouse:
                            if (action.IsLockActive)
                            {
                                // 核心修正：直接使用當初 F2 啟動瞬間儲存的座標，拒絕二次擷取造成的偏移
                                isGlobalLocked = true;
                                DoClipCursor(lockedPosition);
                            }
                            else
                            {
                                UnlockMouse();
                            }
                            break;
                    }

                    if (action.Type != ActionType.Delay) Thread.Sleep(10);
                }

                currentIteration++;
            }
        }

        private void PerformMouseClick(ScriptAction action)
        {
            int x = action.MouseX;
            int y = action.MouseY;

            if (action.UseCurrentPosition)
            {
                x = Cursor.Position.X;
                y = Cursor.Position.Y;
            }
            else
            {
                if (isGlobalLocked)
                {
                    lockedPosition = new Point(x, y);
                    DoClipCursor(lockedPosition);
                    Thread.Sleep(2);
                }
                Cursor.Position = new Point(x, y);
            }

            uint flags = 0;
            switch (action.Type)
            {
                case ActionType.MouseClickLeft:
                    SendMouseInput(NativeMethods.MOUSEEVENTF_LEFTDOWN);
                    Thread.Sleep(30);
                    SendMouseInput(NativeMethods.MOUSEEVENTF_LEFTUP);
                    return;
                case ActionType.MouseClickRight:
                    SendMouseInput(NativeMethods.MOUSEEVENTF_RIGHTDOWN);
                    Thread.Sleep(30);
                    SendMouseInput(NativeMethods.MOUSEEVENTF_RIGHTUP);
                    return;
                case ActionType.MouseLeftDown:
                    flags = NativeMethods.MOUSEEVENTF_LEFTDOWN;
                    break;
                case ActionType.MouseLeftUp:
                    flags = NativeMethods.MOUSEEVENTF_LEFTUP;
                    break;
                case ActionType.MouseRightDown:
                    flags = NativeMethods.MOUSEEVENTF_RIGHTDOWN;
                    break;
                case ActionType.MouseRightUp:
                    flags = NativeMethods.MOUSEEVENTF_RIGHTUP;
                    break;
            }

            if (flags != 0)
            {
                SendMouseInput(flags);
            }
        }

        private void SendMouseInput(uint flags)
        {
            NativeMethods.INPUT[] inputs = new NativeMethods.INPUT[1];
            inputs[0].type = NativeMethods.INPUT_MOUSE;
            inputs[0].u.mi.dx = 0;
            inputs[0].u.mi.dy = 0;
            inputs[0].u.mi.dwFlags = flags;
            NativeMethods.SendInput(1, inputs, Marshal.SizeOf(typeof(NativeMethods.INPUT)));
        }

        private void PerformKeyDown(int keyCode)
        {
            uint scanCode = NativeMethods.MapVirtualKey((uint)keyCode, 0);
            NativeMethods.INPUT[] inputsDown = new NativeMethods.INPUT[1];
            inputsDown[0].type = NativeMethods.INPUT_KEYBOARD;
            inputsDown[0].u.ki.wVk = (ushort)keyCode;
            inputsDown[0].u.ki.wScan = (ushort)scanCode;
            inputsDown[0].u.ki.dwFlags = NativeMethods.KEYEVENTF_SCANCODE;

            if (scanCode == 0) inputsDown[0].u.ki.dwFlags = 0;
            NativeMethods.SendInput(1, inputsDown, Marshal.SizeOf(typeof(NativeMethods.INPUT)));
        }

        private void PerformKeyUp(int keyCode)
        {
            uint scanCode = NativeMethods.MapVirtualKey((uint)keyCode, 0);
            NativeMethods.INPUT[] inputsUp = new NativeMethods.INPUT[1];
            inputsUp[0].type = NativeMethods.INPUT_KEYBOARD;
            inputsUp[0].u.ki.wVk = (ushort)keyCode;
            inputsUp[0].u.ki.wScan = (ushort)scanCode;
            inputsUp[0].u.ki.dwFlags = NativeMethods.KEYEVENTF_SCANCODE | NativeMethods.KEYEVENTF_KEYUP;

            NativeMethods.SendInput(1, inputsUp, Marshal.SizeOf(typeof(NativeMethods.INPUT)));
        }

        private void PerformKeyPress(int keyCode)
        {
            PerformKeyDown(keyCode);
            Thread.Sleep(50);
            PerformKeyUp(keyCode);
        }

        private void PerformRandomKey(RandomKeyType type)
        {
            Random rnd = new Random();
            int key = 0;
            switch (type)
            {
                case RandomKeyType.Number: key = rnd.Next(48, 58); break;
                case RandomKeyType.Letter: key = rnd.Next(65, 91); break;
                case RandomKeyType.Symbol:
                    int[] symbols = { 186, 187, 188, 189, 190, 191 };
                    key = symbols[rnd.Next(symbols.Length)];
                    break;
            }
            PerformKeyPress(key);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptEditor editor = new ScriptEditor();
            if (editor.ShowDialog() == DialogResult.OK)
            {
                allScripts.Add(editor.CurrentScript);
                RefreshList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (chkListScripts.SelectedIndex == -1) return;
            ScriptProfile script = (ScriptProfile)chkListScripts.SelectedItem;
            ScriptEditor editor = new ScriptEditor(script);
            if (editor.ShowDialog() == DialogResult.OK) RefreshList();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (chkListScripts.SelectedIndex == -1) return;

            ScriptProfile script = (ScriptProfile)chkListScripts.SelectedItem;

            DialogResult result = CustomMessageBox.Show(
                "確認刪除",
                $"您確定要刪除腳本「{script.Name}」嗎？\n此動作刪除後將無法還原！"
            );

            if (result == DialogResult.Yes)
            {
                allScripts.RemoveAt(chkListScripts.SelectedIndex);
                RefreshList();
            }
        }

        private void SaveScripts()
        {
            try
            {
                for (int i = 0; i < chkListScripts.Items.Count; i++)
                {
                    ScriptProfile script = (ScriptProfile)chkListScripts.Items[i];
                    script.IsEnabled = chkListScripts.GetItemChecked(i);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(List<ScriptProfile>));
                using (TextWriter writer = new StreamWriter(SAVE_FILE))
                {
                    serializer.Serialize(writer, allScripts);
                }
            }
            catch { }
        }

        private void LoadScripts()
        {
            if (!File.Exists(SAVE_FILE)) return;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ScriptProfile>));
                using (TextReader reader = new StreamReader(SAVE_FILE))
                {
                    allScripts = (List<ScriptProfile>)serializer.Deserialize(reader);
                }
                RefreshList();
            }
            catch { }
        }

        // 重構 RefreshList，使其在清空重建控制項時暫時開啟安全鎖，避開攔截
        private void RefreshList()
        {
            isInternalCheckChange = true;
            chkListScripts.Items.Clear();
            foreach (var s in allScripts)
            {
                chkListScripts.Items.Add(s, s.IsEnabled);
            }
            isInternalCheckChange = false;
        }

        // 精準分離點擊勾選與選取判定，只有點擊 checkbox 時才開啟安全鎖
        private void ChkListScripts_MouseDown(object sender, MouseEventArgs e)
        {
            int index = chkListScripts.IndexFromPoint(e.Location);
            if (index == -1 || index >= chkListScripts.Items.Count)
            {
                chkListScripts.SelectedIndex = -1;
                dragStartPoint = Point.Empty;
                dragIndex = -1;
                return;
            }

            const int CHECKBOX_WIDTH = 25; // 勾選框感應區
            if (e.X < CHECKBOX_WIDTH)
            {
                // 點擊左側勾選框：不反藍、不進行拖曳，且『開啟安全鎖』進行精確切換
                isInternalCheckChange = true;
                bool isChecked = chkListScripts.GetItemChecked(index);
                chkListScripts.SetItemChecked(index, !isChecked);
                allScripts[index].IsEnabled = !isChecked;
                isInternalCheckChange = false;

                dragStartPoint = Point.Empty;
                dragIndex = -1;
            }
            else
            {
                // 點擊文字：不進行勾選動作，只反藍選取並記錄位置供拖曳判定
                chkListScripts.SelectedIndex = index;
                dragStartPoint = e.Location;
                dragIndex = index;
            }
        }

        private void ChkListScripts_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || dragStartPoint == Point.Empty || dragIndex == -1)
                return;

            int dragWidth = SystemInformation.DragSize.Width;
            int dragHeight = SystemInformation.DragSize.Height;
            Rectangle dragRect = new Rectangle(
                dragStartPoint.X - dragWidth / 2,
                dragStartPoint.Y - dragHeight / 2,
                dragWidth,
                dragHeight
            );

            // 超過系統拖曳安全值，啟動拖曳
            if (!dragRect.Contains(e.Location))
            {
                chkListScripts.DoDragDrop(dragIndex, DragDropEffects.Move);
                dragStartPoint = Point.Empty;
                dragIndex = -1;
            }
        }

        // 拖曳進入元件的安全核可，這是讓 DragDrop 事件能夠被引發的關鍵
        private void ChkListScripts_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(int)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ChkListScripts_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(int)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        // 重整拖曳落點與資料排序逻辑，修復排序無效的問題
        private void ChkListScripts_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(int)))
            {
                int sourceIndex = (int)e.Data.GetData(typeof(int));
                Point point = chkListScripts.PointToClient(new Point(e.X, e.Y));
                int targetIndex = chkListScripts.IndexFromPoint(point);

                // 如果拖曳到最下方空白處，落點為最後一個
                if (targetIndex < 0) targetIndex = chkListScripts.Items.Count - 1;
                if (targetIndex < 0) targetIndex = 0;

                if (sourceIndex == targetIndex) return;

                // 儲存其它項目的勾選狀態以防在重整時丟失
                for (int i = 0; i < chkListScripts.Items.Count; i++)
                {
                    var s = (ScriptProfile)chkListScripts.Items[i];
                    s.IsEnabled = chkListScripts.GetItemChecked(i);
                }

                // 更改資料順序
                ScriptProfile item = allScripts[sourceIndex];
                allScripts.RemoveAt(sourceIndex);
                allScripts.Insert(targetIndex, item);

                // 重新整理 UI 並保持反藍選取
                RefreshList();
                chkListScripts.SelectedIndex = targetIndex;
            }
        }

        // 在此攔截並過濾所有非安全鎖控制下的 native 勾選事件
        private void ChkListScripts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!isInternalCheckChange)
            {
                // 阻擋非 checkbox 點擊引發的 native 狀態切換 (關閉點擊文字與雙擊造成的變更)
                e.NewValue = e.CurrentValue;
                return;
            }

            if (e.Index >= 0 && e.Index < allScripts.Count)
            {
                bool isChecked = (e.NewValue == CheckState.Checked);
                ScriptProfile script = allScripts[e.Index];
                script.IsEnabled = isChecked;
            }
        }

        // 啟動/停止音效更改為系統標準 USB 連接/中斷的物理音效
        private void PlaySound(bool isStart)
        {
            try
            {
                if (isStart)
                {
                    // 播放 Windows 系統事件：USB 插入音效 (DeviceConnect)
                    PlaySound("DeviceConnect", IntPtr.Zero, SND_ALIAS | SND_ASYNC | SND_NODEFAULT);
                }
                else
                {
                    // 播放 Windows 系統事件：USB 拔出音效 (DeviceDisconnect)
                    PlaySound("DeviceDisconnect", IntPtr.Zero, SND_ALIAS | SND_ASYNC | SND_NODEFAULT);
                }
            }
            catch
            {
                // 防呆：若因音效設定損毀等極端狀況，向下相容使用一般系統音效
                if (isStart) System.Media.SystemSounds.Asterisk.Play();
                else System.Media.SystemSounds.Hand.Play();
            }
        }
    }

    // ==========================================
    // 客製化 MessageBox 類別 (自動適配當前黑白主題)
    // ==========================================
    public class CustomMessageBox : Form
    {
        private Label lblMessage;
        private Button btnYes;
        private Button btnNo;

        public CustomMessageBox(string title, string message)
        {
            this.Text = title;
            this.Size = new Size(380, 160);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;

            lblMessage = new Label();
            lblMessage.Text = message;
            lblMessage.Location = new Point(20, 20);
            lblMessage.Size = new Size(330, 50);
            lblMessage.Font = new Font("Microsoft JhengHei", 9.5F);

            btnYes = new Button();
            btnYes.Text = "確定";
            btnYes.DialogResult = DialogResult.Yes;
            btnYes.Location = new Point(160, 80);
            btnYes.Size = new Size(90, 32);
            btnYes.Font = new Font("Microsoft JhengHei", 9F);

            btnNo = new Button();
            btnNo.Text = "取消";
            btnNo.DialogResult = DialogResult.No;
            btnNo.Location = new Point(260, 80);
            btnNo.Size = new Size(90, 32);
            btnNo.Font = new Font("Microsoft JhengHei", 9F);

            this.Controls.Add(lblMessage);
            this.Controls.Add(btnYes);
            this.Controls.Add(btnNo);

            this.AcceptButton = btnYes;
            this.CancelButton = btnNo;

            // 套用目前程式啟用中的主題 (亮/暗色，包含標題列關閉按鈕與右鍵選單)
            ThemeManager.ApplyTheme(this);
        }

        // 視窗顯示時播放系統標準 Exclamation 驚嘆號警告音
        public static DialogResult Show(string title, string message)
        {
            using (var msgBox = new CustomMessageBox(title, message))
            {
                // 在確認視窗彈出的瞬間，同步撥放系統警示音效
                System.Media.SystemSounds.Exclamation.Play();
                return msgBox.ShowDialog();
            }
        }
    }
}