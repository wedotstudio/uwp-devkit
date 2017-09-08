using Microsoft.Azure.Mobile;
using System;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WeCode_Next.Pages
{
    public sealed partial class About : Page
    {
        public About()
        {
            this.InitializeComponent();

            InitAbout();
        }

        private async void InitAbout()
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Data/about.md"));
            MarkdownText.Text = await FileIO.ReadTextAsync(file);

            Package package = Package.Current;
            PackageId packageId = package.Id;
            var packageid = packageId.ProductId;
            var packagereid = packageId.ResourceId;

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9nblggh5p90f"));
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/patrick330602/UWP-DevKit"));
        }

        private async void MarkdownText_LinkClicked(object sender, Microsoft.Toolkit.Uwp.UI.Controls.LinkClickedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(e.Link));
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("mailto:patrick.we.studio@outlook.com"));
        }
    }
}
