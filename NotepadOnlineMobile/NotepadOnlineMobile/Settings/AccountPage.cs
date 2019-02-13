using Xamarin.Forms;

namespace NotepadOnlineMobile.Settings
{
    class AccountPage : AbstractPage
    {
        public AccountPage() : base("Account")
        {
            Items.Add(new SettingsItem()
            {
                Header = "Auto-login",
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
                Header = "Change password",
                Action = (item) =>
                {
                    var page = new EditPasswordPage();
                    page.Title = "Changing password";
                    Navigation.PushAsync(page);
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
