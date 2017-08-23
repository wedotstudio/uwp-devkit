using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Xml;
using System.Xml.Linq;
using WeCode_Next.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace WeCode_Next.Pages
{
    public sealed partial class Home : Page
    {
        public Home()
        {
            this.InitializeComponent();
            switch (DeviceInfoHelper.GetDeviceFormFactorType())
            {
                case DeviceFormFactorType.Desktop:
                    Di.Text = "";
                    break;
                case DeviceFormFactorType.Phone:
                    Di.Text = "";
                    break;
                case DeviceFormFactorType.Tablet:
                    Di.Text = "";
                    break;
                default:
                    Di.Text = "";
                    break;
            }
            Dmo.Text = DeviceInfoHelper.GetDeviceModel();
            Dma.Text = DeviceInfoHelper.GetDeviceManufacturer();
            b.Text = DeviceInfoHelper.GetBuild();

            UpdateData();
        }

        private async void UpdateData()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                in_t.Visibility = Windows.UI.Xaml.Visibility.Visible;
                in_pd.Visibility = Windows.UI.Xaml.Visibility.Visible;
                in_l.Visibility = Windows.UI.Xaml.Visibility.Visible;
                in_NC.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                dn_t.Visibility = Windows.UI.Xaml.Visibility.Visible;
                dn_pd.Visibility = Windows.UI.Xaml.Visibility.Visible;
                dn_l.Visibility = Windows.UI.Xaml.Visibility.Visible;
                dn_NC.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(new Uri("https://blogs.windows.com/windowsexperience/tag/windows-insider-program/feed/"));
            XDocument insidernews = XDocument.Load(await response.Content.ReadAsStreamAsync());
            foreach (var item in insidernews.Descendants("item"))
            {
                in_t.Text = (string)item.Element("title");
                in_pd.Text = (string)item.Element("pubDate");
                in_l.NavigateUri = new Uri((string)item.Element("link"));
                break;
            }

                HttpResponseMessage response2 = await client.GetAsync(new Uri("https://blogs.windows.com/buildingapps/feed/"));
                XDocument devnews = XDocument.Load(await response2.Content.ReadAsStreamAsync());
                foreach (var item in devnews.Descendants("item"))
                {
                    dn_t.Text = (string)item.Element("title");
                    dn_pd.Text = (string)item.Element("pubDate");
                    dn_l.NavigateUri = new Uri((string)item.Element("link"));
                    break;
                }
            }
            else {
                in_t.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                in_pd.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                in_l.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                in_NC.Visibility = Windows.UI.Xaml.Visibility.Visible;

                dn_t.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                dn_pd.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                dn_l.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                dn_NC.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }
    }
}
