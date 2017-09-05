using System.Collections.Generic;
using WeCode_Next.DataModel;
using WeCode_Next.Pages;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

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

            InitializeFrostedGlass(bgGrid);
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

        private void ItemClick(object sender, ItemClickEventArgs e)
        {
            if (bottom_view.SelectedIndex >= 0) bottom_view.SelectedIndex = -1;
            if (view.SelectedIndex >= 0) view.SelectedIndex = -1;
            var items = (Nav)e.ClickedItem;
            frame.Navigate(items.PageType);
        }
    }
}
