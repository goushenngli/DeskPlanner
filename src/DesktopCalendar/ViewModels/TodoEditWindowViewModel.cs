using System;
using System.Collections.Generic;
using System.Text;
using DesktopCalendar.Interfaces;
using CommunityToolkit.Mvvm.Input;

namespace DesktopCalendar.ViewModels
{
    internal partial class TodoEditWindowViewModel
    {
        private readonly IWindowService _windowService;
        public TodoEditWindowViewModel(IWindowService windowService)
        {
            _windowService = windowService;
        }
        
        [RelayCommand]
        public void Exit()
        {
            _windowService.CloseWindow(this);
        }
    }
}
