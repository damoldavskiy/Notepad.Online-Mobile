using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotepadOnlineMobile.Settings
{
    class FilesPage : AbstractPage
    {
        public FilesPage() : base("Files")
        {
            Items.Add(new SettingsItem()
            {
                Header = "Ask before delete",
                Value = (bool)Application.Current.Properties["askdel"] ? "Enabled" : "Disabled",
                Action = (item) =>
                {
                    if ((bool)Application.Current.Properties["askdel"])
                    {
                        Application.Current.Properties["askdel"] = false;
                        item.Value = "Disabled";
                    }
                    else
                    {
                        Application.Current.Properties["askdel"] = true;
                        item.Value = "Enabled";
                    }
                }
            });
        }
    }
}
