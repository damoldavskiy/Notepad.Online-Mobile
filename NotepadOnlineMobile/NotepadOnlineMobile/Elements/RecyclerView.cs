using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public class RecyclerView : ListView
    {
        public static BindableProperty CircleColorProperty = BindableProperty.Create("CircleColor", typeof(Color), typeof(RecyclerView), Color.White);
        public static BindableProperty ArrowColorProperty = BindableProperty.Create("ArrowColor", typeof(Color), typeof(RecyclerView), Color.Black);

        public Color CircleColor
        {
            get
            {
                return (Color)GetValue(CircleColorProperty);
            }
            set
            {
                SetValue(CircleColorProperty, value);
            }
        }

        public Color ArrowColor
        {
            get
            {
                return (Color)GetValue(ArrowColorProperty);
            }
            set
            {
                SetValue(ArrowColorProperty, value);
            }
        }
    }
}
