using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using NotepadOnlineMobile;
using NotepadOnlineMobile.Droid;

[assembly: ExportRenderer(typeof(AddButton), typeof(AddButtonRenderer))]
namespace NotepadOnlineMobile.Droid
{
    public class AddButtonRenderer : ViewRenderer<AddButton, Android.Widget.ImageButton>
    {
        public AddButtonRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<AddButton> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var button = new Android.Widget.ImageButton(Context);
                button.LongClickable = true;
                button.Click += (s, args) => Element.OnClick(s, args);
                button.LongClick += (s, args) => Element.OnLongClick(s, args);
                button.SetBackgroundResource(Resource.Drawable.addbutton);
                button.SetImageResource(Resource.Drawable.plus);
                button.SetPadding(10, 10, 10, 10);
                button.SetScaleType(ImageView.ScaleType.FitCenter);
                button.SetAdjustViewBounds(true);

                SetNativeControl(button);
            }
        }
    }
}