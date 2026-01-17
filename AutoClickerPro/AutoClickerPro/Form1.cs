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

        // 熱鍵 ID
        private const int HOTKEY_ID_START = 1;
        private const int HOTKEY_ID_STOP = 2;

        // --- 滑鼠鎖定專用變數 ---
        private static bool isGlobalLocked = false;
        private static Point lockedPosition = new Point(0, 0);
        private Thread mouseLockThread;

        public form1()
        {
            InitializeComponent();
            LoadScripts();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            NativeMethods.RegisterHotKey(this.Handle, HOTKEY_ID_START, 0, 0x71); // F2
            NativeMethods.RegisterHotKey(this.Handle, HOTKEY_ID_STOP, 0, 0x70);  // F1

            // 啟動保險執行緒
            StartLockThread();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            NativeMethods.UnregisterHotKey(this.Handle, HOTKEY_ID_START);
            NativeMethods.UnregisterHotKey(this.Handle, HOTKEY_ID_STOP);

            // 確保解除鎖定
            UnlockMouse();
            if (mouseLockThread != null && mouseLockThread.IsAlive) mouseLockThread.Abort();

            SaveScripts();
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

        // --- 核心：鎖定執行緒 ---
        private void StartLockThread()
        {
            mouseLockThread = new Thread(() =>
            {
                while (true)
                {
                    if (isGlobalLocked)
                    {
                        // 這裡不做 Cursor.Position = ... 因為那會造成震動
                        // 我們持續呼叫 ClipCursor 確保範圍被限制
                        // 雖然 ClipCursor 呼叫一次就有效，但為了防止被其他視窗事件重置，我們重複強制執行
                        DoClipCursor(lockedPosition);
                    }
                    Thread.Sleep(10); // 不需要太快，因為 ClipCursor 是系統級限制
                }
            });
            mouseLockThread.IsBackground = true;
            mouseLockThread.Start();
        }

        // 執行系統級鎖定
        private void DoClipCursor(Point p)
        {
            // 設定一個只有 1x1 大小的矩形，讓滑鼠哪裡都去不了
            NativeMethods.RECT rect = new NativeMethods.RECT
            {
                left = p.X,
                top = p.Y,
                right = p.X + 1,
                bottom = p.Y + 1
            };
            NativeMethods.ClipCursor(ref rect);
        }

        private void UnlockMouse()
        {
            isGlobalLocked = false;
            // 傳入 Zero 解除限制
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
            PlaySound(true);
            // 預設開始時不鎖定，除非腳本裡有寫
            isGlobalLocked = false;

            // 啟動腳本
            foreach (var script in activeScripts)
            {
                Task.Run(() => RunScript(script, cts.Token), cts.Token);
            }

            this.Text = "運行中 (F1 停止)";
            SetUIEnabled(false);
        }

        private void StopAllScripts()
        {
            if (!isRunning) return;
            cts?.Cancel();
            isRunning = false;

            // 立刻解除鎖定
            UnlockMouse();
            PlaySound(false);

            this.Text = "已停止";
            SetUIEnabled(true);
        }

        private void SetUIEnabled(bool enabled)
        {
            chkListScripts.Enabled = enabled;
            btnAdd.Enabled = enabled;
            btnEdit.Enabled = enabled;
        }

        private void RunScript(ScriptProfile script, CancellationToken token)
        {
            // 用一個變數記錄目前跑了幾次
            int currentIteration = 0;

            // 迴圈條件：
            // 1. 如果被取消 (F1) -> 停止
            // 2. 如果是有限循環 且 次數已達標 -> 停止
            while (!token.IsCancellationRequested)
            {
                // 檢查是否達到指定次數
                if (!script.IsInfiniteLoop && currentIteration >= script.LoopCount)
                {
                    // 任務完成，跳出迴圈
                    break;
                }

                // --- 執行單次腳本的所有動作 ---
                foreach (var action in script.Actions)
                {
                    if (token.IsCancellationRequested) break;

                    switch (action.Type)
                    {
                        case ActionType.MouseClickLeft:
                        case ActionType.MouseClickRight:
                            PerformMouseClick(action);
                            break;
                        case ActionType.KeyPress:
                            PerformKeyPress(action.KeyCode);
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
                                lockedPosition = Cursor.Position;
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

                // 跑完一輪，計數器 +1
                currentIteration++;
            }
        }

        private void PerformMouseClick(ScriptAction action)
        {
            int x = action.MouseX;
            int y = action.MouseY;

            // 處理鎖定與座標移動邏輯 (保持原本的 ClipCursor 邏輯)
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

            // 準備 Input 結構
            NativeMethods.INPUT[] inputs = new NativeMethods.INPUT[2];

            // 設定按下
            inputs[0].type = NativeMethods.INPUT_MOUSE;
            inputs[0].u.mi.dx = 0; // 使用當前位置
            inputs[0].u.mi.dy = 0;
            inputs[0].u.mi.dwFlags = (action.Type == ActionType.MouseClickLeft)
                ? NativeMethods.MOUSEEVENTF_LEFTDOWN
                : NativeMethods.MOUSEEVENTF_RIGHTDOWN;

            // 設定放開
            inputs[1].type = NativeMethods.INPUT_MOUSE;
            inputs[1].u.mi.dx = 0;
            inputs[1].u.mi.dy = 0;
            inputs[1].u.mi.dwFlags = (action.Type == ActionType.MouseClickLeft)
                ? NativeMethods.MOUSEEVENTF_LEFTUP
                : NativeMethods.MOUSEEVENTF_RIGHTUP;

            // 發送按下
            NativeMethods.SendInput(1, new NativeMethods.INPUT[] { inputs[0] }, Marshal.SizeOf(typeof(NativeMethods.INPUT)));

            // 遊戲對於滑鼠點擊通常也需要一點間隔
            Thread.Sleep(30);

            // 發送放開
            NativeMethods.SendInput(1, new NativeMethods.INPUT[] { inputs[1] }, Marshal.SizeOf(typeof(NativeMethods.INPUT)));
        }

        private void PerformKeyPress(int keyCode)
        {
            // 1. 取得掃描碼 (Scan Code)
            // 0 = MAPVK_VK_TO_VSC (Virtual Key to Virtual Scan Code)
            uint scanCode = NativeMethods.MapVirtualKey((uint)keyCode, 0);

            // 2. 準備按下 (Key Down)
            NativeMethods.INPUT[] inputsDown = new NativeMethods.INPUT[1];
            inputsDown[0].type = NativeMethods.INPUT_KEYBOARD;
            inputsDown[0].u.ki.wVk = (ushort)keyCode;
            inputsDown[0].u.ki.wScan = (ushort)scanCode;
            // 關鍵旗標：使用掃描碼 (KEYEVENTF_SCANCODE)
            inputsDown[0].u.ki.dwFlags = NativeMethods.KEYEVENTF_SCANCODE;

            // 如果是方向鍵等延伸鍵，需要加 EXTENDEDKEY 旗標，這裡簡化處理通用鍵
            if (scanCode == 0) inputsDown[0].u.ki.dwFlags = 0; // 防呆

            NativeMethods.SendInput(1, inputsDown, Marshal.SizeOf(typeof(NativeMethods.INPUT)));

            // *** 關鍵：遊戲需要按壓持續時間 ***
            // 許多遊戲如果不延遲，會認為是雜訊而忽略
            // 建議至少 30~50ms，這是人類按鍵的最短極限
            Thread.Sleep(50);

            // 3. 準備放開 (Key Up)
            NativeMethods.INPUT[] inputsUp = new NativeMethods.INPUT[1];
            inputsUp[0].type = NativeMethods.INPUT_KEYBOARD;
            inputsUp[0].u.ki.wVk = (ushort)keyCode;
            inputsUp[0].u.ki.wScan = (ushort)scanCode;
            // 關鍵旗標：掃描碼 + 放開
            inputsUp[0].u.ki.dwFlags = NativeMethods.KEYEVENTF_SCANCODE | NativeMethods.KEYEVENTF_KEYUP;

            NativeMethods.SendInput(1, inputsUp, Marshal.SizeOf(typeof(NativeMethods.INPUT)));
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

        // --- UI 事件 ---
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
            if (chkListScripts.SelectedIndex != -1)
            {
                allScripts.RemoveAt(chkListScripts.SelectedIndex);
                RefreshList();
            }
        }

        // --- 存檔與讀檔 ---
        private void SaveScripts()
        {
            try
            {
                // 儲存勾選狀態
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

        private void RefreshList()
        {
            chkListScripts.Items.Clear();
            foreach (var s in allScripts)
            {
                chkListScripts.Items.Add(s, s.IsEnabled);
            }
        }
        private void ChkListScripts_MouseDown(object sender, MouseEventArgs e)
        {
            // 1. 嚴格判斷點擊位置是否在項目上
            int index = chkListScripts.IndexFromPoint(e.Location);

            // 如果 index 是 -1 (ListBox.NoMatches)，代表點在空白處，直接離開
            if (index == -1 || index >= chkListScripts.Items.Count)
            {
                chkListScripts.SelectedIndex = -1; // 取消選取 (視需求可選)
                return;
            }

            // 2. 判斷點擊的是「勾選框」還是「文字」
            // 一般 CheckedListBox 的勾選框寬度約為 16~20 px
            const int CHECKBOX_WIDTH = 20;

            if (e.X < CHECKBOX_WIDTH)
            {
                // --- 點擊了勾選框 ---
                // 這裡什麼都不用做！
                // 因為 CheckOnClick = false 的原生行為就是「點框框會勾選/取消」。
                // 我們不需要手動介入，否則會發生「勾了又取消」的衝突 Bug。
            }
            else
            {
                // --- 點擊了文字區域 ---
                // 選取該項目 (反白)
                chkListScripts.SelectedIndex = index;

                // 啟動拖曳 (DoDragDrop 會暫停程式直到放開滑鼠，所以不會干擾勾選)
                chkListScripts.DoDragDrop(index, DragDropEffects.Move);
            }
        }

        private void ChkListScripts_DragOver(object sender, DragEventArgs e)
        {
            // 只有當拖曳的是同一類型的資料時才允許移動
            e.Effect = DragDropEffects.Move;
        }

        private void ChkListScripts_DragDrop(object sender, DragEventArgs e)
        {
            int sourceIndex = (int)e.Data.GetData(typeof(int));
            Point point = chkListScripts.PointToClient(new Point(e.X, e.Y));
            int targetIndex = chkListScripts.IndexFromPoint(point);

            // 如果拖到空白處，預設放到最後
            if (targetIndex < 0) targetIndex = chkListScripts.Items.Count - 1;

            // 防呆
            if (targetIndex < 0) targetIndex = 0;

            if (sourceIndex == targetIndex) return;

            // 修改資料順序
            ScriptProfile item = allScripts[sourceIndex];
            allScripts.RemoveAt(sourceIndex);
            allScripts.Insert(targetIndex, item);

            // 重新整理 UI
            RefreshList();

            // 恢復選取
            chkListScripts.SelectedIndex = targetIndex;
        }
        private void ChkListScripts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // 注意：這個事件發生在狀態「改變之前」
            // e.CurrentValue 是現在的狀態
            // e.NewValue 是即將變成的狀態

            if (e.Index >= 0 && e.Index < allScripts.Count)
            {
                // 根據 NewValue 來更新我們的資料物件
                bool isChecked = (e.NewValue == CheckState.Checked);
                ScriptProfile script = allScripts[e.Index];
                script.IsEnabled = isChecked;
            }
        }
        private void PlaySound(bool isStart)
        {
            // 使用系統內建音效，不需要外部檔案，且非蜂鳴器
            if (isStart)
            {
                // 類似 Windows 啟動或插入 USB 的聲音
                System.Media.SystemSounds.Asterisk.Play();
            }
            else
            {
                // 類似錯誤或停止的聲音
                System.Media.SystemSounds.Hand.Play();
            }
        }
    }
}