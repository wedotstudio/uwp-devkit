using System;
using System.Collections.Generic;
using System.Diagnostics;
using WeCode_Next.DataModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WeCode_Next.Pages
{
    public sealed partial class GUIDGen : Page
    {
        public GUIDGen()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<GUID> outputlist = new List<GUID> { };
            int a = Convert.ToInt32(((ComboBoxItem)stuff.SelectedItem).Content.ToString());
            for (int b = 0; b < a; b++)
            {
                string tmp = Guid.NewGuid().ToString();
                if (up.IsChecked == true) tmp = tmp.ToUpper();
                if (w.IsChecked == true) tmp = "{" + tmp + "}";
                outputlist.Add(new GUID { ID = tmp });
            }

            output.ItemsSource = outputlist;

        }

        private void output_ItemClick(object sender, ItemClickEventArgs e)
        {
            GUID id = e.ClickedItem as GUID;
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(id.ID);
            Clipboard.SetContent(dataPackage);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string tmp = "";
            if (output.Items.Count > 0)
            {
                foreach (GUID item in output.Items)
                {
                    tmp += item.ID + "\r";
                }
                DataPackage dataPackage = new DataPackage();
                dataPackage.SetText(tmp);
                Clipboard.SetContent(dataPackage);
            }
        }
    }
}
