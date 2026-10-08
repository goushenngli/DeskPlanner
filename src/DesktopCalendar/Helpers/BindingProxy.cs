using System.Windows;

namespace DesktopCalendar.Helpers
{
    /// <summary>
    /// MVVM 跨树数据绑定中转代理类 (BindingProxy)
    /// 
    /// 【解决什么问题】
    /// 1. ContextMenu / Popup：由 Popup 宿主创建独立顶层 HWND，视觉树与主窗口断开。
    ///    （注意：虽然内联定义的 ContextMenu 能通过逻辑树继承当前依附控件的 DataContext，
    ///    但由于视觉树断层，无法使用 RelativeSource FindAncestor 向上跨树查找到主窗口的 ViewModel。）
    /// 2. DataGridColumn：继承自 DependencyObject，不是 UIElement，不在视觉树和逻辑树中，
    ///    不参与 DataContext 隐式继承，且 RelativeSource FindAncestor 同样无法向上查找。
    /// 
    /// 【Freezable 的作用与原理】
    /// Freezable 被放入 ResourceDictionary 时，会通过 DependencyObject 的 InheritanceContext 
    /// 机制与资源宿主（Window / UserControl）建立关联，从而具备访问宿主 DataContext 的能力。
    /// BindingProxy 利用这一点，在资源字典中捕获 ViewModel，再供离树控件通过 
    /// {StaticResource} 显式引用。
    /// 
    /// 【使用方法】
    /// 1. 在 Window.Resources / UserControl.Resources 中注册：
    ///    <helpers:BindingProxy x:Key="MainWindowVM" Data="{Binding}" />
    /// 2. 在离树控件中通过 StaticResource 指定 Source：
    ///    <ContextMenu DataContext="{Binding Data, Source={StaticResource MainWindowVM}}">
    ///        <MenuItem Header="显示" Command="{Binding ShowMainWindowCommand}" />
    ///    </ContextMenu>
    /// 
    /// 【注意事项】
    /// 1. StaticResource 在 XAML 解析到该节点时实例化，必须定义在引用它的控件之前。
    /// 2. BindingProxy 内部的 Data="{Binding}" 是动态绑定，会在宿主 DataContext 
    ///    变化时自动更新，不要求定义时已有值。
    /// </summary>
    public class BindingProxy : Freezable
    {
        /// <summary>
        /// 重写 Freezable 要求的核心抽象方法。
        /// 当 WPF 在内部进行资源克隆、数据绑定变化更新或对象深拷贝时，会调用此方法创建全新的代理实例。
        /// </summary>
        /// <returns>返回 BindingProxy 的新实例。</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        /// <summary>
        /// 存放 ViewModel 或其他上下文数据对象的 CLR 属性包装器。
        /// </summary>
        public object Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        /// <summary>
        /// 注册 Data 依赖属性。
        /// 允许在 XAML 中使用 `Data="{Binding}"` 将宿主当前的整块 DataContext 捕获并存储至此属性中。
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(
                nameof(Data),
                typeof(object),
                typeof(BindingProxy),
                new UIPropertyMetadata(null));
    }
}