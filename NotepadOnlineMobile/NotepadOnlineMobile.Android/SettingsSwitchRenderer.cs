using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Android;
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

[assembly: ExportRenderer(typeof(SettingsSwitch), typeof(SettingsSwitchRenderer))]
namespace NotepadOnlineMobile.Droid
{
    public class SettingsSwitchRenderer : SwitchRenderer
    {
        /*class MaskedListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
        {
            public bool OnTouch(Android.Views.View v, MotionEvent e)
            {
                return e.ActionMasked == MotionEventActions.Move;
            }
        }*/

        public SettingsSwitchRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Clickable = false;
                //Control.SetOnTouchListener(new MaskedListener());
            }
        }
    }
}