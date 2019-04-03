using Xamarin.Forms;

namespace NotepadOnlineMobile.Settings
{
    class AccountPage : AbstractPage
    {
        public AccountPage() : base("Account")
        {
            Items.Add(new SettingsItem
            {
                Header = "Auto-login",
                Value = Storage.AutoLogin ? "Enabled" : "Disabled",
                ValueVisible = true,
                SwitcherVisible = true,
                SwitcherToggled = Storage.AutoLogin,
                Action = (item) =>
                {
                    if (Storage.AutoLogin)
                    {
                        Storage.AutoLogin = false;
                        item.Value = "Disabled";
                        item.SwitcherToggled = false;
                    }
                    else
                    {
                        Storage.AutoLogin = true;
                        item.Value = "Enabled";
                        item.SwitcherToggled = true;
                    }
                }
            });

            Items.Add(new SettingsItem
            {
                Header = "Change password",
                Action = (item) =>
                {
                    var page = new ChangePasswordPage
                    {
                        Title = "Changing password"
                    };
                    Navigation.PushAsync(page);
                }
            });

            Items.Add(new SettingsItem
            {
                Header = "Log out",
                Action = (item) =>
                {
                    Storage.Email = "";
                    Storage.Password = "";
                    Storage.Token = "";
                    Application.Current.MainPage = new LoginRegisterPage();
                }
            });
        }
    }
}
