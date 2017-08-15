using System;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Devices.Power;
using Windows.UI.Core;
using Windows.Devices.Enumeration;
using Windows.System.UserProfile;

namespace WeCode_Next.Pages
{

    public sealed partial class SystemInfo : Page
    {
        bool reportRequested = false;
        public SystemInfo()
        {
            this.InitializeComponent();
            var deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
            df.Text = deviceFamily;

            // get the system version number
            var deviceFamilyVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            var version = ulong.Parse(deviceFamilyVersion);
            var majorVersion = (version & 0xFFFF000000000000L) >> 48;
            var minorVersion = (version & 0x0000FFFF00000000L) >> 32;
            var buildVersion = (version & 0x00000000FFFF0000L) >> 16;
            var revisionVersion = (version & 0x000000000000FFFFL);
            var systemVersion = $"{majorVersion}.{minorVersion}.{buildVersion}.{revisionVersion}";
            sv.Text = systemVersion;

            // get the device manufacturer, model name, OS details etc.
            var clientDeviceInformation = new EasClientDeviceInformation();
            var deviceManufacturer = clientDeviceInformation.SystemManufacturer;
            dm.Text = deviceManufacturer;
            var deviceModel = clientDeviceInformation.SystemProductName;
            dM.Text = deviceModel;

            Battery.AggregateBattery.ReportUpdated += AggregateBattery_ReportUpdated;
            GetBatteryReport();
        }
        private void GetBatteryReport()
        {
            // Clear UI
            BatteryReportPanel1.Children.Clear();
            BatteryReportPanel2.Children.Clear();


            // Request aggregate battery report
            RequestAggregateBatteryReport();

            // Request individual battery report
            RequestIndividualBatteryReports();

            // Note request
            reportRequested = true;
        }

        private void RequestAggregateBatteryReport()
        {
            // Create aggregate battery object
            var aggBattery = Battery.AggregateBattery;

            // Get report
            var report = aggBattery.GetReport();

            // Update UI
            AddReportUI(BatteryReportPanel1, report, aggBattery.DeviceId);
        }

        async private void RequestIndividualBatteryReports()
        {
            // Find batteries 
            var deviceInfo = await DeviceInformation.FindAllAsync(Battery.GetDeviceSelector());
            foreach (DeviceInformation device in deviceInfo)
            {
                try
                {
                    // Create battery object
                    var battery = await Battery.FromIdAsync(device.Id);

                    // Get report
                    var report = battery.GetReport();

                    // Update UI
                    AddReportUI(BatteryReportPanel2, report, battery.DeviceId);
                }
                catch { /* Add error handling, as applicable */ }
            }
        }


        private void AddReportUI(StackPanel sp, BatteryReport report, string DeviceID)
        {
            // Create battery report UI
            TextBlock txt1 = new TextBlock { Text = "Device ID: " + DeviceID };
            txt1.FontSize = 15;
            txt1.Margin = new Thickness(0, 15, 0, 0);
            txt1.TextWrapping = TextWrapping.WrapWholeWords;

            TextBlock txt2 = new TextBlock { Text = "Battery status: " + report.Status.ToString() };
            txt2.FontStyle = Windows.UI.Text.FontStyle.Italic;
            txt2.Margin = new Thickness(0, 0, 0, 15);

            TextBlock txt3 = new TextBlock { Text = "Charge rate (mW): " + report.ChargeRateInMilliwatts.ToString() };
            TextBlock txt4 = new TextBlock { Text = "Design energy capacity (mWh): " + report.DesignCapacityInMilliwattHours.ToString() };
            TextBlock txt5 = new TextBlock { Text = "Fully-charged energy capacity (mWh): " + report.FullChargeCapacityInMilliwattHours.ToString() };
            TextBlock txt6 = new TextBlock { Text = "Remaining energy capacity (mWh): " + report.RemainingCapacityInMilliwattHours.ToString() };

            // Create energy capacity progress bar & labels
            TextBlock pbLabel = new TextBlock { Text = "Percent remaining energy capacity" };
            pbLabel.Margin = new Thickness(0, 10, 0, 5);
            pbLabel.FontFamily = new FontFamily("Segoe UI");
            pbLabel.FontSize = 11;

            ProgressBar pb = new ProgressBar()
            {
                Margin = new Thickness(0, 5, 0, 0),
                Width = 200,
                Height = 10,
                IsIndeterminate = false,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            TextBlock pbPercent = new TextBlock()
            {
                Margin = new Thickness(0, 5, 0, 10),
                FontFamily = new FontFamily("Segoe UI")
            };
            pbLabel.FontSize = 11;

            // Disable progress bar if values are null
            if ((report.FullChargeCapacityInMilliwattHours == null) ||
                (report.RemainingCapacityInMilliwattHours == null))
            {
                pb.IsEnabled = false;
                pbPercent.Text = "N/A";
            }
            else
            {
                pb.IsEnabled = true;
                pb.Maximum = Convert.ToDouble(report.FullChargeCapacityInMilliwattHours);
                pb.Value = Convert.ToDouble(report.RemainingCapacityInMilliwattHours);
                pbPercent.Text = ((pb.Value / pb.Maximum) * 100).ToString("F2") + "%";
            }

            // Add controls to stackpanel
            sp.Children.Add(txt1);
            sp.Children.Add(txt2);
            sp.Children.Add(txt3);
            sp.Children.Add(txt4);
            sp.Children.Add(txt5);
            sp.Children.Add(txt6);
            sp.Children.Add(pbLabel);
            sp.Children.Add(pb);
            sp.Children.Add(pbPercent);
        }

        async private void AggregateBattery_ReportUpdated(Battery sender, object args)
        {
            if (reportRequested)
            {

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    // Clear UI
                    BatteryReportPanel1.Children.Clear();
                    BatteryReportPanel2.Children.Clear();

                    // Request aggregate battery report
                    RequestAggregateBatteryReport();

                    // Request individual battery report
                    RequestIndividualBatteryReports();
                });
            }
        }
    }
}
