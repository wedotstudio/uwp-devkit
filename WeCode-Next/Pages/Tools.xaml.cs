using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WeCode_Next.DataModel;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Tools : Page
    {
        public Tools()
        {
            this.InitializeComponent();
            Loaded += Tools_Loaded;
        }

        private async void Tools_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Data/tools.csv"));
            using (var storageStream = await file.OpenReadAsync())
            {
                using (Stream stream = storageStream.AsStreamForRead())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        List<AccessItem> Items = new List<AccessItem>();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            Items.Add(new AccessItem { Name=values[0], UriStr = new Uri(values[1]), glyph=values[2]});
                        }
                        gridView.ItemsSource = Items;

                        reader.Dispose();
                    }
                    stream.Dispose();
                }
                storageStream.Dispose();
            }
        }

        private void Icon_Click(object sender, ItemClickEventArgs e)
        {

        }

    }
}
