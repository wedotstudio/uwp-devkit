using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using WeCode_Next.Core;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace WeCode_Next.Pages
{
    public sealed partial class Home : Page
    {
        public Home()
        {
            this.InitializeComponent();

            UpdateData();
        }

        private void UpdateData()
        {
            switch (DeviceInfoHelper.GetDeviceFormFactorType())
            {
                case DeviceFormFactorType.Desktop:
                    Di.Text = "";
                    break;
                case DeviceFormFactorType.Phone:
                    Di.Text = "";
                    break;
                case DeviceFormFactorType.Tablet:
                    Di.Text = "";
                    break;
                case DeviceFormFactorType.SurfaceHub:
                    Di.Text = "";
                    break;
                case DeviceFormFactorType.IoT:
                    Di.Text = "";
                    break;
                default:
                    Di.Text = "";
                    break;
            }
            Dmo.Text = DeviceInfoHelper.GetDeviceModel();
            Dma.Text = DeviceInfoHelper.GetDeviceManufacturer();
            b.Text = DeviceInfoHelper.GetBuild();

            ApplicationDataContainer _appSettings = ApplicationData.Current.LocalSettings;
            if (_appSettings.Values.ContainsKey("OfflineMode"))
            {
                if (Convert.ToBoolean(_appSettings.Values["OfflineMode"]))
                {
                    in_t.Visibility = in_pd.Visibility = in_l.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    in_NC.Visibility = Windows.UI.Xaml.Visibility.Visible;

                    dn_t.Visibility = dn_pd.Visibility = dn_l.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    dn_NC.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    in_NC.Text = dn_NC.Text = "Currently in Offline Mode. ";
                }
                else
                {
                    if (NetworkInterface.GetIsNetworkAvailable())
                    {
                        in_t.Visibility = in_pd.Visibility = in_l.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        in_NC.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        dn_t.Visibility = dn_pd.Visibility = dn_l.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        dn_NC.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        NewsLoad(true);
                        NewsLoad(false);
                    }
                    else
                    {
                        in_t.Visibility = in_pd.Visibility = in_l.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        in_NC.Visibility = Windows.UI.Xaml.Visibility.Visible;

                        dn_t.Visibility = dn_pd.Visibility = dn_l.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        dn_NC.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    }
                }
            }
            else
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    in_t.Visibility = in_pd.Visibility = in_l.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    in_NC.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    dn_t.Visibility = dn_pd.Visibility = dn_l.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    dn_NC.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    NewsLoad(true);
                    NewsLoad(false);
                }
                else
                {
                    in_t.Visibility = in_pd.Visibility = in_l.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    in_NC.Visibility = Windows.UI.Xaml.Visibility.Visible;

                    dn_t.Visibility = dn_pd.Visibility = dn_l.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    dn_NC.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }
        }

        private async void NewsLoad(bool type)
        {
            try
            {
                string uri = (type) ? "https://blogs.windows.com/buildingapps/feed/" : "https://blogs.windows.com/windowsexperience/tag/windows-insider-program/feed/";
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri(uri));
                XDocument news = XDocument.Load(await response.Content.ReadAsStreamAsync());
                foreach (var item in news.Descendants("item"))
                {
                    if (type)
                    {
                        dn_t.Text = (string)item.Element("title");
                        dn_pd.Text = (string)item.Element("pubDate");
                        dn_l.NavigateUri = new Uri((string)item.Element("link"));
                    }
                    else
                    {
                        in_t.Text = (string)item.Element("title");
                        in_pd.Text = (string)item.Element("pubDate");
                        in_l.NavigateUri = new Uri((string)item.Element("link"));
                    }
                    break;
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
