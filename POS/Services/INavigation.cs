﻿using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace POS.Services
{
    public interface INavigation
    {
        NavigationViewItem GetCurrentNavigationViewItem();

        List<NavigationViewItem> GetNavigationViewItems();

        List<NavigationViewItem> GetNavigationViewItems(Type type);

        List<NavigationViewItem> GetNavigationViewItems(Type type, string title);

        void SetCurrentNavigationViewItem(NavigationViewItem item);

        void SetCurrentPage(Type type);
        void SetCurrentNavigationViewItemForMenuWithArgument(object arg);
    }
}
