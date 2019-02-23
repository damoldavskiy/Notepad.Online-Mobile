using Android.App;

using NotepadOnlineMobile.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(ColorSetter))]
namespace NotepadOnlineMobile.Droid
{
    public class ColorSetter : IColorSetter
    {
        public static Activity Activity { get; set; }

        public void SetStatusBarColor(Color color)
        {
            Activity.Window.SetStatusBarColor(color.ToAndroid());
        }
    }
}