using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using POS;

namespace POS
{
        public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
<<<<<<< Updated upstream
            m_window = new LoginWindow();
            Frame rootFrame = new Frame();
            rootFrame.NavigationFailed += OnNavigationFailed;
            // Navigate to the first page, configuring the new page
            // by passing required information as a navigation parameter
            rootFrame.Navigate(typeof(MainPage), args.Arguments);

            // Place the frame in the current Window
            m_window.Content = rootFrame;
            m_window.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private Window m_window;
=======
            // Tạo cửa sổ chính ShellWindow
            var shellWindow = new ShellWindow();
            shellWindow.Activate();
        }
>>>>>>> Stashed changes
    }
}
