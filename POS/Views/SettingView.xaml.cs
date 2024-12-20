using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using POS.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using POS.Models;
using POS.Services.DAO;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace POS.Views
{
    public sealed partial class SettingView : Page
    {
        public SettingView()
        {
            this.InitializeComponent();
        }

        private static readonly HttpClient client = new HttpClient();
        private static String getSignature(String text, String key)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        private async void Momo_Click(object sender, RoutedEventArgs e)
        {
            // Generate UUID for requestId and orderId
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();

            string accessKey = "F8BBA842ECF85"; // change your business access key here
            string secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz"; // change your business secret key here

            QuickPayResquest request = new QuickPayResquest();
            request.orderInfo = "xxx";
            request.partnerCode = "MOMO";
            request.redirectUrl = "";
            request.ipnUrl = "https://webhook.site/4cb43743-df24-494e-839d-c6cc184d872c";
            request.amount = 50000;
            request.orderId = myuuidAsString;
            request.requestId = myuuidAsString;
            request.extraData = "";
            request.partnerName = "MOMO";
            request.storeId = "POS HCMUS";
            request.orderGroupId = "";
            request.autoCapture = true;
            request.lang = "vi"; // en or vi
            request.requestType = "captureWallet"; // captureWallet or captureMoMo

            var rawSignature = "accessKey=" + accessKey +
                               "&amount=" + request.amount +
                               "&extraData=" + request.extraData +
                               "&ipnUrl=" + request.ipnUrl +
                               "&orderId=" + request.orderId +
                               "&orderInfo=" + request.orderInfo +
                               "&partnerCode=" + request.partnerCode +
                               "&redirectUrl=" + request.redirectUrl +
                               "&requestId=" + request.requestId +
                               "&requestType=" + request.requestType;
            request.signature = getSignature(rawSignature, secretKey);

            System.Diagnostics.Debug.WriteLine("Raw Signature: " + rawSignature);
            System.Diagnostics.Debug.WriteLine("Generated Signature: " + request.signature);


            // Call MoMo API
            StringContent httpContent = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
            var quickPayResponse = await client.PostAsync("https://test-payment.momo.vn/v2/gateway/api/create", httpContent);
            System.Diagnostics.Debug.WriteLine("Response: " + quickPayResponse);
            // Read response
            var contents = await quickPayResponse.Content.ReadAsStringAsync();
            JObject jMessage = JObject.Parse(contents);
            System.Diagnostics.Debug.WriteLine("Response: " + jMessage);
            //System.Diagnostics.Debug.WriteLine("payUrl: " + jMessage["payUrl"].ToString());
            // Return the payUrl
        }
    }
}
