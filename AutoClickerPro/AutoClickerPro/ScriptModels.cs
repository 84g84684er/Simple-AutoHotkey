using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoClickerPro
{
    // 定義動作類型
    public enum ActionType
    {
        MouseClickLeft,
        MouseClickRight,
        KeyPress,       // 指定按鍵
        RandomKey,      // 隨機按鍵
        Delay,          // 延遲
        LockMouse,      // 鎖定滑鼠 (開啟/關閉)
    }

    // 定義隨機按鍵的種類
    public enum RandomKeyType
    {
        Number, // 0-9
        Letter, // A-Z
        Symbol  // 符號
    }

    // 單一行動作的定義
    [Serializable]
    public class ScriptAction
    {
        public ActionType Type { get; set; }

        // 滑鼠相關
        public int MouseX { get; set; }
        public int MouseY { get; set; }
        public bool UseCurrentPosition { get; set; }

        // 鍵盤相關
        public int KeyCode { get; set; } // 存 ASCII 或 Virtual Key
        public RandomKeyType RandomType { get; set; }

        // 其他
        public int DelayTime { get; set; } // 毫秒
        public bool IsLockActive { get; set; } // 用於 LockMouse 動作 (true=鎖, false=解)

        public override string ToString()
        {
            switch (Type)
            {
                case ActionType.MouseClickLeft:
                    return $"[滑鼠左鍵] {(UseCurrentPosition ? "當前位置" : $"{MouseX},{MouseY}")}";
                case ActionType.MouseClickRight:
                    return $"[滑鼠右鍵] {(UseCurrentPosition ? "當前位置" : $"{MouseX},{MouseY}")}";
                case ActionType.KeyPress:
                    return $"[按鍵] {((System.Windows.Forms.Keys)KeyCode)}";
                case ActionType.RandomKey:
                    return $"[隨機按鍵] {RandomType}";
                case ActionType.Delay:
                    return $"[延遲] {DelayTime} ms";
                case ActionType.LockMouse:
                    return $"[滑鼠鎖定] {(IsLockActive ? "鎖定" : "解鎖")}";
                default:
                    return "未知動作";
            }
        }
    }

    // 完整的腳本檔案
    [Serializable]
    public class ScriptProfile
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; } = true;
        public List<ScriptAction> Actions { get; set; } = new List<ScriptAction>();

        // --- 新增：循環控制設定 ---
        public bool IsInfiniteLoop { get; set; } = true; // 預設為無限循環
        public int LoopCount { get; set; } = 1;          // 預設執行 1 次
                                                         // ------------------------

        public override string ToString()
        {
            // 為了方便辨識，可以在名稱後面顯示循環狀態 (選用)
            string loopInfo = IsInfiniteLoop ? "[無限]" : $"[{LoopCount}次]";
            return $"{Name} {loopInfo}";
        }
    }
}