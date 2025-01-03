﻿using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using POS.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using POS.Services;

namespace POS
{

    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        public PaymentViewModel PaymentViewModel { get; set; } = new PaymentViewModel();
        public INavigation navigate { get; set; }
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window2 = new ShellWindow();
            //m_window.Activate();
            //navigate = m_window;
            m_window2.Activate();
        }

        //protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        //{
        //    m_window = new MainWindow();
        //    var root = new Frame();
        //    m_window.Content = root;
        //    var name = "POS.Views.Menu2";
        //    var type = Type.GetType(name);
        //    m_window.Activate();
        //    root.Navigate(type);

           

            //m_window = new LoginWindow();
            //Frame rootFrame = new Frame();
            //rootFrame.NavigationFailed += OnNavigationFailed;
            //// Navigate to the first page, configuring the new page
            //// by passing required information as a navigation parameter
            //rootFrame.Navigate(typeof(MainPage), args.Arguments);

            //// Place the frame in the current Window
            //m_window.Content = rootFrame;
            //m_window.Activate();
        //}

        //void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        //{
        //    throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        //}

        internal Shell m_window { get; set; }
        internal ShellWindow m_window2 { get; set; }
    }
}
