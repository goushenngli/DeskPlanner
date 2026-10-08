using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopCalendar.Services
{
    public static class WindowServiceExtensions
    {
        /// <summary>
        /// 查找已存活的窗口，找不到则通过 DI 容器创建
        /// </summary>
        public static T? GetOrCreateWindow<T>(this IServiceProvider serviceProvider) where T : Window
        {
            try
            {
                //检查 Application 实例是否存在
                if (Application.Current == null) return null;
                //尝试从内存存活列表中查找
                var window = Application.Current.Windows.OfType<T>().FirstOrDefault();
                if (window != null) return window;
                // 内存中没有，则使用 GetService<T> 尝试创建（未注册时返回 null，不会抛异常）
                return serviceProvider?.GetService<T>();
            }
            catch
            {
                return null;
            }
        }
    }
}