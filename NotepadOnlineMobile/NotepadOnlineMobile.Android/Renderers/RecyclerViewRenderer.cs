using Android.Content;
using Android.Support.V4.Widget;

using NotepadOnlineMobile;
using NotepadOnlineMobile.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RecyclerView), typeof(RecyclerViewRenderer))]
namespace NotepadOnlineMobile.Droid
{
    public class RecyclerViewRenderer : ListViewRenderer
    {
        public RecyclerViewRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            
            if (Control != null)
                if (Control.Parent is SwipeRefreshLayout)
                {
                    var element = (RecyclerView)Element;
                    var refresh = (SwipeRefreshLayout)Control.Parent;

                    refresh.SetProgressBackgroundColorSchemeColor(element.CircleColor.ToAndroid());
                    refresh.SetColorSchemeColors(element.ArrowColor.ToAndroid());
                }
        }
    }
}