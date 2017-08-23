using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using WeCode_Next.DataModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeCode_Next.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WindowsBlog : Page
    {
        public WindowsBlog()
        {
            this.InitializeComponent();
            NewsLoad(false);
            NewsLoad(true);

        }

        private async void NewsLoad(bool type)
        {
            string uri = (type) ? "https://blogs.windows.com/buildingapps/feed/" : "https://blogs.windows.com/windowsexperience/tag/windows-insider-program/feed/";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri(uri));
            XDocument news = XDocument.Load(await response.Content.ReadAsStreamAsync());
            var data = from item in news.Descendants("item")
                       select new News
                       {
                           Title = (string)item.Element("title"),
                           PubDate = (string)item.Element("pubDate"),
                           Link = (string)item.Element("link")
                       };
            if (type)
            {
                wd.ItemsSource = data;
            }
            else
            {
                wip.ItemsSource = data;
            }
        }
    }
}
