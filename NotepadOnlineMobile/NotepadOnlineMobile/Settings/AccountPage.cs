using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotepadOnlineMobile.Settings
{
    class AccountPage : AbstractPage
    {
        public AccountPage() : base("Account")
        {
            Items.Add(new SettingsItem()
            {
                Header = "Auto-authorization",
                Value = (bool)Application.Current.Properties["autoreg"]? "Enabled" : "Disabled",
                Action = (item) =>
                {
                    if ((bool)Application.Current.Properties["autoreg"])
                    {
                        Application.Current.Properties["autoreg"] = false;
                        item.Value = "Disabled";
                    }
                    else
                    {
                        Application.Current.Properties["autoreg"] = true;
                        item.Value = "Enabled";
                    }
                }
            });

            Items.Add(new SettingsItem()
            {
                Header = "Log out",
                Action = (item) =>
                {
                    Application.Current.Properties.Remove("login");
                    Application.Current.Properties.Remove("password");
                    Application.Current.Properties.Remove("token");
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
            });
        }
    }
}
