using System;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;

namespace WeCode_Next.Core
{
    public class Generator
    {
        /// <summary>
        /// Resize the file to a certain level
        /// </summary>
        /// <param name="name">Final Name Of the Files Generated </param>
        /// <param name="hsize">Height Of Image</param>
        /// <param name="wsize">Width Of Image</param>
        /// <param name="file">The File That Needs To Be Resized</param>
        /// <param name="folder">Output Location</param>
        public async static Task Resizer(String name, uint hsize, uint wsize, StorageFile file, StorageFolder folder)
        {
            StorageFile newFile = await folder.CreateFileAsync(name);
            using (var sourceStream = await file.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(sourceStream);
                BitmapTransform transform = new BitmapTransform() { ScaledHeight = hsize, ScaledWidth = wsize };
                PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                    BitmapPixelFormat.Rgba8,
                    BitmapAlphaMode.Straight,
                    transform,
                    ExifOrientationMode.RespectExifOrientation,
                    ColorManagementMode.DoNotColorManage);

                using (var destinationStream = await newFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, destinationStream);
                    encoder.SetPixelData(BitmapPixelFormat.Rgba8, BitmapAlphaMode.Premultiplied, wsize, hsize, 100, 100, pixelData.DetachPixelData());
                    await encoder.FlushAsync();
                }
            }
        }
    }
}

