using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;
using Windows.UI.ViewManagement;

namespace WeCode_Next.Core
{
    public static class DeviceInfoHelper
    {
        public static DeviceFormFactorType GetDeviceFormFactorType()
        {
            switch (AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Mobile":
                    return DeviceFormFactorType.Phone;
                case "Windows.Desktop":
                    return UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Mouse
                        ? DeviceFormFactorType.Desktop
                        : DeviceFormFactorType.Tablet;
                case "Windows.Universal":
                    return DeviceFormFactorType.IoT;
                case "Windows.Team":
                    return DeviceFormFactorType.SurfaceHub;
                default:
                    return DeviceFormFactorType.Other;
            }
        }

        public static string GetBuild()
        {
            var deviceFamilyVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            var version = ulong.Parse(deviceFamilyVersion);
            var buildVersion = (version & 0x00000000FFFF0000L) >> 16;
            var systemVersion = $"{buildVersion}";
            return systemVersion;
        }

        public static string GetDeviceManufacturer() {
            var clientDeviceInformation = new EasClientDeviceInformation();
            return clientDeviceInformation.SystemManufacturer;
        }

        public static string GetDeviceModel()
        {
            var clientDeviceInformation = new EasClientDeviceInformation();
            return clientDeviceInformation.SystemProductName;
        }

        
    }

    public enum DeviceFormFactorType
    {
        Phone,
        Desktop,
        Tablet,
        IoT,
        SurfaceHub,
        Other
    }
}
