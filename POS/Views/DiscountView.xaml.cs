using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using POS.ViewModels;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace POS.Views
{
    public sealed partial class DiscountView : Page
    {
        public DiscountViewModel ViewModel { get; set; }

        public DiscountView()
        {
            this.InitializeComponent();
            ViewModel = new DiscountViewModel();
            this.DataContext = ViewModel;
            UpdatePagingInfo_bootstrap();
        }

        private async void CreateDiscount_Click(object sender, RoutedEventArgs e)
        {
            // Reset các TextBox trước khi mở ContentDialog
            QuantityTextBox.Text = string.Empty;
            await CreateDiscountDialog.ShowAsync();
        }

        private async void OnCreateDiscount_Click(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Kiểm tra nếu người dùng chưa nhập quantity hoặc value
            if (string.IsNullOrEmpty(QuantityTextBox.Text) || DiscountValueComboBox.SelectedItem == null)
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = "Vui lòng chọn giá trị và số lượng mã giảm giá muốn tạo.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot
                };
                // Close CreateDiscountDialog and show error dialog
                CreateDiscountDialog.Hide();
                await errorDialog.ShowAsync();
                args.Cancel = true;
                return;
            }

            // Kiểm tra số lượng là một số nguyên và lớn hơn 0
            if (!int.TryParse(QuantityTextBox.Text, out int _quantity) || _quantity <= 0)
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = "Số lượng phải là một số nguyên lớn hơn 0.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot
                };
                // Close CreateDiscountDialog and show error dialog
                CreateDiscountDialog.Hide();
                await errorDialog.ShowAsync();
                args.Cancel = true;
                return;
            }

            // Lấy dữ liệu từ ComboBox và TextBox và gán vào ViewModel
            ViewModel.Value = int.Parse((DiscountValueComboBox.SelectedItem as ComboBoxItem).Content.ToString());
            ViewModel.Quantity = int.Parse(QuantityTextBox.Text);

            // Tạo mã giảm giá
            bool result = ViewModel.CreateDiscounts(ViewModel.Value, ViewModel.Quantity);

            // Hiển thị thông báo tạo mã giảm giá thành công
            if (result)
            {
                ShowAddSuccessTeachingTip();
                // Cập nhật lại thông tin phân trang
                UpdatePagingInfo_bootstrap();
            }
            // Hiển thị thông báo lỗi
            else
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = "Không thể tạo mã giảm giá, vui lòng kiểm tra lại.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close
                };

                await errorDialog.ShowAsync();
                args.Cancel = true;
            }
        }


        private void OnCancel_Click(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Do nothing
        }

        private async void DeleteDiscount_Click(object sender, RoutedEventArgs e)
        {
            var selectedDiscount = ViewModel.SelectedDiscount;

            if (selectedDiscount == null)
            {
                await ShowErrorDialog("Vui lòng chọn một mã giảm giá để xóa.");
                return;
            }

            var confirmDialog = new ContentDialog
            {
                Title = "Xác nhận xóa",
                Content = $"Bạn có chắc chắn muốn xóa mã giảm giá {ViewModel.SelectedDiscount.DiscountCode} không?",
                PrimaryButtonText = "Xóa",
                CloseButtonText = "Hủy",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = this.XamlRoot
            };

            var result = await confirmDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                bool deleteResult = ViewModel.DeleteDiscount(selectedDiscount);

                if (deleteResult)
                {
                    ShowDeleteSuccessTeachingTip();
                    // Cập nhật lại thông tin phân trang
                    UpdatePagingInfo_bootstrap();
                }
                else
                {
                    await ShowErrorDialog("Xóa mã giảm giá thất bại. Vui lòng thử lại.");
                }
            }
        }

        private async Task ShowErrorDialog(string message)
        {
            var errorDialog = new ContentDialog
            {
                Title = "Lỗi",
                Content = message,
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot
            };

            await errorDialog.ShowAsync();
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

        private void ShowDeleteSuccessTeachingTip()
        {
            DeleteSuccessTeachingTip.IsOpen = true;

            // Auto close after 3s
            _ = Task.Delay(3000).ContinueWith(_ =>
            {
                DispatcherQueue.TryEnqueue(() => DeleteSuccessTeachingTip.IsOpen = false);
            });
        }

        //================================================================

        /// <summary>
        /// Xử lý sự kiện khi người dùng chuyển sang trang chức năng tài khoản.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (ViewModel.CurrentPage < ViewModel.TotalPages)
            {
                ViewModel.LoadDiscounts(ViewModel.CurrentPage + 1);
                UpdatePagingButtons();
                pagesComboBox.SelectedIndex = ViewModel.CurrentPage - 1;
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentPage > 1)
            {
                ViewModel.LoadDiscounts(ViewModel.CurrentPage - 1);
                UpdatePagingButtons();
                pagesComboBox.SelectedIndex = ViewModel.CurrentPage - 1;
            }
        }


        /// <summary>
        /// Cập nhật thông tin phân trang.
        /// </summary>
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
            if (pagesComboBox.SelectedItem is not null)
            {
                dynamic selectedPage = pagesComboBox.SelectedItem;
                ViewModel.LoadDiscounts(selectedPage.Page);
                UpdatePagingButtons(); // Cập nhật trạng thái nút
            }
        }

        private void UpdatePagingButtons()
        {
            previousButton.IsEnabled = ViewModel.CurrentPage > 1;
            nextButton.IsEnabled = ViewModel.CurrentPage < ViewModel.TotalPages;
        }
    }
}
