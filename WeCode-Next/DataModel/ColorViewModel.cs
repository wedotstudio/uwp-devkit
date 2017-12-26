using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace WeCode_Next.DataModel
{
    public class ColorViewModel : INotifyPropertyChanged
    {
        Color picked_color;
        string rgb_r;
        string rgb_g;
        string rgb_b;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ColorViewModel()
        {
            Picked_Color = Color.FromArgb(0, 255, 255, 255);
            RGB_r = "255";
            RGB_g = "255";
            RGB_b = "255";
        }

        public Color Picked_Color
        {
            get { return this.picked_color; }
            set
            {
                this.picked_color = value;
                this.OnPropertyChanged();
            }
        }

        public string RGB_r {
            get { return this.rgb_r; }
            set
            {
                this.rgb_r = value;
                this.OnPropertyChanged();
            }
        }

        public string RGB_g
        {
            get { return this.rgb_g; }
            set
            {
                this.rgb_g = value;
                this.OnPropertyChanged();
            }
        }

        public string RGB_b
        {
            get { return this.rgb_b; }
            set
            {
                this.rgb_b = value;
                this.OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
