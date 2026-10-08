using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

// TODO 搞懂这一段，至少半懂，知道个原理
namespace DesktopCalendar.Helpers
{
    public static class WindowPenetrateHelper
    {
        public static readonly DependencyProperty IsPenetratableProperty =
            DependencyProperty.RegisterAttached(
                "IsPenetratable",
                typeof(bool),
                typeof(WindowPenetrateHelper),
                new FrameworkPropertyMetadata(false, OnIsPenetratableChanged));

        public static bool GetIsPenetratable(DependencyObject obj) => (bool)obj.GetValue(IsPenetratableProperty);
        public static void SetIsPenetratable(DependencyObject obj, bool value) => obj.SetValue(IsPenetratableProperty, value);

        private static void OnIsPenetratableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window) return;

            bool isPenetratable = (bool)e.NewValue;

            if (window.IsLoaded)
            {
                ApplyPenetrate(window, isPenetratable);
            }
            else
            {
                // 确保 Handle 已创建
                RoutedEventHandler loadedHandler = null!;
                loadedHandler = (s, args) =>
                {
                    window.Loaded -= loadedHandler;
                    ApplyPenetrate(window, isPenetratable);
                };
                window.Loaded += loadedHandler;
            }
        }

        private static void ApplyPenetrate(Window window, bool isPenetratable)
        {
            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero) return;

            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            if (isPenetratable)
            {
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT | WS_EX_LAYERED);
            }
            else
            {
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle & ~WS_EX_TRANSPARENT);
            }
        }

        #region Win32 API
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int WS_EX_LAYERED = 0x00080000;

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        #endregion
    }
}