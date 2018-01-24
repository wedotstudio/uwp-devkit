using System;
using System.Diagnostics;
using WeCode_Next.Core;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace WeCode_Next.Pages
{
    public sealed partial class Settings : Page
    {
        private ApplicationDataContainer _appSettings;
        public Settings()
        {
            _appSettings = ApplicationData.Current.LocalSettings;
            this.InitializeComponent();

            Loaded += Settings_Loaded;
        }

        private async void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            // Themes

            if (_appSettings.Values.ContainsKey("Themes"))
            {
                theme_s.SelectedIndex = Convert.ToInt32(_appSettings.Values["Themes"]);
            }
            else
            {
                theme_s.SelectedIndex = 0;
            }

            //Offline Mode
            if (_appSettings.Values.ContainsKey("OfflineMode"))
            {
                offline_m.IsOn = Convert.ToBoolean(_appSettings.Values["OfflineMode"]);
            }
            else
            {
                offline_m.IsOn = false;
                _appSettings.Values["OfflineMode"] = offline_m.IsOn;
            }

            //URI History
            if (await Base.IsFilePresent("history.log"))
            {
                Windows.Storage.FileProperties.BasicProperties basicProperties = await (await ApplicationData.Current.LocalFolder.GetFileAsync("history.log")).GetBasicPropertiesAsync();
                string fileSize = string.Format("{0:##0.###}", basicProperties.Size * 0.001);
                history_content.Text = fileSize + " KB";
            }
            else
            {
                history_content.Text = "0 KB";
            }
        }

        private void theme_s_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ComboBoxItem)theme_s.SelectedItem).Content.ToString())
            {
                case "System":
                    _appSettings.Values["Themes"] = 0;
                    break;
                case "Light":
                    _appSettings.Values["Themes"] = 1;
                    break;
                case "Dark":
                    _appSettings.Values["Themes"] = 2;
                    break;
            }
        }

        private void offline_m_Toggled(object sender, RoutedEventArgs e)
        {
            _appSettings.Values["OfflineMode"] = offline_m.IsOn;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (await Base.IsFilePresent("history.log"))
            {
                await (await ApplicationData.Current.LocalFolder.GetFileAsync("history.log")).DeleteAsync();
            }
            history_content.Text = "0 KB";
        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("You are about to reset the settings. Are you sure you want to proceed?");

            messageDialog.Commands.Add(new UICommand(
                "I am Sure",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand(
                "Cancel",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;

            await messageDialog.ShowAsync();
        }
        private async void CommandInvokedHandler(IUICommand command)
        {
            if (command.Label == "I am Sure")
            {
                _appSettings.Values.Remove("Themes");
                _appSettings.Values.Remove("OfflineMode");
                if (await Base.IsFilePresent("history.log"))
                {
                    await (await ApplicationData.Current.LocalFolder.GetFileAsync("history.log")).DeleteAsync();
                }
                history_content.Text = "0 KB";
                var messageDialog = new MessageDialog("Reset Complete. Restart app to take effect.");
                await messageDialog.ShowAsync();
            }
        }
    }
}
