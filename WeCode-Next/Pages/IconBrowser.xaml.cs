using System;
using System.IO;
using System.Text;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WeCode_Next.DataModel;
using System.Collections.Generic;
using Windows.ApplicationModel.DataTransfer;

namespace WeCode_Next.Pages
{
    public sealed partial class IconBrowser : Page
    {
        public IconBrowser()
        {
            this.InitializeComponent();
            Loaded += IconBrowser_Loaded;
            InitializeList();
        }

        private void IconBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            CmbFontFamily.SelectedIndex = 0;
        }
        private void InitializeList()
        {
            var fontList = InstalledFont.GetFonts();
            CmbFontFamily.ItemsSource = fontList;
        }
        private async void Icon_Click(object sender, ItemClickEventArgs e)
        {
            /*Character output = e.ClickedItem as Character;
            //AddToHistory(output);
            var currentAV = ApplicationView.GetForCurrentView();
            var newAV = CoreApplication.CreateNewView();
            await newAV.Dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            async () =>
                            {
                                var newWindow = Window.Current;
                                var newAppView = ApplicationView.GetForCurrentView();

                                newAppView.Title = "Details";

                                var frame = new Frame();
                                frame.Navigate(typeof(IconViewer), output);
                                newWindow.Content = frame;
                                newWindow.Activate();

                                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                                    newAppView.Id,
                                    ViewSizePreference.UseMinimum,
                                    currentAV.Id,
                                    ViewSizePreference.UseMinimum);
                            });
                            */
            if (iconPanel.Visibility == Visibility.Collapsed) iconPanel.Visibility = Visibility.Visible;
            Character Item = e.ClickedItem as Character;
            string c = Item.Char;

            char[] myChar = c.ToCharArray();
            c = "";
            foreach (char chars in myChar)
            {
                c += ((int)chars).ToString("X4");
            }
            //Segoe MDL2 Assets Database
            if (Item.Font == "Segoe MDL2 Assets")
            {
                //Get Info From Database
                List<string> codes = new List<string>();
                List<string> names = new List<string>();
                List<string> remarks = new List<string>();
                Uri uri = new Uri("ms-appx:///Assets/Data/data.csv");
                var storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
                using (var storageStream = await storageFile.OpenReadAsync())
                {
                    using (Stream stream = storageStream.AsStreamForRead())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {

                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var values = line.Split(',');
                                codes.Add(values[0]);
                                names.Add(values[1]);
                                remarks.Add(values[2]);
                            }
                        }
                    }
                }
                //SMA Retrive Data
                foreach (string code in codes)
                {
                    //It exists in database
                    if (c == code)
                    {
                        int count = codes.FindIndex(n => n == c);
                        string codestring = codes[count];
                        string remark = remarks[count];
                        int p = int.Parse(code, System.Globalization.NumberStyles.HexNumber);
                        Ficon_i.Text = ((char)p).ToString();
                        Ficon.Text = ((char)p).ToString();
                        Ftext.Text = names[count];
                        FiconT.Text = "\u0026\u0023x" + codestring + "\u003B";
                        FiconS.Text = "\u003cFontIcon\u0020FontFamily\u003d\u0022" + Item.Font + "\u0022\u0020Glyph\u003d\u0022\u0026\u0023x" + codestring + "\u003B\u0022\u002f\u003e";
                        codestring = codestring.ToLower();
                        Ucode.Text = "\\u" + code;
                        if (remark == "" || remark == null)
                        {
                            Rm.Text = "N/A";
                        }
                        else
                        {
                            Rm.Text = remark;
                        }
                    }
                    else
                    {
                        string codestring = c;
                        Rm.Text = "N/A";
                        int p = int.Parse(c, System.Globalization.NumberStyles.HexNumber);
                        Ficon_i.Text = ((char)p).ToString();
                        Ficon.Text = ((char)p).ToString();
                        Ftext.Text = "U+" + c;
                        FiconT.Text = "\u0026\u0023x" + codestring + "\u003B";
                        FiconS.Text = "\u003cFontIcon\u0020FontFamily\u003d\u0022" + Item.Font + "\u0022\u0020Glyph\u003d\u0022\u0026\u0023x" + codestring + "\u003B\u0022\u002f\u003e";
                        codestring = codestring.ToLower();
                        Ucode.Text = "\\u" + c;
                    }

                }
            }
            else {
                string codestring = c;
                Rm.Text = "N/A";
                int p = int.Parse(c, System.Globalization.NumberStyles.HexNumber);
                Ficon_i.FontFamily = new Windows.UI.Xaml.Media.FontFamily(Item.Font);
                Ficon.FontFamily = new Windows.UI.Xaml.Media.FontFamily(Item.Font);
                Ficon_i.Text = ((char)p).ToString();
                Ficon.Text = ((char)p).ToString();
                Ftext.Text = "U+" + c;
                FiconT.Text = "\u0026\u0023x" + codestring + "\u003B";
                FiconS.Text = "\u003cFontIcon\u0020FontFamily\u003d\u0022" + Item.Font + "\u0022\u0020Glyph\u003d\u0022\u0026\u0023x" + codestring + "\u003B\u0022\u002f\u003e";
                codestring = codestring.ToLower();
                Ucode.Text = "\\u" + c;
            }
        }
        private async void AddToHistory(Character data)
        {
            String ori = "";

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("history_icon.log");
            StringBuilder result = new StringBuilder();
            using (var storageStream = await sampleFile.OpenReadAsync())
            {
                using (Stream Inputstream = storageStream.AsStreamForRead())
                {
                    using (StreamReader reader = new StreamReader(Inputstream))
                    {
                        ori = reader.ReadToEnd();
                        ori = ori + data.Font + "," + data.Char + "," + data.UnicodeIndex + Environment.NewLine;
                        reader.Dispose();
                    }
                    Inputstream.Dispose();
                }
                storageStream.Dispose();
            }
            var streamout = await sampleFile.OpenAsync(FileAccessMode.ReadWrite);
            using (var outputStream = streamout.GetOutputStreamAt(0))
            {
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    dataWriter.WriteString(ori);
                    await dataWriter.StoreAsync();
                    await outputStream.FlushAsync();
                }
            }
            streamout.Dispose();
        }

        private void CmbFontFamily_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var font = (sender as ComboBox).SelectedItem as InstalledFont;
            var fontList = InstalledFont.GetFonts();
            var items = font.GetCharacters();
            gridView.ItemsSource = items;
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Copy(Ficon.Text);
        }

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Copy(FiconT.Text);
        }

        private void Button_Click_2(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Copy(Ucode.Text);
        }
        public void Copy(String a)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(a);
            Clipboard.SetContent(dataPackage);
        }

        private void Button_Click_3(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Copy(FiconS.Text);
        }
    }
}
