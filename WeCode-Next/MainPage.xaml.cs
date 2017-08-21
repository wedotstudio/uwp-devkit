using System;
using System.Collections.Generic;
using WeCode_Next.DataModel;
using WeCode_Next.Pages;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Navigation;

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
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            if (e.Size.Width > 800)
            {
                GeneralNav.Visibility = Visibility.Visible;
                MobNav.Visibility = Visibility.Collapsed;
            }
            else {
                GeneralNav.Visibility = Visibility.Collapsed;
                MobNav.Visibility = Visibility.Visible;
            }
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
                new Nav { Icon = "", Name = "System Information", PageType = typeof(SystemInfo) },
                new Nav { Icon = "", Name = "Icon Browser", PageType = typeof(IconBrowser) },
                new Nav { Icon = "", Name = "URI Tester", PageType = typeof(URILauncher) },
                new Nav { Icon = "", Name = "Assets Generator", PageType = typeof(AssetsGen) }
            };
            view.ItemsSource = NavList;
            Mobview.ItemsSource = NavList;
        }

        private void InitializeUI()
        {
            var isDark = Application.Current.RequestedTheme == ApplicationTheme.Dark;
            var applicationView = ApplicationView.GetForCurrentView();
            var titleBar = applicationView.TitleBar;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveForegroundColor = (isDark)?Colors.White:Colors.Black;
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

        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {

        }

        private void ItemClick(object sender, ItemClickEventArgs e)
        {
            var items = (Nav)e.ClickedItem;
            frame.Navigate(items.PageType);
        }
    }
}
