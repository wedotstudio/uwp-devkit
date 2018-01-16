using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml;
using WeCode_Next.DataModel;
using WeCode_Next.Pages;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Imaging;

namespace WeCode_Next
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            InitializeUI();
            InitializeList();

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
                new Nav { Icon = "", Name = "Windows Blog", PageType = typeof(WindowsBlog) },
                new Nav { Icon = "", Name = "BuildFeed", PageType = typeof(BuildFeed) },
                new Nav { Icon = "", Name = "Icon Browser", PageType = typeof(IconBrowser) },
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
        private async void CheckUpdate()
        {
            string url = "http://ap.westudio.ml/sources/json/wecode-update.json";
#if DEBUG
            url = "http://ap.westudio.ml/sources/json/wecode-update-test.json";
#endif
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri(url));

            var jsonString = await response.Content.ReadAsStringAsync();
            Update.RootObject data = JsonConvert.DeserializeObject<Update.RootObject>(jsonString);
            string version = data.version;
            string[] versionnum = version.Split('.');
            int versioncount = Convert.ToInt32(versionnum[0]) * 10000 + Convert.ToInt32(versionnum[1]) * 100 + Convert.ToInt32(versionnum[2]);
            if (versioncount > 20200)
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                HttpResponseMessage detailstring;
                switch (loader.GetString("nr_lan"))
                {
                    case "en":
                        detailstring = await client.GetAsync(new Uri(data.detail.en));
                        break;
                    case "zh-hans":
                        detailstring = await client.GetAsync(new Uri(data.detail.zh_hans));
                        break;
                    case "zh-hant":
                        detailstring = await client.GetAsync(new Uri(data.detail.zh_hant));
                        break;
                    default:
                        detailstring = await client.GetAsync(new Uri(data.detail.en));
                        break;
                }

                string detailstringin = await detailstring.Content.ReadAsStringAsync();
                string xmlContent = string.Empty;
                XmlDocument xdoc = new XmlDocument();
                xmlContent = string.Format(
                    "<toast>" +
                        "<visual>" +
                            "<binding template = 'ToastGeneric'>" +
                                   "<image placement = 'appLogoOverride' src = '' />" +
                                   "<text> {0} {1} {2}</text>" +
                                    "<text>{3}</text>" +
                                    "<image  placement = 'hero' src = 'Assets/new-ver.png' />" +
                            "</binding>" +
                        "</visual>" +
                        "<actions>" +
                            "<action" +
                             " content = '{4}'" +
                             " activationType='protocol'" +
                             " arguments = 'ms-windows-store://pdp/?ProductId=9nblggh5p90f' />" +
                             "<action" +
                             " content = '{5}'" +
                             " arguments = 'action=disableNoti' />" +
                             "<action" +
                             " content = '{6}'" +
                             " activationType='system'" +
                             " arguments = 'dismiss' />" +
                         "</actions>" +
                    "</toast>",
                     loader.GetString("nr_1"), version, loader.GetString("nr_2"), detailstringin, loader.GetString("nr_3"), loader.GetString("nr_5"), loader.GetString("nr_4")
                );
                xdoc.LoadXml(xmlContent);
                ToastNotification toast1 = new ToastNotification(xdoc);
                ToastNotificationManager.CreateToastNotifier().Show(toast1);
            }
        }
        private void ItemClick(object sender, ItemClickEventArgs e)
        {
            if (bottom_view.SelectedIndex >= 0) bottom_view.SelectedIndex = -1;
            if (view.SelectedIndex >= 0) view.SelectedIndex = -1;
            var items = (Nav)e.ClickedItem;
            frame.Navigate(items.PageType);
        }
    }
}
