using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoClickerPro
{
    public static class ThemeManager
    {
        public enum Theme { Light, Dark }
        public static Theme CurrentTheme { get; set; } = Theme.Light;

        // 深色主題顏色配置
        private static readonly Color DarkBg = Color.FromArgb(30, 30, 30);
        private static readonly Color DarkFg = Color.FromArgb(240, 240, 240);
        private static readonly Color DarkControlBg = Color.FromArgb(45, 45, 45);
        private static readonly Color DarkBtnBg = Color.FromArgb(62, 62, 66);
        private static readonly Color DarkBorder = Color.FromArgb(85, 85, 85);

        // 淺色主題顏色配置
        private static readonly Color LightBg = Color.FromArgb(245, 245, 245);
        private static readonly Color LightFg = Color.FromArgb(33, 33, 33);
        private static readonly Color LightControlBg = Color.White;
        private static readonly Color LightBtnBg = Color.FromArgb(225, 225, 225);

        // Win32 API 引入 - 用於修改系統視窗與右上角控制項 (最小化、關閉) 的深淺色樣式
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        // 用於修改系統右鍵選單、滾動條的深淺色主題
        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        private static extern int SetPreferredAppMode(int appMode);

        [DllImport("uxtheme.dll", EntryPoint = "#136")]
        private static extern void FlushMenuThemes();

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        public static void ApplyTheme(Form form)
        {
            bool useDark = (CurrentTheme == Theme.Dark);

            // 1. 套用 Windows 系統視窗標題列與右上角控制按鈕 (最小化、關閉、視窗框線) 樣式
            SetTitleBarTheme(form.Handle, useDark);

            // 2. 套用視窗系統右鍵選單 (如點擊視窗標題列或工作列圖示彈出之選單) 的深淺色樣式
            try
            {
                SetPreferredAppMode(useDark ? 2 : 3); // 2 = ForceDark, 3 = ForceLight
                FlushMenuThemes();
            }
            catch { }

            // 3. 套用標準控制項背景色與前景色
            if (CurrentTheme == Theme.Dark)
            {
                form.BackColor = DarkBg;
                form.ForeColor = DarkFg;
            }
            else
            {
                form.BackColor = LightBg;
                form.ForeColor = LightFg;
            }

            foreach (Control ctrl in form.Controls)
            {
                ApplyToControl(ctrl);
            }
        }

        private static void SetTitleBarTheme(IntPtr handle, bool useDark)
        {
            try
            {
                int build = Environment.OSVersion.Version.Build;
                int attr = DWMWA_USE_IMMERSIVE_DARK_MODE;
                if (build < 18985) // Windows 10 版本 18985 (19H2) 以前
                {
                    attr = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                }

                int value = useDark ? 1 : 0;
                DwmSetWindowAttribute(handle, attr, ref value, sizeof(int));
            }
            catch { }
        }

        private static void ApplyToControl(Control ctrl)
        {
            if (CurrentTheme == Theme.Dark)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = DarkBtnBg;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = DarkBorder;
                }
                else if (ctrl is ListBox || ctrl is CheckedListBox || ctrl is TextBox || ctrl is NumericUpDown || ctrl is ComboBox)
                {
                    ctrl.BackColor = DarkControlBg;
                    ctrl.ForeColor = DarkFg;

                    if (ctrl is TextBox txt) txt.BorderStyle = BorderStyle.FixedSingle;

                    if (ctrl is ComboBox cmb)
                    {
                        cmb.FlatStyle = FlatStyle.Flat;
                    }
                }
                else if (ctrl is Panel || ctrl is GroupBox)
                {
                    ctrl.BackColor = Color.Transparent;
                    ctrl.ForeColor = DarkFg;
                }
                else if (ctrl is Label || ctrl is CheckBox || ctrl is RadioButton)
                {
                    ctrl.ForeColor = DarkFg;
                }
            }
            else
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = LightBtnBg;
                    btn.ForeColor = LightFg;
                    btn.FlatStyle = FlatStyle.Standard;
                }
                else if (ctrl is ListBox || ctrl is CheckedListBox || ctrl is TextBox || ctrl is NumericUpDown || ctrl is ComboBox)
                {
                    ctrl.BackColor = LightControlBg;
                    ctrl.ForeColor = LightFg;

                    if (ctrl is TextBox txt) txt.BorderStyle = BorderStyle.Fixed3D;

                    if (ctrl is ComboBox cmb)
                    {
                        cmb.FlatStyle = FlatStyle.Standard;
                    }
                }
                else if (ctrl is Panel || ctrl is GroupBox)
                {
                    ctrl.BackColor = Color.Transparent;
                    ctrl.ForeColor = LightFg;
                }
                else if (ctrl is Label || ctrl is CheckBox || ctrl is RadioButton)
                {
                    ctrl.ForeColor = LightFg;
                }
            }

            if (ctrl.HasChildren)
            {
                foreach (Control child in ctrl.Controls)
                {
                    ApplyToControl(child);
                }
            }
        }
    }
}