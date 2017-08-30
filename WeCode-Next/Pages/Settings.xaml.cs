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
    }
}
