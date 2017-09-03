using Microsoft.Azure.Mobile.Push;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using WeCode_Next.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
                Windows.Storage.FileProperties.BasicProperties basicProperties = await ( await ApplicationData.Current.LocalFolder.GetFileAsync("history.log")).GetBasicPropertiesAsync();
                string fileSize = string.Format("{0:##0.###}", basicProperties.Size * 0.001);
                history_content.Text = fileSize + " KB";
            }
            else {
                history_content.Text = "0 KB";
            }

            //Push Notifications
            if (_appSettings.Values.ContainsKey("IsPushEnabled"))
            {
                push_n.IsOn = Convert.ToBoolean(_appSettings.Values["IsPushEnabled"]);
            }
            else
            {
                push_n.IsOn = true;
                _appSettings.Values["IsPushEnabled"] = push_n.IsOn;
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

        private void push_n_Toggled(object sender, RoutedEventArgs e)
        {
            _appSettings.Values["IsPushEnabled"] = push_n.IsOn;
            Push.SetEnabledAsync(Convert.ToBoolean(_appSettings.Values["IsPushEnabled"]));
        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("You are about to reset the settings. Are you sure you want to proceed?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "I am Sure",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand(
                "Cancel",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }
        private async void CommandInvokedHandler(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            if (command.Label == "I am Sure")
            {
                _appSettings.Values.Remove("IsPushEnabled");
                _appSettings.Values.Remove("Themes");
                _appSettings.Values.Remove("OfflineMode");
                if (await Base.IsFilePresent("history.log"))
                {
                    await(await ApplicationData.Current.LocalFolder.GetFileAsync("history.log")).DeleteAsync();
                }
                history_content.Text = "0 KB";
                var messageDialog = new MessageDialog("Reset Complete. Restart app to take effect.");
                await messageDialog.ShowAsync();
            }
        }
    }
}
