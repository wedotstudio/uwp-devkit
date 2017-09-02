using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.NetworkInformation;
using WeCode_Next.DataModel;
using Windows.Storage;
using Windows.UI.Xaml.Controls;


namespace WeCode_Next.Pages
{
    public sealed partial class BuildFeed : Page
    {
        public BuildFeed()
        {
            this.InitializeComponent();
            ApplicationDataContainer _appSettings = ApplicationData.Current.LocalSettings;
            if (_appSettings.Values.ContainsKey("OfflineMode"))
            {
                if (Convert.ToBoolean(_appSettings.Values["OfflineMode"]))
                {
                    NC.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    bf.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    NC.Text = "Currently in Offline Mode. ";
                }
                else
                {
                    if (NetworkInterface.GetIsNetworkAvailable())
                    {
                        NC.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        bf.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        NewsLoad();
                    }
                    else
                    {
                        NC.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        bf.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                }
            }
            else
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    NC.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    bf.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    NewsLoad();
                }
                else
                {
                    NC.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    bf.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        private async void NewsLoad()
        {
            //string uri = "https://buildfeed.net/api/GetBuildsByLab?lab=rs_prerelease";
            string uri = "https://buildfeed.net/api/";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri(uri));
            ObservableCollection<BuildFeedItem> Data = JsonConvert.DeserializeObject<ObservableCollection<BuildFeedItem>>(await response.Content.ReadAsStringAsync());
            bf.ItemsSource = Data;
            
        }
    }
}
