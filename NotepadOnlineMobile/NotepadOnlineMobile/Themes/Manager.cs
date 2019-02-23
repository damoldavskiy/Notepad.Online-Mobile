using System;
using System.Collections.Generic;
using System.Text;

using Xamarin;
using Xamarin.Forms;

namespace NotepadOnlineMobile.Themes
{
    public static class Manager
    {
        static Light light = new Light();
        static Dark dark = new Dark();

        public static void Update()
        {
            if (Settings.Storage.Theme == "Light")
                Application.Current.Resources.MergedDictionaries.Add(light);
            else
                Application.Current.Resources.MergedDictionaries.Add(dark);
        }
    }
}
