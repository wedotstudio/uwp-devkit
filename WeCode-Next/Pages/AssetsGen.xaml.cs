using SharpDX.Direct2D1;
using System;
using System.Linq;
using WeCode_Next.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeCode_Next.Pages
{
    public sealed partial class AssetsGen : Page
    {
        public bool IsPicked1 = false;
        public bool IsPicked2 = false;
        public bool Iss44LVisible = true;
        public StorageFile StoredFile1 = null;
        public StorageFile StoredFile2 = null;
        public AssetsGen()
        {
            this.InitializeComponent();
            s44L.Checked += s44L_Checked;
            s44L.Unchecked += s44L_Unchecked;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StoredFile1 = null;
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            StoredFile1 = file;
            if (file != null)
            {
                Stor.Text = file.Name.Substring(0, file.Name.LastIndexOf("."));
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.DecodePixelHeight = 1240;
                    bitmapImage.DecodePixelWidth = 1240;

                    await bitmapImage.SetSourceAsync(fileStream);
                    img_s.Source = bitmapImage;
                }
                
                IsPicked1 = true;
            }

        }
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StoredFile2 = null;
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            StoredFile2 = file;
            if (file != null)
            {
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.DecodePixelHeight = 1200;
                    bitmapImage.DecodePixelWidth = 2480;

                    await bitmapImage.SetSourceAsync(fileStream);
                    img_w.Source = bitmapImage;
                }

                IsPicked2 = true;
            }
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsPicked1 && IsPicked2)
            {
                var folderPicker = new Windows.Storage.Pickers.FolderPicker();
                folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
                folderPicker.FileTypeFilter.Add(".png");
                StorageFolder folder = await folderPicker.PickSingleFolderAsync();
                if (folder != null && StoredFile1 != null && StoredFile2 != null)
                {

                    Windows.Storage.AccessCache.StorageApplicationPermissions.
                    FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                    StorageFolder newFolder = await folder.CreateFolderAsync(Stor.Text);
                    if (s44L.IsChecked == true)
                    {
                        await Generator.Resizer("Square44x44Logo.scale-400.png", 176, 176, StoredFile1, newFolder);
                        await Generator.Resizer("Square44x44Logo.scale-200.png", 88, 88, StoredFile1, newFolder);
                        await Generator.Resizer("Square44x44Logo.scale-150.png", 66, 66, StoredFile1, newFolder);
                        await Generator.Resizer("Square44x44Logo.scale-125.png", 55, 55, StoredFile1, newFolder);
                        await Generator.Resizer("Square44x44Logo.scale-100.png", 44, 44, StoredFile1, newFolder);
                        if (s44TL.IsChecked == true)
                        {
                            await Generator.Resizer("Square44x44Logo.targetsize-256.png", 256, 256, StoredFile1, newFolder);
                            await Generator.Resizer("Square44x44Logo.targetsize-48.png", 48, 48, StoredFile1, newFolder);
                            await Generator.Resizer("Square44x44Logo.targetsize-32.png", 32, 32, StoredFile1, newFolder);
                            await Generator.Resizer("Square44x44Logo.targetsize-24.png", 24, 24, StoredFile1, newFolder);
                            await Generator.Resizer("Square44x44Logo.targetsize-16.png", 16, 16, StoredFile1, newFolder);

                        }
                        if (s44uTL.IsChecked == true)
                        {
                            await Generator.Resizer("Square44x44Logo.altform-unplated_targetsize-256.png", 256, 256, StoredFile1, newFolder);
                            await Generator.Resizer("Square44x44Logo.altform-unplated_targetsize-48.png", 48, 48, StoredFile1, newFolder);
                            await Generator.Resizer("Square44x44Logo.altform-unplated_targetsize-32.png", 32, 32, StoredFile1, newFolder);
                            await Generator.Resizer("Square44x44Logo.altform-unplated_targetsize-24.png", 24, 24, StoredFile1, newFolder);
                            await Generator.Resizer("Square44x44Logo.altform-unplated_targetsize-16.png", 16, 16, StoredFile1, newFolder);
                        }
                    }
                    if (s71L.IsChecked == true)
                    {
                        await Generator.Resizer("SmallTile.scale-400.png", 284, 284, StoredFile1, newFolder);
                        await Generator.Resizer("SmallTile.scale-200.png", 142, 142, StoredFile1, newFolder);
                        await Generator.Resizer("SmallTile.scale-150.png", 107, 107, StoredFile1, newFolder);
                        await Generator.Resizer("SmallTile.scale-125.png", 89, 89, StoredFile1, newFolder);
                        await Generator.Resizer("SmallTile.scale-100.png", 71, 71, StoredFile1, newFolder);
                    }
                    if (mL.IsChecked == true)
                    {
                        await Generator.Resizer("Square150x150Logo.scale-400.png", 600, 600, StoredFile1, newFolder);
                        await Generator.Resizer("Square150x150Logo.scale-200.png", 300, 300, StoredFile1, newFolder);
                        await Generator.Resizer("Square150x150Logo.scale-150.png", 225, 225, StoredFile1, newFolder);
                        await Generator.Resizer("Square150x150Logo.scale-125.png", 188, 188, StoredFile1, newFolder);
                        await Generator.Resizer("Square150x150Logo.scale-100.png", 150, 150, StoredFile1, newFolder);
                    }
                    if (wL.IsChecked == true)
                    {
                        await Generator.Resizer("Wide310x150Logo.scale-400.png", 600, 1240, StoredFile2, newFolder);
                        await Generator.Resizer("Wide310x150Logo.scale-200.png", 300, 620, StoredFile2, newFolder);
                        await Generator.Resizer("Wide310x150Logo.scale-150.png", 225, 465, StoredFile2, newFolder);
                        await Generator.Resizer("Wide310x150Logo.scale-125.png", 188, 388, StoredFile2, newFolder);
                        await Generator.Resizer("Wide310x150Logo.scale-100.png", 150, 310, StoredFile2, newFolder);
                    }
                    if (lL.IsChecked == true)
                    {
                        await Generator.Resizer("LargeTile.scale-400.png", 1240, 1240, StoredFile1, newFolder);
                        await Generator.Resizer("LargeTile.scale-200.png", 620, 620, StoredFile1, newFolder);
                        await Generator.Resizer("LargeTile.scale-150.png", 465, 465, StoredFile1, newFolder);
                        await Generator.Resizer("LargeTile.scale-125.png", 388, 388, StoredFile1, newFolder);
                        await Generator.Resizer("LargeTile.scale-100.png", 310, 310, StoredFile1, newFolder);
                    }
                    if (sL.IsChecked == true)
                    {
                        await Generator.Resizer("StoreLogo.scale-400.png", 200, 200, StoredFile1, newFolder);
                        await Generator.Resizer("StoreLogo.scale-200.png", 100, 100, StoredFile1, newFolder);
                        await Generator.Resizer("StoreLogo.scale-150.png", 75, 75, StoredFile1, newFolder);
                        await Generator.Resizer("StoreLogo.scale-125.png", 63, 63, StoredFile1, newFolder);
                        await Generator.Resizer("StoreLogo.scale-100.png", 50, 50, StoredFile1, newFolder);
                    }
                    if (bL.IsChecked == true)
                    {
                        await Generator.Resizer("BadgeLogo.scale-400.png", 96, 96, StoredFile1, newFolder);
                        await Generator.Resizer("BadgeLogo.scale-200.png", 48, 48, StoredFile1, newFolder);
                        await Generator.Resizer("BadgeLogo.scale-150.png", 36, 36, StoredFile1, newFolder);
                        await Generator.Resizer("BadgeLogo.scale-125.png", 30, 30, StoredFile1, newFolder);
                        await Generator.Resizer("BadgeLogo.scale-100.png", 24, 24, StoredFile1, newFolder);
                    }
                    if (sS.IsChecked == true)
                    {
                        await Generator.Resizer("SplashScreen.scale-400.png", 1200, 2480, StoredFile2, newFolder);
                        await Generator.Resizer("SplashScreen.scale-200.png", 600, 1240, StoredFile2, newFolder);
                        await Generator.Resizer("SplashScreen.scale-150.png", 450, 930, StoredFile2, newFolder);
                        await Generator.Resizer("SplashScreen.scale-125.png", 375, 775, StoredFile2, newFolder);
                        await Generator.Resizer("SplashScreen.scale-100.png", 300, 620, StoredFile2, newFolder);


                    }
                    await new MessageDialog("The Requested Assets generated.").ShowAsync();
                    StoredFile1 = null;
                    StoredFile2 = null;
                    IsPicked1 = false;
                    IsPicked2 = false;
                }
            }
            else
            {
                await new MessageDialog("ERROR: Incomplete resources provided").ShowAsync();
            }
        }


        

        private void s44L_Checked(object sender, RoutedEventArgs e)
        {
            s44TL.IsEnabled = true;
            s44uTL.IsEnabled = true;
            s44TL.IsChecked = true;
            s44uTL.IsChecked = true;
        }

        private void s44L_Unchecked(object sender, RoutedEventArgs e)
        {
            s44TL.IsEnabled = false;
            s44uTL.IsEnabled = false;
            s44TL.IsChecked = false;
            s44uTL.IsChecked = false;
        }

        private void Grid_DragOver_s(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
            e.DragUIOverride.Caption = "drop 1240x1240 .png file here"; // Sets custom UI text
            e.DragUIOverride.IsCaptionVisible = true; // Sets if the caption is visible
            e.DragUIOverride.IsContentVisible = true; // Sets if the dragged content is visible
        }

        private async void Grid_Drop_s(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Count > 0)
                {
                    var storageFile = items[0] as StorageFile;
                    StoredFile1 = storageFile;
                    if (storageFile != null)
                    {
                        Stor.Text = storageFile.Name.Substring(0, storageFile.Name.LastIndexOf("."));
                        using (IRandomAccessStream fileStream = await storageFile.OpenAsync(FileAccessMode.Read))
                        {
                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.DecodePixelHeight = 1240;
                            bitmapImage.DecodePixelWidth = 1240;

                            await bitmapImage.SetSourceAsync(fileStream);
                            img_s.Source = bitmapImage;
                        }

                        IsPicked1 = true;
                    }
                }
            }
        }

        private void Grid_Drop_l(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
            e.DragUIOverride.Caption = "drop 1200x2480.png file here"; // Sets custom UI text
            e.DragUIOverride.IsCaptionVisible = true; // Sets if the caption is visible
            e.DragUIOverride.IsContentVisible = true; // Sets if the dragged content is visible
        }

        private async void Grid_DragOver_l(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Count > 0)
                {
                    var storageFile = items[0] as StorageFile;
                    StoredFile2 = storageFile;
                    if (storageFile != null)
                    {
                        Stor.Text = storageFile.Name.Substring(0, storageFile.Name.LastIndexOf("."));
                        using (IRandomAccessStream fileStream = await storageFile.OpenAsync(FileAccessMode.Read))
                        {
                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.DecodePixelHeight = 1200;
                            bitmapImage.DecodePixelWidth = 2480;

                            await bitmapImage.SetSourceAsync(fileStream);
                            img_w.Source = bitmapImage;
                        }

                        IsPicked2 = true;
                    }
                }
            }
        }
    }
}
