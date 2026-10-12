using System;
using System.Collections.Generic;

namespace AutoClickerPro
{
    public enum ActionType
    {
        MouseClickLeft,   // 左鍵點擊 (壓下+放開)
        MouseClickRight,  // 右鍵點擊 (壓下+放開)
        MouseLeftDown,    // 左鍵按住 (壓下)
        MouseLeftUp,      // 左鍵放開 (彈起)
        MouseRightDown,   // 右鍵按住 (壓下)
        MouseRightUp,     // 右鍵放開 (彈起)
        KeyPress,         // 鍵盤完整點擊 (壓下+放開)
        KeyDown,          // 鍵盤按住 (壓下)
        KeyUp,            // 鍵盤放開 (彈起)
        RandomKey,
        Delay,
        LockMouse,
        MouseMiddleClick, // 中鍵點擊 (壓下+放開)
        MouseMiddleDown,  // 中鍵按住 (壓下)
        MouseMiddleUp,    // 中鍵放開 (彈起)
        MouseWheelUp,     // 滾輪向上滾動
        MouseWheelDown    // 滾輪向下滾動
    }

    public enum RandomKeyType
    {
        Number,
        Letter,
        Symbol
    }

    [Serializable]
    public class ScriptAction
    {
        public ActionType Type { get; set; }
        public int MouseX { get; set; }
        public int MouseY { get; set; }
        public bool UseCurrentPosition { get; set; }
        public int KeyCode { get; set; }
        public RandomKeyType RandomType { get; set; }
        public int DelayTime { get; set; }
        public bool IsLockActive { get; set; }
        public int WheelAmount { get; set; } = 1; // 滾輪滾動格數 (舊版存檔缺少此欄位時預設為 1)

        public override string ToString()
        {
            string pos = UseCurrentPosition ? "當前位置" : $"座標({MouseX}, {MouseY})";
            switch (Type)
            {
                case ActionType.MouseMiddleClick:
                    return $"[滑鼠] 中鍵點擊 -> {pos}";
                case ActionType.MouseMiddleDown:
                    return $"[滑鼠] 中鍵按住 -> {pos}";
                case ActionType.MouseMiddleUp:
                    return $"[滑鼠] 中鍵放開 -> {pos}";
                case ActionType.MouseWheelUp:
                    return $"[滑鼠] 滾輪向上 {WheelAmount} 格 -> {pos}";
                case ActionType.MouseWheelDown:
                    return $"[滑鼠] 滾輪向下 {WheelAmount} 格 -> {pos}";
                case ActionType.MouseClickLeft:
                    return $"[滑鼠] 左鍵點擊 -> {(UseCurrentPosition ? "當前位置" : $"座標({MouseX}, {MouseY})")}";
                case ActionType.MouseClickRight:
                    return $"[滑鼠] 右鍵點擊 -> {(UseCurrentPosition ? "當前位置" : $"座標({MouseX}, {MouseY})")}";
                case ActionType.MouseLeftDown:
                    return $"[滑鼠] 左鍵按住 -> {(UseCurrentPosition ? "當前位置" : $"座標({MouseX}, {MouseY})")}";
                case ActionType.MouseLeftUp:
                    return $"[滑鼠] 左鍵放開 -> {(UseCurrentPosition ? "當前位置" : $"座標({MouseX}, {MouseY})")}";
                case ActionType.MouseRightDown:
                    return $"[滑鼠] 右鍵按住 -> {(UseCurrentPosition ? "當前位置" : $"座標({MouseX}, {MouseY})")}";
                case ActionType.MouseRightUp:
                    return $"[滑鼠] 右鍵放開 -> {(UseCurrentPosition ? "當前位置" : $"座標({MouseX}, {MouseY})")}";
                case ActionType.KeyPress:
                    return $"[鍵盤] 按下並放開 -> {((System.Windows.Forms.Keys)KeyCode)}";
                case ActionType.KeyDown:
                    return $"[鍵盤] 壓下按住 -> {((System.Windows.Forms.Keys)KeyCode)}";
                case ActionType.KeyUp:
                    return $"[鍵盤] 彈起放開 -> {((System.Windows.Forms.Keys)KeyCode)}";
                case ActionType.RandomKey:
                    return $"[隨機] 鍵盤按鍵 -> {RandomType}";
                case ActionType.Delay:
                    return $"[時間] 延遲 -> {DelayTime} ms";
                case ActionType.LockMouse:
                    return $"[系統] 滑鼠鎖定 -> {(IsLockActive ? "鎖定" : "解除")}";
                default:
                    return "未知動作";
            }
        }
    }

    [Serializable]
    public class ScriptProfile
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; } = true;
        public List<ScriptAction> Actions { get; set; } = new List<ScriptAction>();
        public bool IsInfiniteLoop { get; set; } = true;
        public int LoopCount { get; set; } = 1;

        public override string ToString()
        {
            string loopInfo = IsInfiniteLoop ? "[無限]" : $"[{LoopCount}次]";
            return $"{Name} {loopInfo}";
        }
    }
}