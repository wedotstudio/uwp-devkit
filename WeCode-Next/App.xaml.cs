using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.StartScreen;
using WeCode_Next.Core;

namespace WeCode_Next
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            ApplicationDataContainer _appSettings = ApplicationData.Current.LocalSettings;
            if (_appSettings.Values.ContainsKey("Themes"))
            {
                if (Convert.ToInt32(_appSettings.Values["Themes"]) == 1)
                {
                    RequestedTheme = ApplicationTheme.Light;
                }
                if (Convert.ToInt32(_appSettings.Values["Themes"]) == 2)
                {
                    RequestedTheme = ApplicationTheme.Dark;
                }
            }
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            await ConfigureJumpList();

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        private async Task ConfigureJumpList()
        {
            JumpList jumpList = await JumpList.LoadCurrentAsync();

            jumpList.Items.Clear();
            Base.JumpListBuilder(ref jumpList, "DevSh", "Dev Shortcuts", "ms-appx:///Assets/app.png");
            Base.JumpListBuilder(ref jumpList, "WinBlog", "Windows Blog", "ms-appx:///Assets/app.png");
            Base.JumpListBuilder(ref jumpList, "BFeed", "BuildFeed", "ms-appx:///Assets/app.png");
            Base.JumpListBuilder(ref jumpList, "IBrowser", "Fonticon Browser", "ms-appx:///Assets/app.png");
            Base.JumpListBuilder(ref jumpList, "UriT", "URI Tester", "ms-appx:///Assets/app.png");
            Base.JumpListBuilder(ref jumpList, "GuidG", "GUID Generator", "ms-appx:///Assets/app.png");
            Base.JumpListBuilder(ref jumpList, "AssG", "Assets Generator", "ms-appx:///Assets/app.png");
            Base.JumpListBuilder(ref jumpList, "ReEx", "Regular Expression", "ms-appx:///Assets/app.png");

            await jumpList.SaveAsync();
        }

        

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
        }
        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
