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
                Value = (bool)Storage.Get("autoreg") ? "Enabled" : "Disabled",
                ValueVisible = true,
                SwitcherVisible = true,
                SwitcherToggled = (bool)Storage.Get("autoreg"),
                Action = (item) =>
                {
                    if ((bool)Storage.Get("autoreg"))
                    {
                        Storage.Set("autoreg", false);
                        item.Value = "Disabled";
                        item.SwitcherToggled = false;
                    }
                    else
                    {
                        Storage.Set("autoreg", true);
                        item.Value = "Enabled";
                        item.SwitcherToggled = true;
                    }
                }
            });

            Items.Add(new SettingsItem()
            {
                Header = "Log out",
                Action = (item) =>
                {
                    Storage.Set("login", "");
                    Storage.Set("password", "");
                    Storage.Set("token", "");
                    Application.Current.MainPage = new LoginRegisterPage();
                }
            });
        }
    }
}
