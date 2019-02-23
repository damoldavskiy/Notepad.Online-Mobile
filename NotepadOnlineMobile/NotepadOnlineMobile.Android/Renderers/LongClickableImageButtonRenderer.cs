using Android.Content;

using NotepadOnlineMobile;
using NotepadOnlineMobile.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LongClickableImageButton), typeof(LongClickableImageButtonRenderer))]
namespace NotepadOnlineMobile.Droid
{
    public class LongClickableImageButtonRenderer : ImageButtonRenderer
    {
        public LongClickableImageButtonRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<ImageButton> e)
        {
            base.OnElementChanged(e);
            
            LongClickable = true;
            LongClick += (sender, args) => ((LongClickableImageButton)e.NewElement).OnLongClicked();
        }
    }
}