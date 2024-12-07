using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using POS.ViewModels;
using System.Collections.Generic;
using CommunityToolkit.WinUI.UI.Controls;
using System;
using POS.Models;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;


namespace POS.Views
{
    public sealed partial class WarehouseView : Page
    {
        public WarehouseViewModel ViewModel { get; set; }
        public WarehouseView()
        {
            this.InitializeComponent();
            ViewModel = new WarehouseViewModel();
            this.DataContext = ViewModel;
            UpdatePagingInfo_bootstrap();
        }

        //==========================================================
        // search feature
        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ViewModel.Keyword = SearchBox.Text;
            ViewModel.LoadWarehouses(1);
            UpdatePagingInfo_bootstrap();
        }

        //================================================================

        private void AccountItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Lấy NavigationViewItem từ sender
            var menuItem = sender as NavigationViewItem;

            // Kiểm tra xem menuItem có hợp lệ không
            if (menuItem != null)
            {
                var accountWindow = new ShellWindow();
                accountWindow.Activate();
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagesComboBox.SelectedIndex < ViewModel.TotalPages - 1)
            {
                pagesComboBox.SelectedIndex++;
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentPage > 1)
            {
                pagesComboBox.SelectedIndex--;
            }
        }

        void UpdatePagingInfo_bootstrap()
        {
            var infoList = new List<object>();
            for (int i = 1; i <= ViewModel.TotalPages; i++)
            {
                infoList.Add(new
                {
                    Page = i,
                    Total = ViewModel.TotalPages
                });
            };

            pagesComboBox.ItemsSource = infoList;
            pagesComboBox.SelectedIndex = 0;
        }

        private void pagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic item = pagesComboBox.SelectedItem;
            if (item != null)
            {
                ViewModel.LoadWarehouses(item.Page);
            }
        }

        //================================================================
        // Add feature
        private void AddWarehouse_Click(object sender, RoutedEventArgs e)
        {
            ShowAddWarehouseDialog();
        }

        private void ShowAddWarehouseDialog()
        {
            // Reset dialog fields
            IngredientNameTextBox.Text = "";
            StockQuantityTextBox.Text = "";
            UnitTextBox.Text = "";
            EntryDatePicker.SelectedDate = null;
            ExpirationDatePicker.SelectedDate = null;

            _ = AddWarehouseDialog.ShowAsync(); 
        }

        private void ShowAddSuccessTeachingTip()
        {
            AddSuccessTeachingTip.IsOpen = true;

            // Auto close after 3s
            _ = Task.Delay(3000).ContinueWith(_ =>
            {
                DispatcherQueue.TryEnqueue(() => AddSuccessTeachingTip.IsOpen = false);
            });
        }

        private async void OnSaveWarehouse(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string ingredientName = IngredientNameTextBox.Text;
            int stockQuantity = int.Parse(StockQuantityTextBox.Text);
            string unit = UnitTextBox.Text;
            DateTime entryDate = EntryDatePicker.Date.DateTime;
            DateTime expirationDate = ExpirationDatePicker.Date.DateTime;

            var newWarehouse = new Warehouse
            {
                IngredientName = ingredientName,
                StockQuantity = stockQuantity,
                Unit = unit,
                EntryDate = entryDate,
                ExpirationDate = expirationDate
            };

            bool result = ViewModel.AddWarehouse(newWarehouse);
            if (result)
            {
                ShowAddSuccessTeachingTip();
                AddWarehouseDialog.Hide();
            }
            else
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = "Không thể lưu nguyên liệu. Vui lòng thử lại.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close
                };

                await errorDialog.ShowAsync();
                args.Cancel = true;
            }
        }

        private void OnCancelAddWarehouse(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AddWarehouseDialog.Hide();
        }

        //================================================================
        // Delete feature
        private async void DeleteWarehouse_Click(object sender, RoutedEventArgs e)
        {
            await DeleteWarehouseAsync();
        }

        private async Task DeleteWarehouseAsync()
        {
            if (ViewModel.SelectedWarehouse == null)
            {
                // Show an error dialog if no item is selected
                var errorDialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = "Vui lòng chọn nguyên liệu muốn xóa.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot // Bind to the current XamlRoot
                };

                await errorDialog.ShowAsync();
                return;
            }

            // Show confirmation dialog
            var confirmDialog = new ContentDialog
            {
                Title = "Xác nhận xóa",
                Content = $"Bạn có chắc muốn xóa nguyên liệu '{ViewModel.SelectedWarehouse.IngredientName}'?",
                PrimaryButtonText = "Hủy",
                SecondaryButtonText = "Xóa",
                DefaultButton = ContentDialogButton.Secondary,
                XamlRoot = this.XamlRoot
            };

            var result = await confirmDialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                // Remove the selected warehouse
                bool deleteResult = ViewModel.DeleteWarehouse(ViewModel.SelectedWarehouse);
                if (deleteResult)
                {
                    ShowDeleteSuccessTeachingTip();
                }
                else
                {
                    var errorDialog = new ContentDialog
                    {
                        Title = "Lỗi",
                        Content = "Không thể xóa nguyên liệu. Vui lòng thử lại.",
                        CloseButtonText = "OK",
                        DefaultButton = ContentDialogButton.Close,
                        XamlRoot = this.XamlRoot
                    };

                    await errorDialog.ShowAsync();
                }
            }
        }

        private async void DeleteWarehouse()
        {
            if (ViewModel.SelectedWarehouse == null)
            {
                // Show an error dialog if no item is selected
                var errorDialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = "Vui lòng chọn một dòng trước khi xóa.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot // Bind to the current XamlRoot
                };

                await errorDialog.ShowAsync();
                return;
            }

            // Show confirmation dialog
            var confirmDialog = new ContentDialog
            {
                Title = "Xác nhận xóa",
                Content = $"Bạn có chắc muốn xóa nguyên liệu '{ViewModel.SelectedWarehouse.IngredientName}'?",
                PrimaryButtonText = "Xóa",
                CloseButtonText = "Hủy",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot
            };

            var result = await confirmDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                // Remove the selected warehouse
                bool deleteResult = ViewModel.DeleteWarehouse(ViewModel.SelectedWarehouse);
                if (deleteResult)
                {
                    ShowDeleteSuccessTeachingTip();
                }
                else
                {
                    var errorDialog = new ContentDialog
                    {
                        Title = "Lỗi",
                        Content = "Không thể xóa nguyên liệu. Vui lòng thử lại.",
                        CloseButtonText = "OK",
                        DefaultButton = ContentDialogButton.Close,
                        XamlRoot = this.XamlRoot
                    };

                    await errorDialog.ShowAsync();
                }
            }
        }

        private void ShowDeleteSuccessTeachingTip()
        {
            DeleteSuccessTeachingTip.IsOpen = true;

            // Auto close after 3s
            _ = Task.Delay(3000).ContinueWith(_ =>
            {
                DispatcherQueue.TryEnqueue(() => DeleteSuccessTeachingTip.IsOpen = false);
            });
        }

        // Sort feature
        private void OnDataGridSorting(object sender, DataGridColumnEventArgs e)
        {
            // Only apply for the columns that need to be sorted
            if (e.Column.Header?.ToString() == "Ngày nhập" || e.Column.Header?.ToString() == "Ngày hết hạn")
            {
                var column = e.Column as DataGridTextColumn;
                if (column != null)
                {
                    var binding = column.Binding as Binding;
                    if (binding != null)
                    {
                        var propertyName = binding.Path.Path; // Get the property name to sort
                        ViewModel.SortWarehouses(propertyName);
                    }
                }
            }
            else
            {
                // Do nothing when click on other columns
            }
        }
    }
}
