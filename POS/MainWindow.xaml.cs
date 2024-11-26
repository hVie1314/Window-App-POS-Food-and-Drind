using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using Microsoft.UI;
using POS.Views;
using Windows.UI.WindowManagement;
using AppWindow = Microsoft.UI.Windowing.AppWindow;

namespace POS
{
    public sealed partial class MainWindow : Window
    {
        private AppWindow _apw;
        AppWindow m_appWindow;
        private OverlappedPresenter _presenter;

        public MainWindow()
        {
            this.InitializeComponent();
            //// Set FullScreen
            ////================================================================================================
            //m_appWindow = GetAppWindowForCurrentWindow();
            //m_appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            ////================================================================================================
            //// Initialize AppWindow and OverlappedPresenter
            //GetAppWindowAndPresenter();
            //// Set fixed size and disable resizing
            //SetFixedSize(1920, 1080);
        }


        private void GetAppWindowAndPresenter()
        {
            var hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            _apw = AppWindow.GetFromWindowId(myWndId);
            _presenter = _apw.Presenter as OverlappedPresenter;
        }
        private void SetFixedSize(int width, int height)
        {
            if (_presenter != null)
            {
                _presenter.IsResizable = false;
                _apw.Resize(new Windows.Graphics.SizeInt32(width, height));
                _apw.SetPresenter(AppWindowPresenterKind.Overlapped);
                // Removed the line causing the error as there is no SetPreferredMinSize method in AppWindow
                //_apw.SetPreferredMinSize(new Windows.Graphics.SizeInt32(width, height));
                // _apw.SetPreferredMaxSize(new Windows.Graphics.SizeInt32(width, height));
            }
        }

        //================================================================================================
        //Lấy set Fullcreen cho AppWindow
        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WindowId myWndId2 = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);

            return AppWindow.GetFromWindowId(myWndId2);
        }

        private void SwitchPresenter_FullScreen(object sender, RoutedEventArgs e)
        {
            m_appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
        }
        //================================================================================================
        //private void Window_Activated(object sender, WindowActivatedEventArgs args)
        //{
        //    MainFrame.Navigate(typeof(Menu));
        //}
    }
}