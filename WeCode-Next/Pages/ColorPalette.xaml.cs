using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace WeCode_Next.Pages
{
    public sealed partial class ColorPalette : Page
    {
        public ColorPalette()
        {
            this.InitializeComponent();

            InitialData();
        }

        private void InitialData()
        {
            Color basecolor = Main_Picker.Color;
            rgb_r.Text = basecolor.R.ToString();
            rgb_g.Text = basecolor.G.ToString();
            rgb_b.Text = basecolor.B.ToString();

        }
    }
}
