using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using WeCode_Next.DataModel;
using Windows.UI.Xaml.Controls;


namespace WeCode_Next.Pages
{
    public sealed partial class BuildFeed : Page
    {
        public BuildFeed()
        {
            this.InitializeComponent();
            NewsLoad();
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
