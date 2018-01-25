using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using WeCode_Next.DataModel;
using WeCode_Next.Pages;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Data.Xml.Dom;
using System.Threading.Tasks;

namespace WeCode_Next
{
    public sealed partial class MainPage : Page
    {
        public String verNaStr="";
        public MainPage()
        {
            this.InitializeComponent();

            InitializeUI();
            InitializeList();
            CheckUpdateAsync();

            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            view.SelectedIndex = 0;
            frame.Navigate(typeof(Home));
        }

        private void InitializeList()
        {
            List<Nav> NavList = new List<Nav>
            {
                new Nav { Icon = "", Name = "Home", PageType = typeof(Home) },
                new Nav { Icon = "", Name = "Dev Shortcuts", PageType = typeof(Tools) },
                new Nav { Icon = "", Name = "Windows Blog", PageType = typeof(WindowsBlog) },
                new Nav { Icon = "", Name = "BuildFeed", PageType = typeof(BuildFeed) },
                new Nav { Icon = "", Name = "Fonticon Browser", PageType = typeof(IconBrowser) },
                new Nav { Icon = "", Name = "URI Tester", PageType = typeof(URILauncher) },
                new Nav { Icon = "", Name = "GUID Generator", PageType = typeof(GUIDGen) },
                new Nav { Icon = "", Name = "Assets Generator", PageType = typeof(AssetsGen) },
                new Nav { Icon = "", Name = "Regular Expression", PageType = typeof(RegularExpression) }
                //new Nav { Icon = "", Name = "Json2C#", PageType = typeof(Json2Csharp) },
                //new Nav { Icon = "", Name = "Color Palette", PageType = typeof(ColorPalette) }
            };
            view.ItemsSource = NavList;

            List<Nav> BottomNavList = new List<Nav>
            {
                new Nav { Icon = "", Name = "Settings", PageType = typeof(Settings) },
                new Nav { Icon = "", Name = "About", PageType = typeof(About) }
            };
           bottom_view.ItemsSource = BottomNavList;
        }
        private void InitializeUI()
        {
            var isDark = Application.Current.RequestedTheme == ApplicationTheme.Dark;
            var applicationView = ApplicationView.GetForCurrentView();
            var titleBar = applicationView.TitleBar;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveForegroundColor = (isDark) ? Colors.White : Colors.Black;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonForegroundColor = (isDark) ? Colors.White : Colors.Black;
            Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            BitmapImage de_icon = new BitmapImage(new Uri("ms-appx:///Assets/app.png"));
            BitmapImage bk_icon = new BitmapImage(new Uri("ms-appx:///Assets/app-dark.png"));
            panel_icon.Source = isDark? de_icon:bk_icon;
            //InitializeFrostedGlass(bgGrid);
        }
        private void InitializeFrostedGlass(UIElement glassHost)
        {
            Visual hostVisual = ElementCompositionPreview.GetElementVisual(glassHost);
            Compositor compositor = hostVisual.Compositor;
            var backdropBrush = compositor.CreateHostBackdropBrush();
            var glassVisual = compositor.CreateSpriteVisual();
            glassVisual.Brush = backdropBrush;
            ElementCompositionPreview.SetElementChildVisual(glassHost, glassVisual);
            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);
            glassVisual.StartAnimation("Size", bindSizeAnimation);
        }
        private async Task CheckUpdateAsync()
        {
            try
            {
                string url = "https://garage.patrickwu.cf/sources/logfile/uwp-devkit.log";
#if DEBUG
                url = "https://garage.patrickwu.cf/sources/logfile/uwp-devkit-test.log";
#endif
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri(url));
                String verString = await response.Content.ReadAsStringAsync();
                int verInt = Convert.ToInt32(verString);
                if (verInt > Convert.ToInt32(Core.Base.VERSION))
                {
                    int verInt_main = Convert.ToInt32(verString.Substring(0, 2));
                    int verInt_sub = Convert.ToInt32(verString.Substring(2, 2));
                    verNaStr = verInt_main.ToString() + "." + verInt_sub.ToString();
                    vtext.Text = verNaStr;
                    InAppPopupNotification.Show();
                }
            } catch (Exception e)
            {

            }
        }
        private void ItemClick(object sender, ItemClickEventArgs e)
        {
            if (bottom_view.SelectedIndex >= 0) bottom_view.SelectedIndex = -1;
            if (view.SelectedIndex >= 0) view.SelectedIndex = -1;
            var items = (Nav)e.ClickedItem;
            frame.Navigate(items.PageType);
        }

        private async void changelog_Click(object sender, RoutedEventArgs e)
        {
            if (verNaStr != "")
            {
                string url = "https://github.com/WEdotStudio/UWP-DevKit/releases/tag/v"+verNaStr;
#if DEBUG
                url = "https://github.com/WEdotStudio/UWP-DevKit/wiki/Changelog-Before-5.0";
#endif
                await Windows.System.Launcher.LaunchUriAsync(new Uri(url));
            }
        }

        private async void update_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?ProductId=9NBLGGH5P90F"));
        }

        private void dismiss_Click(object sender, RoutedEventArgs e)
        {
            InAppPopupNotification.Dismiss();
        }
    }
}
