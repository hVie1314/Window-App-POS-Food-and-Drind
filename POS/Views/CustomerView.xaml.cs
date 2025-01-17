using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using POS.ViewModels;
using System.Collections.Generic;
using CommunityToolkit.WinUI.UI.Controls;
using System;
using POS.Models;
using System.Threading.Tasks;
using POS.Shells;
using Microsoft.UI.Xaml.Data;


namespace POS.Views
{
    /// <summary>
    /// Giao diện quản lý khách hàng
    /// </summary>
    public sealed partial class CustomerView : Page
    {
        /// <summary>
        /// ViewModel quản lý logic và dữ liệu của giao diện quản lý khách hàng.
        /// </summary>
        public CustomerViewModel ViewModel { get; set; }

        /// <summary>
        /// Khởi tạo giao diện quản lý khách hàng.
        /// </summary>
        public CustomerView()
        {
            this.InitializeComponent();
            ViewModel = new CustomerViewModel();
            this.DataContext = ViewModel;
            UpdatePagingInfo_bootstrap();
        }

        //==========================================================

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhập từ khóa tìm kiếm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ViewModel.Keyword = SearchBox.Text;
            ViewModel.LoadCustomers(1);
            UpdatePagingInfo_bootstrap();
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

        /// <summary>
        /// Xử lý sự kiện khi người dùng chuyển trang trong phân trang.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagesComboBox.SelectedIndex < ViewModel.TotalPages - 1)
            {
                pagesComboBox.SelectedIndex++;
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng chuyển trang trong phân trang.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentPage > 1)
            {
                pagesComboBox.SelectedIndex--;
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

        /// <summary>
        /// Xử lý sự kien khi người dùng chọn trang trong phân trang.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic item = pagesComboBox.SelectedItem;
            if (item != null)
            {
                ViewModel.LoadCustomers(item.Page);
            }
        }

        //================================================================

        /// <summary>
        /// Xử lý sự kiện khi người dùng chọn nút thêm khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            ShowAddCustomerDialog();
        }

        /// <summary>
        /// Hiển thị dialog thêm khách hàng.
        /// </summary>
        private void ShowAddCustomerDialog()
        {
            // Reset dialog fields
            NameTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
            EmailTextBox.Text = "";
            AddressTextBox.Text = "";
            CustomerTypeTextBox.Text="";

            _ = AddCustomerDialog.ShowAsync(); 
        }

        /// <summary>
        /// Hiển thị thông báo thêm khách hàng thành công.
        /// </summary>
        private void ShowAddSuccessTeachingTip()
        {
            AddSuccessTeachingTip.IsOpen = true;

            // Auto close after 3s
            _ = Task.Delay(3000).ContinueWith(_ =>
            {
                DispatcherQueue.TryEnqueue(() => AddSuccessTeachingTip.IsOpen = false);
            });
        }

        /// <summary>
        /// Xử lý sự kien khi người dùng chọn nút lưu khách hàng.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void OnSaveCustomer(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Kiểm tra nếu người dùng chưa nhập tên khách hàng
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = "Tên khách hàng không được để trống.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = this.XamlRoot
                };
                // Close CreateDiscountDialog and show error dialog
                AddCustomerDialog.Hide();
                await errorDialog.ShowAsync();
                args.Cancel = true;
                return;
            }


            string name = NameTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            string email = EmailTextBox.Text;
            string address = AddressTextBox.Text;
            string customerType = CustomerTypeTextBox.Text;

            var newCustomer = new Customer
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Email = email,
                Address = address,
                CustomerType = customerType
            };

    bool result = ViewModel.AddCustomer(newCustomer);
            if (result)
            {
                ShowAddSuccessTeachingTip();
                AddCustomerDialog.Hide();
            }
            else
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = "Không thể lưu khách hàng. Vui lòng thử lại.",
                    CloseButtonText = "OK",
                    DefaultButton = ContentDialogButton.Close
                };

                await errorDialog.ShowAsync();
                args.Cancel = true;
            }
        }

        /// <summary>
        /// Xử lý sự kien khi người dùng chọn nút hủy thêm khách hàng.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnCancelAddCustomer(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AddCustomerDialog.Hide();
        }

        //================================================================
        /// <summary>
        /// Xử lý sự kien khi người dùng chọn nút xóa khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            await DeleteCustomerAsync();
        }

        /// <summary>
        /// Xử lý sự kiện xóa khách hàng.
        /// </summary>
        /// <returns></returns>
        private async Task DeleteCustomerAsync()
        {
            if (ViewModel.SelectedCustomer == null)
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
                Content = $"Bạn có chắc muốn xóa nguyên liệu '{ViewModel.SelectedCustomer.Name}'?",
                PrimaryButtonText = "Hủy",
                SecondaryButtonText = "Xóa",
                DefaultButton = ContentDialogButton.Secondary,
                XamlRoot = this.XamlRoot
            };

            var result = await confirmDialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                // Remove the selected Customer
                bool deleteResult = ViewModel.DeleteCustomer(ViewModel.SelectedCustomer);
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

        /// <summary>
        /// Hiển thị thông báo xóa khách hàng thành công.
        /// </summary>
        private async void DeleteCustomer()
        {
            if (ViewModel.SelectedCustomer == null)
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
                Content = $"Bạn có chắc muốn xóa khách hàng '{ViewModel.SelectedCustomer.Name}'?",
                PrimaryButtonText = "Xóa",
                CloseButtonText = "Hủy",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot
            };

            var result = await confirmDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                // Remove the selected Customer
                bool deleteResult = ViewModel.DeleteCustomer(ViewModel.SelectedCustomer);
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

        /// <summary>
        /// Hiển thị thông báo xóa khách hàng thành công.
        /// </summary>
        private void ShowDeleteSuccessTeachingTip()
        {
            DeleteSuccessTeachingTip.IsOpen = true;

            // Auto close after 3s
            _ = Task.Delay(3000).ContinueWith(_ =>
            {
                DispatcherQueue.TryEnqueue(() => DeleteSuccessTeachingTip.IsOpen = false);
            });
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng chọn sắp xếp cột trong bảng khách hàng.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataGridSorting(object sender, DataGridColumnEventArgs e)
        {
            if (e.Column != null)
            {
                var column = e.Column as DataGridTextColumn;
                if (column != null)
                {
                    var binding = column.Binding as Binding;
                    if (binding != null)
                    {
                        var propertyName = binding.Path.Path; // Get the property name to sort
                        ViewModel.SortCustomers(propertyName);
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
