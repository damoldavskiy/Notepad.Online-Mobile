using Android.Content;

using NotepadOnlineMobile;
using NotepadOnlineMobile.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NonClickableSwitch), typeof(NonClickableSwitchRenderer))]
namespace NotepadOnlineMobile.Droid
{
    public class NonClickableSwitchRenderer : SwitchRenderer
    {
        public NonClickableSwitchRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Clickable = false;
            }
        }
    }
}