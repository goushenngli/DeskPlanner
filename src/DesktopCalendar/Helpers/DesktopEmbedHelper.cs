using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

// TODO 搞懂这一段，至少半懂，知道个原理
// TODO 窗口置底有问题，有时候不显示
namespace DesktopCalendar.Helpers
{
    public static class DesktopEmbedHelper
    {
        public static readonly DependencyProperty IsEmbeddedInDesktopProperty =
            DependencyProperty.RegisterAttached(
                "IsEmbeddedInDesktop",
                typeof(bool),
                typeof(DesktopEmbedHelper),
                new PropertyMetadata(false, OnIsEmbeddedInDesktopChanged));
        public static bool GetIsEmbeddedInDesktop(DependencyObject obj) => (bool)obj.GetValue(IsEmbeddedInDesktopProperty);
        public static void SetIsEmbeddedInDesktop(DependencyObject obj, bool value) => obj.SetValue(IsEmbeddedInDesktopProperty, value);
        private static void OnIsEmbeddedInDesktopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window) return;
            bool isEmbed = (bool)e.NewValue;
            if (window.IsLoaded)
            {
                ApplyDesktopEmbed(window, isEmbed);
            }
            else
            {
                RoutedEventHandler loadedHandler = null!;
                loadedHandler = (s, args) =>
                {
                    window.Loaded -= loadedHandler;
                    ApplyDesktopEmbed(window, isEmbed);
                };
                window.Loaded += loadedHandler;
            }
        }
        private static void ApplyDesktopEmbed(Window window, bool isEmbed)
        {
            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero) return;
            HwndSource source = HwndSource.FromHwnd(hwnd);
            if (source == null) return;
            if (isEmbed)
            {
                // 1. 获取 Progman/Shell 窗口句柄
                IntPtr progman = FindWindow("Progman", null);
                // 2. 将窗口的 Owner 设置为 Progman，让 Windows 认定它属于桌面背景层
                SetWindowLongPtr(hwnd, GWLP_HWNDPARENT, progman);
                // 3. 将窗口层级置于最底层 (HWND_BOTTOM)
                SetWindowPos(hwnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
                // 4. Hook 窗口消息，拦截 Win+D 最小化信号
                source.RemoveHook(WndProc); // 避免重复添加
                source.AddHook(WndProc);
            }
            else
            {
                // 1. 移除消息 Hook
                source.RemoveHook(WndProc);

                // 2. 还原 Owner 为无
                SetWindowLongPtr(hwnd, GWLP_HWNDPARENT, IntPtr.Zero);

                // 3. 恢复为正常窗口层级 (HWND_NOTOPMOST)
                SetWindowPos(hwnd, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
            }
        }

        /// <summary>
        /// 消息钩子：拦截 Win+D 和系统最小化消息，保持窗口置底且可见
        /// </summary>
        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020;
            const int WM_WINDOWPOSCHANGING = 0x0046;

            // 1. 拦截通过系统命令发起的最小化 (如 Win+D / 显示桌面)
            if (msg == WM_SYSCOMMAND && (wParam.ToInt32() & 0xFFF0) == SC_MINIMIZE)
            {
                handled = true; // 阻止最小化动作
                return IntPtr.Zero;
            }

            // 2. 拦截 Z-Order 改变，强制保持在底层 (HWND_BOTTOM)
            if (msg == WM_WINDOWPOSCHANGING)
            {
                WINDOWPOS pos = Marshal.PtrToStructure<WINDOWPOS>(lParam);
                pos.hwndInsertAfter = HWND_BOTTOM; // 强制保持在 Bottom
                Marshal.StructureToPtr(pos, lParam, false);
            }

            return IntPtr.Zero;
        }

        #region Win32 API 声明

        private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        private const int GWLP_HWNDPARENT = -8;

        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOACTIVATE = 0x0010;

        [StructLayout(LayoutKind.Sequential)]
        private struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string? lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        // 兼容 32 位与 64 位系统的 SetWindowLongPtr 封装
        private static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            return IntPtr.Size == 8
                ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong)
                : new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        #endregion
    }
}