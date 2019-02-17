using Android.Content;
using Android.Support.V4.Widget;

using NotepadOnlineMobile;
using NotepadOnlineMobile.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RefreshableListView), typeof(RefreshableListViewRenderer))]
namespace NotepadOnlineMobile.Droid
{
    public class RefreshableListViewRenderer : ListViewRenderer
    {
        public RefreshableListViewRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            
            if (Control != null)
            {
                if (Control.Parent is SwipeRefreshLayout)
                    ((SwipeRefreshLayout)Control.Parent).SetColorSchemeResources(Resource.Color.colorPrimary);
            }
        }
    }
}