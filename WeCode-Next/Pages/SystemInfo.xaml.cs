using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Security.ExchangeActiveSyncProvisioning;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeCode_Next.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SystemInfo : Page
    {
        public SystemInfo()
        {
            this.InitializeComponent();

            var deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;

            // get the system version number
            var deviceFamilyVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            var version = ulong.Parse(deviceFamilyVersion);
            var majorVersion = (version & 0xFFFF000000000000L) >> 48;
            var minorVersion = (version & 0x0000FFFF00000000L) >> 32;
            var buildVersion = (version & 0x00000000FFFF0000L) >> 16;
            var revisionVersion = (version & 0x000000000000FFFFL);
            var systemVersion = $"{majorVersion}.{minorVersion}.{buildVersion}.{revisionVersion}";

            // get the device manufacturer, model name, OS details etc.
            var clientDeviceInformation = new EasClientDeviceInformation();
            var deviceManufacturer = clientDeviceInformation.SystemManufacturer;
            var deviceModel = clientDeviceInformation.SystemProductName;
            var operatingSystem = clientDeviceInformation.OperatingSystem;
            var systemHardwareVersion = clientDeviceInformation.SystemHardwareVersion;
            var systemFirmwareVersion = clientDeviceInformation.SystemFirmwareVersion;
        }
    }
}
